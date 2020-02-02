using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle2Notify : MonoBehaviour
{

    [FMODUnity.EventRef] public string PuzzleNotifyEvent;
    FMOD.Studio.EventInstance PuzzleNotifyInstance;

    int PuzzleTrigger;

    void Start()
    {
        PuzzleNotifyInstance = FMODUnity.RuntimeManager.CreateInstance(PuzzleNotifyEvent);
        //PuzzleTrigger = GetComponent<Somewhere>
    }

    public void ComeToPuzzle()
    {
        if (PuzzleTrigger == 2)
        {
            PuzzleNotifyInstance.start();
        }
    }

    void Update()
    {
        
    }
}
