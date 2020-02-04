using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityAmbience : MonoBehaviour
{
    [FMODUnity.EventRef] public string CityAmbienceEvent;
    FMOD.Studio.EventInstance CityAmbienceInstance;

    //Transform transformPos = null;

    private void Awake()
    {
    }

    private void Start()
    {
        CityAmbienceInstance = FMODUnity.RuntimeManager.CreateInstance(CityAmbienceEvent);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(CityAmbienceInstance, transform, GetComponent<Rigidbody>());
        CityAmbienceInstance.start();

        //transformPos = GetComponent<Transform>();
    }

    void Update()
    {
        //CityAmbienceInstance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(transformPos));
    }

    private void OnDestroy()
    {
        //CityAmb.release();
        CityAmbienceInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }
}
