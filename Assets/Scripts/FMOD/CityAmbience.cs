using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityAmbience : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string EventPath;
    FMOD.Studio.EventInstance CityAmb;

    Transform transformPos = null;

    private void Awake()
    {
        CityAmb = FMODUnity.RuntimeManager.CreateInstance(EventPath);
        CityAmb.start();
    }

    private void Start()
    {
        transformPos = GetComponent<Transform>();
    }

    void Update()
    {
        CityAmb.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(transformPos));
    }

    private void OnDestroy()
    {
        //CityAmb.release();
        CityAmb.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }
}
