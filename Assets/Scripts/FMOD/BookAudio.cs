using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookAudio : MonoBehaviour
{
    [FMODUnity.EventRef] public string BookMoveAudioEvent;
    FMOD.Studio.EventInstance BookMoveAudioInstance;
    
    private Rigidbody rb;
    float bookSpeed;
    public bool bookPuzzleSolved = false;
    public int bookDistanceMultiplier = 1;


    void Start()
    {
        BookMoveAudioInstance = FMODUnity.RuntimeManager.CreateInstance(BookMoveAudioEvent);
        rb = GetComponent<Rigidbody>();
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(BookMoveAudioInstance, transform, rb);
    }

    void Update()
    {
        if (rb != null)
        {
            bookSpeed = rb.velocity.magnitude;
        }

        BookMoveAudioInstance.setParameterByName("BookSpeed", bookSpeed);
    }

    public void StartBookMoveAudio()
    {
        BookMoveAudioInstance.setParameterByName("End", 0);
        BookMoveAudioInstance.start();
    }

    public void BookPuzzleSolved()
    {
        BookMoveAudioInstance.setParameterByName("End", 1);
        BookMoveAudioInstance.release();

        bookPuzzleSolved = true;
        bookDistanceMultiplier = 1000;
    }
}