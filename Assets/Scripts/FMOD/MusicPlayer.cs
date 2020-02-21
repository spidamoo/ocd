using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{

    [FMODUnity.EventRef] public string MasterAudioEvent;

    public static FMOD.Studio.EventInstance GameplayMasterInstance;

    //public int startGame = 0;

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
        //ToAnxiety();
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


    private void OnDestroy()
    {
        //AnxietyMusicInstance.release();
        GameplayMasterInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }

}