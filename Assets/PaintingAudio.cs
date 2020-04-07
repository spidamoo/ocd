using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintingAudio : MonoBehaviour
{
    [FMODUnity.EventRef] public string PaintingMoveAudioEvent;
    FMOD.Studio.EventInstance PaintingMoveAudioInstance;

    private Rigidbody rb;
    private float paintingSpeed;
    public bool paintingPuzzleSolved = false;


    void Start()
    {
        PaintingMoveAudioInstance = FMODUnity.RuntimeManager.CreateInstance(PaintingMoveAudioEvent);
        rb = GetComponent<Rigidbody>();
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(PaintingMoveAudioInstance, transform, rb);
    }

    void Update()
    {
        paintingSpeed = Mathf.Abs(rb.angularVelocity.x);
        PaintingMoveAudioInstance.setParameterByName("PaintingSpeed", paintingSpeed);
    }

    public void StartPaintingMoveAudio()
    {
        PaintingMoveAudioInstance.start();
    }

    public void PaintingPuzzleSolved()
    {
        PaintingMoveAudioInstance.release();
        PaintingMoveAudioInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);

        paintingPuzzleSolved = true;
        Debug.Log("Painting Puzzle Solved");
    }
}