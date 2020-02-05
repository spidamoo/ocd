using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleNotify : MonoBehaviour
{

    [FMODUnity.EventRef] public string PuzzleNotifyEvent;
    FMOD.Studio.EventInstance PuzzleNotifyInstance;
    private bool isNotifying = false;
    private float notifyDelay;

    void Start()
    {
        PuzzleNotifyInstance = FMODUnity.RuntimeManager.CreateInstance(PuzzleNotifyEvent);
    }


    void Update()
    {
        PuzzleNotifyInstance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(transform));
        if (isNotifying)
        {
            if (notifyDelay < 0.0f)
            {
                NotifyMe();
                ResetTimer();
            }

            notifyDelay -= Time.deltaTime;
        }
    }

    private void ResetTimer()
    {
        notifyDelay = Random.Range(1.0f, 1.0f);
    }


    private void NotifyMe()
    {
        PuzzleNotifyInstance.start();
    }

    public void StartNotifying()
    {
        isNotifying = true;
        ResetTimer();
    }


    public void StopNotifying()
    {
        isNotifying = false;
        PuzzleNotifyInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }
}
