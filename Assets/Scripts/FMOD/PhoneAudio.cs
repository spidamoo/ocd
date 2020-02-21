using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;

public class PhoneAudio : MonoBehaviour
{

    [FMODUnity.EventRef] public string AlexCallingEvent; 
    [FMODUnity.EventRef] public string PlayVoicemailEvent;

    FMOD.Studio.EventInstance AlexCallingInstance;
    FMOD.Studio.EventInstance PlayVoicemailInstance;


    void Start()
    {
        AlexCallingInstance = FMODUnity.RuntimeManager.CreateInstance(AlexCallingEvent);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(AlexCallingInstance, transform, GetComponent<Rigidbody>());
    
        PlayVoicemailInstance = FMODUnity.RuntimeManager.CreateInstance(PlayVoicemailEvent);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(PlayVoicemailInstance, transform, GetComponent<Rigidbody>());


        AlexCallingInstance.start();
        AlexCallingInstance.release();
    }


    public void ToVoicemail()
    {
        AlexCallingInstance.setParameterByName("ToVoicemail", 1f);
    }

    public void PlayVoicemail()
    {
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("Voicemail", 1f);
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("Anxiety", 1f);

        PlayVoicemailInstance.start();
    }

    public void StopVoicemail()
    {
        PlayVoicemailInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

}
