using TMPro;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using System.Collections;

public class Stamping : MonoBehaviour
{
    public GameObject StampPrefab;
    public Transform hand; // Reference to the hand object
    public Transform stampPlace; // Reference to the stamp place object
    public TMP_Text scoreText; // Reference to the TextMeshPro UI text
    public float tolerance = 0.5f; // Tolerance for alignment
    private int score = 0;
    private float Distance;
    public EventReference StampReferance;
    private EventInstance StampInstance;
    public Animator Anim;

    private void Start()
    {
        Anim = GetComponent<Animator>();
        StampInstance = RuntimeManager.CreateInstance(StampReferance);
    }
    void Update()
    {
        Distance = Mathf.Abs(hand.position.x - stampPlace.position.x);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Anim.SetTrigger("Stamp");
            HandleStamp();
            StampInstance.start();
        }
        
    }

    void HandleStamp()
    {
        if (Distance<=tolerance)
        {
            score++;
            UpdateScoreText();
        }
        else
        {
            score--;
            UpdateScoreText();
        }
    }

    void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }

    IEnumerator StmpStamp()
    {/*
        GameObject newstamp Instantiate(StampPrefab,);
        New
        
        NewStampPlacement.Instance.GenerateRandomStampPlace();*/
        yield return null;
    }
}
