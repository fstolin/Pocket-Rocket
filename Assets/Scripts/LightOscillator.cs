using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightOscillator : MonoBehaviour
{
    [SerializeField] Vector3 maxMovementRange;
    [SerializeField] [Range (0,1)] float movementProgress;
    [SerializeField] AudioClip thudAudio;
    [SerializeField] float period = 2f;
    
    Vector3 startingPosition;
    Transform doorPos;
    AudioSource audioSource;

    void Start()
    {
        doorPos = GetComponent<Transform>();
        audioSource = GetComponent<AudioSource>();
        startingPosition = doorPos.position;
    }


    void Update()
    {
        if (Time.time == 0f) return;
        // Number of cycles already oscillated
        float cycles = Time.time / period;        
        // Range of sin function - 1 cycle
        const float tau = Mathf.PI * 2;
        // Number of cycles * tau = radians
        float rawSinWave = Mathf.Sin(cycles * tau);
        // Sin goes from -1 to 1, convert to 0 to 1
        movementProgress = (rawSinWave + 1) / 2;
        Debug.Log(movementProgress);

        Oscillate();
        if (movementProgress < 0.005f) PlayThud();
    }

    void Oscillate()
    {
        Vector3 tran = (maxMovementRange * movementProgress);
        doorPos.position = startingPosition + tran;
    }

    void PlayThud()
    {
        if (!audioSource.isPlaying) audioSource.PlayOneShot(thudAudio);
        Invoke("StopThud", 1f);
    }

    void StopThud()
    {
        audioSource.Stop();
    }
}
