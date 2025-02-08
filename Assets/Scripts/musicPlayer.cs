using UnityEngine;
using FMOD.Studio;
using FMODUnity;

public class musicPlayer : MonoBehaviour
{
    public EventReference MusicReferance;
    private EventInstance MusicInstance;
    void Start()
    {
        MusicInstance = RuntimeManager.CreateInstance(MusicReferance);
        MusicInstance.start();
    }

    void OnDestroy()
    {
        // Stop and release the FMOD event instance when the object is destroyed
        if (MusicInstance.isValid())
        {
            MusicInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE); // Stop the music immediately
            MusicInstance.release(); // Release the event instance
        }
    }
}
