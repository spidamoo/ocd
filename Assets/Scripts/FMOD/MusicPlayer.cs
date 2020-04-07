using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{

    [FMODUnity.EventRef] public string MasterAudioEvent;

    public static FMOD.Studio.EventInstance GameplayMasterInstance;

    public int PuzzleCounter = 0;
    public PhoneAudio phoneAudio;


    void Start()
    {
        GameplayMasterInstance = FMODUnity.RuntimeManager.CreateInstance(MasterAudioEvent);
        GameplayMasterInstance.start();

    }

    void Update()
    {
        
    }


    public void InitiateNextPuzzle()
    {
        PuzzleCounter++;
        Debug.Log("PuzzleCounter: " + PuzzleCounter);
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("PuzzleCounter", PuzzleCounter);
    }


    public void ToAnxiety()
    {
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("Anxiety", 1f);
    }

    public void ToRelief()
    {

        phoneAudio.StopVoicemail();
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("Anxiety", 0f);
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("Voicemail", 0f);

    }

    //public void JarPuzzle()
    //{
    //    FMODUnity.RuntimeManager.StudioSystem.setParameterByName("PuzzleType", 1f);
    //    InitiateNextPuzzle();
    //}

    //public void PaintingPuzzle()
    //{
    //    FMODUnity.RuntimeManager.StudioSystem.setParameterByName("PuzzleType", 2f);
    //    InitiateNextPuzzle();
    //}

    //public void InitiateBookPuzzle()
    //{
    //    FMODUnity.RuntimeManager.StudioSystem.setParameterByName("PuzzleType", 3f);
    //    InitiateNextPuzzle();
    //}



    private void OnDestroy()
    {
        GameplayMasterInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }

}