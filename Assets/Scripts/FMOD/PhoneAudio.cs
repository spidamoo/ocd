using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneAudio : MonoBehaviour
{

    [FMODUnity.EventRef]
    public string EventPath;
    //FMOD.Studio.EventInstance AlexCalling;


    void Start()
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached(EventPath, gameObject);
    }



    void Update()
    {
        if (Input.GetKey(KeyCode.V))
        {

        }
    }
}
