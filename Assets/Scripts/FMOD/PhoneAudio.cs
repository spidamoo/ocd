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

    //int puzzleCounter = 0;

    Transform transformPos = null;

    void Start()
    {
        transformPos = GetComponent<Transform>();

        AlexCallingInstance = FMODUnity.RuntimeManager.CreateInstance(AlexCallingEvent);
        PlayVoicemailInstance = FMODUnity.RuntimeManager.CreateInstance(PlayVoicemailEvent);


        AlexCallingInstance.start();
        AlexCallingInstance.release();
        //FMODUnity.RuntimeManager.PlayOneShotAttached(EventPath, gameObject);
    }



    void Update()
    {
        AlexCallingInstance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(transformPos));
        PlayVoicemailInstance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(transformPos));

        //if (Input.GetKeyDown(KeyCode.P))
        //{
        //    puzzleCounter += 1;
        //    Debug.Log("puzzlecounter: " + puzzleCounter);
        //}

        //if (Input.GetKeyDown(KeyCode.V))
        //{
        //    FMODUnity.RuntimeManager.StudioSystem.setParameterByName("Voicemail", 1f);
        //    FMODUnity.RuntimeManager.StudioSystem.setParameterByName("Anxiety", 1f);
        //    PlayVoicemail();
        //}

        //if (Input.GetKeyDown(KeyCode.B))
        //{
        //    StopVoicemail();
        //}

    }

    public void ToVoicemail()
    {
        AlexCallingInstance.setParameterByName("ToVoicemail", 1f);
    }

    public void PlayVoicemail()
    {
        //FMODUnity.RuntimeManager.StudioSystem.setParameterByName("PuzzleCounter", puzzleCounter);
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("Voicemail", 1f);
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("Anxiety", 1f);

        PlayVoicemailInstance.start();
        //PlayVoicemailInstance.release();

    }

    public void StopVoicemail()
    {
        //FMODUnity.RuntimeManager.StudioSystem.setParameterByName("PuzzleCounter", puzzleCounter);
        PlayVoicemailInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

}
