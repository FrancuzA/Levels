using UnityEngine;
using FMOD.Studio;
using FMODUnity;

public class PlayerController : MonoBehaviour
{
    private Animator _animator;
    public EventReference WritingReferance;
    private EventInstance WritingInstance;

    private bool isWriting = false; // Tracks whether the writing sound is currently playing

    private void Start()
    {
        WritingInstance = RuntimeManager.CreateInstance(WritingReferance);
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        bool isSpacePressed = Input.GetKey(KeyCode.Space);

        if (isSpacePressed)
        {
            // Set the player as cheating and trigger the "Cheating" animation
            Teacher.Instance.SetPlayerIsCheating(true);
            _animator.SetTrigger("Cheating");
            _animator.SetBool("basic", false);

            // Start the writing sound if it's not already playing
            if (!isWriting)
            {
                WritingInstance.start();
                isWriting = true;
            }
        }
        else
        {
            // Reset the player state and stop the writing sound
            Teacher.Instance.SetPlayerIsCheating(false);
            _animator.SetBool("basic", true);

            if (isWriting)
            {
                WritingInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
                isWriting = false;
            }
        }
    }
}