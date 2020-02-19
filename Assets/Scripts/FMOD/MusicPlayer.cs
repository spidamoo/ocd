using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{

    [FMODUnity.EventRef] public string AnxietyMusic;

    public static FMOD.Studio.EventInstance AnxietyMusicInstance;

    //public int startGame = 0;

    public int PuzzleCounter = -1;
    public PhoneAudio phoneAudio;


    void Start()
    {
        AnxietyMusicInstance = FMODUnity.RuntimeManager.CreateInstance(AnxietyMusic);
        AnxietyMusicInstance.start();

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


    //public void PlacePen()
    //{
    //    PlaceMarkerInstance.start();
    //}



    private void OnDestroy()
    {
        //AnxietyMusicInstance.release();
        AnxietyMusicInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }

}