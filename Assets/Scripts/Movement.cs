using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    Rigidbody rb;
    [SerializeField] float rocketThrustForce = 720f;
    [SerializeField] float rotationForce = 100f;
    [SerializeField] float worldGravity = 9.81f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Physics.gravity = worldGravity * Vector3.down;
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
            rb.AddRelativeForce(Vector3.up * rocketThrustForce * Time.deltaTime);
        } 
    }

    // Processing rotation of the rocket using AD / arrow keys
    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            ApplyRotation(Vector3.forward);
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            ApplyRotation(Vector3.back);
        }
    }

    private void ApplyRotation(Vector3 vec)
    {
        // Freezing rotation so we can manually override the rotation
        rb.freezeRotation = true; 
        rb.transform.Rotate(vec * rotationForce * Time.deltaTime);
        // Unfreezing rotation of physics system
        rb.freezeRotation = false;
    }
}
