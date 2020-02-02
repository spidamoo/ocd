using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{

    [FMODUnity.EventRef] public string AnxietyMusic;
    public static FMOD.Studio.EventInstance AnxietyMusicInstance;
    public int PuzzleCounter = 1;

    private void Awake()
    {
        MusingSingleton();
    }




    void Start()
    {
        AnxietyMusicInstance = FMODUnity.RuntimeManager.CreateInstance(AnxietyMusic);

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


    public void InitiateNextPuzzle()
    {
        PuzzleCounter += 1;
        Debug.Log("PuzzleCounter: " + PuzzleCounter);
        ToAnxiety();
    }


    public void ToAnxiety()
    {
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("Anxiety", 1f);
    }

    public void ToRelief()
    {
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("Anxiety", 0f);
    }



    private void OnDestroy()
    {
        AnxietyMusicInstance.release();
        AnxietyMusicInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }

}
