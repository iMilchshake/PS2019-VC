using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    [Header("References")]
    public Rigidbody rb;
    public Transform respawnLocation;
    [Header("Movement Settings")]
    public float acceleration = 1;
    public float friction = 1;
    [Header("Hotkey Settings")]
    public KeyCode respawnKey;
    [Header("Debugging")]
    public Vector3 velocity;

    void Update()
    {
        KeyInputs();
        Movement();
        checkErrors();
    }

    void Movement()
    {
        Vector3 input = new Vector3(Input.GetAxisRaw("Vertical") * acceleration, 0, -Input.GetAxisRaw("Horizontal") * acceleration); //Get User Input
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

    void KeyInputs()
    {
        if(Input.GetKeyDown(respawnKey))
        {
            Respawn();
        }
    }

    void Respawn()
    {
        transform.position = respawnLocation.position; //reset position
        rb.velocity = Vector3.zero; //reset velocity
        CheckpointScript tmp = GetComponent<CheckpointScript>();
        tmp.reachedCheckpoints.Clear(); //reset reached checkpoints
    }
}
