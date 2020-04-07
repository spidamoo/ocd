﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JarAudio : MonoBehaviour
{
    [FMODUnity.EventRef] public string JarMoveAudioEvent;
    FMOD.Studio.EventInstance JarMoveAudioInstance;

    private Rigidbody rb;
    private float jarSpeed;
    public bool jarsPuzzleSolved = false;


    void Start()
    {
        JarMoveAudioInstance = FMODUnity.RuntimeManager.CreateInstance(JarMoveAudioEvent);
        rb = GetComponent<Rigidbody>();
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(JarMoveAudioInstance, transform, rb);
    }

    void Update()
    {
        jarSpeed = Mathf.Abs(rb.angularVelocity.x);
        JarMoveAudioInstance.setParameterByName("JarSpeed", jarSpeed);
    }

    public void StartJarMoveAudio()
    {
        JarMoveAudioInstance.start();
    }

    public void JarsPuzzleSolved()
    {
        JarMoveAudioInstance.release();
        JarMoveAudioInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);

        jarsPuzzleSolved = true;
        Debug.Log("Jars Puzzle Solved");
    }
}