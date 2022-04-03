using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;

public class Effects : MonoBehaviour
{

    public WoodSpawnerManager timer;
    public PostProcessVolume postProcess;
    private Vignette vignette;

    private FloatParameter intensity = new FloatParameter();

    void Start()
    {
        postProcess.profile.TryGetSettings(out vignette);
    }

    void Update()
    {
        if(timer.elapsedTime >= 40)
        {
            return;
        }
        else
        {
            intensity.value = 0.3f + 0.3f * (timer.elapsedTime / 40);
            vignette.intensity.value = intensity;
        }

    }
}
