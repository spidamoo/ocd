using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbiencePlayer : MonoBehaviour
{

    [FMODUnity.EventRef] public string RoomToneEvent;
    FMOD.Studio.EventInstance RoomToneInstance;

    [FMODUnity.EventRef] public string PenScribbleEvent;
    FMOD.Studio.EventInstance PenScribbleInstance;


    private void Awake()
    {
        RoomToneInstance = FMODUnity.RuntimeManager.CreateInstance(RoomToneEvent);
        RoomToneInstance.start();

        PenScribbleInstance = FMODUnity.RuntimeManager.CreateInstance(PenScribbleEvent);
        PenScribbleInstance.start();
        //PenScribbleInstance.release();
    }

    void Update()
    {
        
    }

    private void OnDestroy()
    {
        RoomToneInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }
}
