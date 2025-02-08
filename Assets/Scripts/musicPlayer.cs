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

    
}
