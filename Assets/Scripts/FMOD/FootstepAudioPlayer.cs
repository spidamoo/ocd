using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepAudioPlayer : MonoBehaviour
{

    [FMODUnity.EventRef] public string FootstepsEvent;
    FMOD.Studio.EventInstance FootstepsInstance;
    private FMOD.Studio.PLAYBACK_STATE FootstepsState;

    public CharacterController characterController;


    void Start()
    {
        FootstepsInstance = FMODUnity.RuntimeManager.CreateInstance(FootstepsEvent);
    }

    void Update()
    {
        Debug.Log("Character velocity: " + characterController.velocity.magnitude);

        FootstepsInstance.getPlaybackState(out FootstepsState);

        if (FootstepsState == FMOD.Studio.PLAYBACK_STATE.PLAYING)
        {

            //if (Input.GetKeyUp(KeyCode.W) | Input.GetKeyUp(KeyCode.A) | Input.GetKeyUp(KeyCode.S) | Input.GetKeyUp(KeyCode.D))
            if (characterController.velocity.magnitude < Mathf.Epsilon)

            {
                FootstepsInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            }

        }

        //else if (Input.GetKey(KeyCode.W) | Input.GetKey(KeyCode.A) | Input.GetKey(KeyCode.S) | Input.GetKey(KeyCode.D))

        else if (characterController.velocity.magnitude > Mathf.Epsilon)
        {
            FootstepsInstance.start();
        }
    }
}
