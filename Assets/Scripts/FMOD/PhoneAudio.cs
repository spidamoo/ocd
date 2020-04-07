using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using FMOD.Studio;

public class PhoneAudio : MonoBehaviour
{

    [FMODUnity.EventRef] public string PhoneRingEvent;
    [FMODUnity.EventRef] public string PhoneToVoicemailEvent;
    [FMODUnity.EventRef] public string PlayVoicemailEvent;

    FMOD.Studio.EventInstance PhoneRingInstance;
    FMOD.Studio.EventInstance PlayVoicemailInstance;


    void Start()
    {
        PhoneRingInstance = FMODUnity.RuntimeManager.CreateInstance(PhoneRingEvent);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(PhoneRingInstance, transform, GetComponent<Rigidbody>());
    
        PlayVoicemailInstance = FMODUnity.RuntimeManager.CreateInstance(PlayVoicemailEvent);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(PlayVoicemailInstance, transform, GetComponent<Rigidbody>());

        PhoneRingInstance.start();
        PhoneRingInstance.release();
    }


    public void ToVoicemail()
    {
        PhoneRingInstance.setParameterByName("ToVoicemail", 1f);
        FMODUnity.RuntimeManager.PlayOneShotAttached(PhoneToVoicemailEvent, gameObject);
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
