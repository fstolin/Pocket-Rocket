using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    [SerializeField] float rocketThrustForce = 720f;
    [SerializeField] float rotationForce = 100f;
    [SerializeField] float worldGravity = 9.81f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem mainThruster;
    [SerializeField] ParticleSystem sideThrusterLeft;
    [SerializeField] ParticleSystem sideThrusterRight;

    Rigidbody rb;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

        Physics.gravity = worldGravity * Vector3.down;
        audioSource.PlayOneShot(mainEngine);
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    // Processing the (vertical) thrust of the rocket
    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }

    // Processing rotation of the rocket using AD / arrow keys
    void ProcessRotation()
    {
        // Rotating boolean for particle purposes - did we rotate this frame?
        bool rotating = false;

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            RotateLeft();
            rotating = true;
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            RotateRight();
            rotating = true;
        }

        if (!rotating) DisableSideParticles();
    }

    private void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * rocketThrustForce * Time.deltaTime);
        playEngineSound();
        HandleRocketParticles(mainThruster);
    }

    private void StopThrusting()
    {
        stopEngineSound();
        mainThruster.Stop();
    }

    private void RotateLeft()
    {
        ApplyRotation(Vector3.forward);
        HandleRocketParticles(sideThrusterLeft);
    }

    private void RotateRight()
    {
        ApplyRotation(Vector3.back);
        HandleRocketParticles(sideThrusterRight);
    }

    private void ApplyRotation(Vector3 vec)
    {
        // Freezing rotation so we can manually override the rotation
        rb.freezeRotation = true;
        rb.transform.Rotate(vec * rotationForce * Time.deltaTime);
        // Unfreezing rotation of physics system
        rb.freezeRotation = false;
    }

    private void playEngineSound()
    {
        if (!audioSource.isPlaying) audioSource.PlayOneShot(mainEngine);
    }

    private void stopEngineSound()
    {
        audioSource.Stop();
    }

    private void HandleRocketParticles(ParticleSystem ps)
    {
        if (!ps.isEmitting) ps.Play();
    }

    private void DisableSideParticles()
    {
        sideThrusterLeft.Stop();
        sideThrusterRight.Stop();
    }
}
