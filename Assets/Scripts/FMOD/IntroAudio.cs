using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroAudio : MonoBehaviour
{

    [FMODUnity.EventRef] public string IntroAudioEvent;
    public static FMOD.Studio.EventInstance IntroAudioInstance;

    //[FMODUnity.EventRef] public string MouseClickEvent;


    void Start()
    {
        IntroAudioInstance = FMODUnity.RuntimeManager.CreateInstance(IntroAudioEvent);
        IntroAudioInstance.start();

    }

    //void Update()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        FMODUnity.RuntimeManager.PlayOneShot(MouseClickEvent);
    //    }

    //}


    public void StartGame()
    {
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("StartGame", 1f);
    }


    private void OnDestroy()
    {
        IntroAudioInstance.release();
        IntroAudioInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }

}