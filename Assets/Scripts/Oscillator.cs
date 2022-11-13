using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    [SerializeField] Vector3 maxMovementRange;
    [SerializeField] [Range (0,1)] float movementProgress;
    [SerializeField] AudioClip thudAudio;
    
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
        Oscillate();
        if (movementProgress == 1) PlayThud();
    }

    void Oscillate()
    {
        doorPos.position = startingPosition + (maxMovementRange * movementProgress);
    }

    void PlayThud()
    {
        audioSource.PlayOneShot(thudAudio);
        Invoke("StopThud", 1f);
    }

    void StopThud()
    {
        audioSource.Stop();
    }
}
