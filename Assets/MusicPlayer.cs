using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{

    [FMODUnity.EventRef] public string AnxietyMusic;
    [FMODUnity.EventRef] public string AnxietySoundLayer;

    public static FMOD.Studio.EventInstance AnxietyMusicInstance;
    public static FMOD.Studio.EventInstance AnxietySoundLayerInstance;

    void Start()
    {
        AnxietyMusicInstance = FMODUnity.RuntimeManager.CreateInstance(AnxietyMusic);
        AnxietySoundLayerInstance = FMODUnity.RuntimeManager.CreateInstance(AnxietySoundLayer);

        AnxietyMusicInstance.start();
        AnxietySoundLayerInstance.start();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        AnxietyMusicInstance.release();
        AnxietySoundLayerInstance.release();

        AnxietyMusicInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        AnxietySoundLayerInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }

}
