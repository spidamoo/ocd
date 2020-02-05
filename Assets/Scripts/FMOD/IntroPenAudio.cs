using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroPenAudio : MonoBehaviour
{

    [FMODUnity.EventRef] public string PenScribbleEvent;
    [FMODUnity.EventRef] public string PenCapEvent;
    [FMODUnity.EventRef] public string PenOnFridgeEvent;



    public void PenIsScribbling()
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached(PenScribbleEvent, gameObject);
    }

    public void PutCapOnPen()
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached(PenCapEvent, gameObject);
    }

    public void PlacePenOnFridge()
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached(PenOnFridgeEvent, gameObject);
    }
}