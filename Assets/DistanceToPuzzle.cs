﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceToPuzzle : MonoBehaviour
{
    public Transform jarPuzzle;
    public Transform paintingPuzzle;
    public Transform bookPuzzle;
    JarAudio jarAudioScript;
    PaintingAudio paintingAudioScript;
    BookAudio bookAudioScript;
    float distanceToJars;
    float distanceToPainting;
    float distanceToBooks;

    private float puzzleType;


    void Start()
    {
        jarAudioScript = FindObjectOfType<JarAudio>();
        paintingAudioScript = FindObjectOfType<PaintingAudio>();
        bookAudioScript = FindObjectOfType<BookAudio>();
    }

    void Update()
    {
        SetNearestPuzzle();
    }

    private void SetNearestPuzzle()
    {
        distanceToJars = Vector3.Distance(jarPuzzle.position, transform.position);
        distanceToPainting = Vector3.Distance(paintingPuzzle.position, transform.position);
        distanceToBooks = Vector3.Distance(bookPuzzle.position, transform.position);

        //Debug.Log("Distance to jars: " + distanceToJars);

        if (distanceToJars < distanceToPainting && distanceToJars < distanceToBooks && !jarAudioScript.jarsPuzzleSolved)
        {
            FMODUnity.RuntimeManager.StudioSystem.setParameterByName("PuzzleType", 1);
        }
        else if (distanceToPainting < distanceToJars && distanceToPainting < distanceToBooks && !paintingAudioScript.paintingPuzzleSolved)
        {
            FMODUnity.RuntimeManager.StudioSystem.setParameterByName("PuzzleType", 2);
        }
        else if (distanceToBooks < distanceToJars && distanceToBooks < distanceToPainting && !bookAudioScript.bookPuzzleSolved)
        {
            FMODUnity.RuntimeManager.StudioSystem.setParameterByName("PuzzleType", 3);
        }

        else
        {
            FMODUnity.RuntimeManager.StudioSystem.setParameterByName("PuzzleType", 0);
        }

        FMODUnity.RuntimeManager.StudioSystem.getParameterByName("PuzzleType", out puzzleType);
        Debug.Log("PuzzleType: " + puzzleType);
    }

    //public void JarsPuzzleSolved()
    //{
    //    jarsPuzzleSolved = true;
    //}

    //public void PaintingPuzzleSolved()
    //{
    //    paintingPuzzleSolved = true;
    //}

    //public void BookPuzzleSolved()
    //{
    //    bookPuzzleSolved = true;
    //}

}