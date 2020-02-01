using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbiencePlayer : MonoBehaviour
{

    [FMODUnity.EventRef]
    public string EventPath;
    FMOD.Studio.EventInstance RoomTone;

    private void Awake()
    {
        RoomTone = FMODUnity.RuntimeManager.CreateInstance(EventPath);
        RoomTone.start();
    }

    void Update()
    {
        
    }

    private void OnDestroy()
    {
        RoomTone.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }
}
