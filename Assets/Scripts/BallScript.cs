using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    [Header("References")]
    public Rigidbody rb;
    [Header("Movement Settings")]
    public float acceleration = 1;
    public float friction = 1;
    [Header("Debugging")]
    public Vector3 velocity;

    void Update()
    {
        Movement();
        checkErrors();
    }

    void Movement()
    {
        Vector3 input = new Vector3(Input.GetAxis("Horizontal") * acceleration, 0, Input.GetAxis("Vertical") * acceleration); //Get User Input
        velocity = transform.InverseTransformDirection(rb.velocity); //In case rb.velocity changed due to collisions, apply changes to velocity aswell
        velocity += input; //Add Acceleration to current Velocity
        velocity = Vector3.MoveTowards(velocity, Vector3.zero, friction); //Apply Friction
        rb.velocity = transform.TransformDirection(velocity);
    }

    void checkErrors()
    {
        if (friction >= acceleration)
            Debug.LogError("Acceleration ("+ acceleration + ") should be larger than friction ("+ friction + ") !");
    }
}
