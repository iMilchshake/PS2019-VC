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
    public Vector2 velocity;

    void Update()
    {
        Movement();
        checkErrors();
    }

    void Movement()
    {
        velocity += new Vector2(Input.GetAxis("Horizontal") * acceleration, Input.GetAxis("Vertical") * acceleration); //Add Acceleration to current Velocity
        velocity = Vector3.MoveTowards(velocity, Vector3.zero, friction); //Apply Friction
        transform.localPosition += new Vector3(velocity.x,0,velocity.y) * Time.deltaTime; //Add Velocity to Position
    }

    void checkErrors()
    {
        if (friction >= acceleration)
            Debug.LogError("Acceleration ("+ acceleration + ") should be larger than friction ("+ friction + ") !");
    }
}
