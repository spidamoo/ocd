using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepAudioTrigger : MonoBehaviour
{

    
    
    

    void OnTriggerEnter()
    {
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("Floor Surface", 1f);
    }

    void OnTriggerExit()
    {
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("Floor Surface", 0f);
    }
}
