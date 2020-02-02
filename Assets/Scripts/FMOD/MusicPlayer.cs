using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{

    [FMODUnity.EventRef] public string AnxietyMusic;
    public static FMOD.Studio.EventInstance AnxietyMusicInstance;

    [FMODUnity.EventRef] public string PlaceMarkerEvent;
    public static FMOD.Studio.EventInstance PlaceMarkerInstance;


    public int startGame = 0;
    public int PuzzleCounter = 0;

    private void Awake()
    {
        MusingSingleton();
    }




    void Start()
    {
        AnxietyMusicInstance = FMODUnity.RuntimeManager.CreateInstance(AnxietyMusic);
        PlaceMarkerInstance = FMODUnity.RuntimeManager.CreateInstance(PlaceMarkerEvent);


        AnxietyMusicInstance.start();

    }

    void Update()
    {
        
    }

    private void MusingSingleton()
    {
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }


    public void StartGameMusic()
    {
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("StartGame", 1f);
    }

    public void InitiateNextPuzzle()
    {
        PuzzleCounter += 1;
        Debug.Log("PuzzleCounter: " + PuzzleCounter);
        //ToAnxiety();
    }


    public void ToAnxiety()
    {
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("Anxiety", 1f);
    }

    public void ToRelief()
    {
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("Anxiety", 0f);
    }


    public void PlacePen()
    {
        PlaceMarkerInstance.start();
    }



    private void OnDestroy()
    {
        AnxietyMusicInstance.release();
        AnxietyMusicInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }

}
