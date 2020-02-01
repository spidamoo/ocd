using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;

public class PhoneAudio : MonoBehaviour
{

    [FMODUnity.EventRef]
    public string AlexCallingEvent;
    FMOD.Studio.EventInstance AlexCalling;
    public string OneNewVoicemailEvent;
    FMOD.Studio.EventInstance OneNewVoicemail;

    Transform transformPos = null;

    void Start()
    {
        transformPos = GetComponent<Transform>();

        AlexCalling = FMODUnity.RuntimeManager.CreateInstance(AlexCallingEvent);
        AlexCalling.start();
        AlexCalling.release();
        //FMODUnity.RuntimeManager.PlayOneShotAttached(EventPath, gameObject);
    }



    void Update()
    {
        AlexCalling.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(transformPos));

        if (Input.GetKey(KeyCode.V))
        {
            Debug.Log("v pressed");
            AlexCalling.setParameterByName("ToVoicemail", 1f);

        }
    }
}
