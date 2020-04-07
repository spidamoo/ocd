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
    public int paintingDistanceMultiplier = 1;


    void Start()
    {
        PaintingMoveAudioInstance = FMODUnity.RuntimeManager.CreateInstance(PaintingMoveAudioEvent);
        rb = GetComponent<Rigidbody>();
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(PaintingMoveAudioInstance, transform, rb);
    }

    void Update()
    {
        if (rb != null)
        {
            paintingSpeed = Mathf.Abs(rb.angularVelocity.x);
        }

        if (Input.GetMouseButton(0))
        {
            //Debug.Log("Mouse Down");
            PaintingMoveAudioInstance.setParameterByName("PaintingSpeed", paintingSpeed);
        }
        else
        {
            PaintingMoveAudioInstance.setParameterByName("PaintingSpeed", 0f);
        }
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
        paintingDistanceMultiplier = 1000;
        //Debug.Log("Painting Puzzle Solved");
    }
}