using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightOscillator : MonoBehaviour
{
    [SerializeField] float maxIntensity = 135f;
    [SerializeField] [Range(0, 1)] float intensityProgress;
    [SerializeField] float period = 2f;

    Light thisLight;

    void Start()
    {
        thisLight = GetComponent<Light>();
    }


    void Update()
    {
        if (Time.time == 0f) return;
        if (period == 0) return;
        // Number of cycles already oscillated
        float cycles = Time.time / period;
        // Range of sin function - 1 cycle
        const float tau = Mathf.PI * 2;
        // Number of cycles * tau = radians
        float rawSinWave = Mathf.Sin(cycles * tau);
        // Sin goes from -1 to 1, convert to 0 to 1
        intensityProgress = (rawSinWave + 1) / 2;

        Oscillate();
    }

    void Oscillate()
    {
        thisLight.intensity = maxIntensity * intensityProgress;
    }
}
