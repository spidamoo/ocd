﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackPenAudio : MonoBehaviour
{

    [FMODUnity.EventRef] public string PenMoveAudioEvent;
    [FMODUnity.EventRef] public string PenPlaceAudioEvent;

    FMOD.Studio.EventInstance PenMoveAudioInstance;

    private Rigidbody rb;
    float penSpeed = 0f;

    void Start()
    {
        PenMoveAudioInstance= FMODUnity.RuntimeManager.CreateInstance(PenMoveAudioEvent);
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (rb != null)
        {
            penSpeed = rb.velocity.magnitude;
            PenMoveAudioInstance.setParameterByName("PenSpeed", penSpeed);
        }
    }

    public void StartPenMoveAudio()
    {
        PenMoveAudioInstance.setParameterByName("End", 0);
        PenMoveAudioInstance.start();
        PenMoveAudioInstance.release();
    }

    public void StopPenMoveAudio()
    {
        PenMoveAudioInstance.setParameterByName("End", 1);
        FMODUnity.RuntimeManager.PlayOneShotAttached(PenPlaceAudioEvent, gameObject);
    }
}