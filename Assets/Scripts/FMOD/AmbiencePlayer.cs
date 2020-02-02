using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbiencePlayer : MonoBehaviour
{

    [FMODUnity.EventRef] public string EventPath;
    FMOD.Studio.EventInstance RoomTone;

    [FMODUnity.EventRef] public string PenScribbleEvent;
    FMOD.Studio.EventInstance PenScribbleInstance;


    private void Awake()
    {
        RoomTone = FMODUnity.RuntimeManager.CreateInstance(EventPath);
        RoomTone.start();

        PenScribbleInstance = FMODUnity.RuntimeManager.CreateInstance(PenScribbleEvent);
        PenScribbleInstance.start();
        PenScribbleInstance.release();
    }

    void Update()
    {
        
    }

    private void OnDestroy()
    {
        RoomTone.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }
}
