using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepAudioTrigger : MonoBehaviour
{

    void OnTriggerEnter()
    {
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("FloorSurface", 1f);
        //Debug.Log("On Carpet");
    }

    void OnTriggerExit()
    {
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("FloorSurface", 0f);
        //Debug.Log("On Wood");
    }
}
