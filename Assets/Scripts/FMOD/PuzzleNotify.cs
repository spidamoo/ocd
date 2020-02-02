using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleNotify : MonoBehaviour
{

    [FMODUnity.EventRef] public string PuzzleNotifyEvent;
    FMOD.Studio.EventInstance PuzzleNotifyInstance;

    Transform transformPos = null;

    void Start()
    {
        PuzzleNotifyInstance = FMODUnity.RuntimeManager.CreateInstance(PuzzleNotifyEvent);
    }


    void Update()
    {
        PuzzleNotifyInstance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(transformPos));

    }


    public void NotifyMe()
    {
        PuzzleNotifyInstance.start();
    }


    public void ComeToMe()
    {
        PuzzleNotifyInstance.release();
        PuzzleNotifyInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }
}
