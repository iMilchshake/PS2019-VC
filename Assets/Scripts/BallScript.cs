using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    [Header("References")]
    public Rigidbody rb;
    [Header("Movement Settings")]
    public float acceleration = 1;
    public float friction = 1; //not implemented yet
    [Header("Debugging")]
    public Vector2 velocity;

    void Update()
    {
        Movement();
    }

    void Movement()
    {
        velocity += new Vector2(Input.GetAxis("Horizontal") * acceleration, Input.GetAxis("Vertical") * acceleration); //Add Acceleration to current Velocity
        transform.localPosition += new Vector3(velocity.x,0,velocity.y) * Time.deltaTime; //Apply Velocity to Position
    }
}
