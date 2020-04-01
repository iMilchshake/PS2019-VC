using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    [Header("References")]
    public Rigidbody rb;
    public Transform respawnLocation;
    public Text debugText;
    public GameObject lightMain;
    public GameObject FinishScreen;
    public Text FinishText;
    public CheckpointScript checkPscr;
    [Header("Movement Settings")]
    public float desktopAcceleration = 1;
    public float mobileAcceleration = 1;
    public float lightTurnMultiplier = 1;
    public float friction = 1;
    [Header("Hotkey Settings")]
    public KeyCode respawnKey;
    public KeyCode abortKey;
    [Header("Debugging")]
    public Vector3 velocity;
    public Gyroscope myGyro;
    public Vector3 input;
    public Quaternion defaultLightRotation;
    public float timeSinceRespawn;
    public float timeSinceStart;
    public float maxReachedCheckpoints;
    public int respawns;

    public void EnableFinishScreen()
    {
        CheckpointScript checkPscr = GetComponent<CheckpointScript>();

        FinishScreen.SetActive(true);
        FinishText.text =
            "Time to complete level: " + timeSinceRespawn.ToString("0.00") + " seconds\n" +
            "Overall playtime: " + timeSinceStart.ToString("0.00") + " seconds\n" +
            "Reached checkpoints: " + Mathf.Max(maxReachedCheckpoints, checkPscr.reachedCheckpoints.Count) + "/" + "36 \n" +
            "Respawns: " + respawns + "\n" +
            "\n" +
            "Thank you for playing!";

        mobileAcceleration = 0;
        desktopAcceleration = 0;
    }

    void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 120;
        myGyro = Input.gyro;
        myGyro.enabled = true; //enable Gyro
        defaultLightRotation = lightMain.transform.rotation;
        checkPscr = GetComponent<CheckpointScript>(); //get reference to the checkpointScript
    }

    void Update()
    {
        UserInputs();
        NewMovement();
        timeSinceRespawn += Time.deltaTime;
        timeSinceStart += Time.deltaTime;
        //checkErrors();
        //Light();

        if (Time.frameCount % 10 == 0)
            debugText.text = (1 / Time.deltaTime).ToString("0.00") + "\n" + timeSinceRespawn.ToString("0.0");

        if (transform.position.y < -10f)
        {
            Respawn();
        }
    }

    void Light()
    {
        Vector3 rotated_input = new Vector3(-input.z, 0, input.x);
        lightMain.transform.rotation = defaultLightRotation;
        lightMain.transform.Rotate((lightMain.transform.InverseTransformDirection(rotated_input) / mobileAcceleration) * lightTurnMultiplier);
    }

    void NewMovement()
    {
        rb.AddForce(input,ForceMode.VelocityChange);
    }

    void Movement()
    {
        velocity = transform.InverseTransformDirection(rb.velocity); //In case rb.velocity changed due to collisions, apply changes to velocity aswell
        velocity += input; //Add Acceleration to current Velocity
        velocity = Vector3.MoveTowards(velocity, Vector3.zero, friction); //Apply Friction
        rb.velocity = transform.TransformDirection(velocity);
    }

    void checkErrors()
    {
        if (friction >= desktopAcceleration)
            Debug.LogError("Acceleration ("+ desktopAcceleration + ") should be larger than friction ("+ friction + ") !");
    }

    void UserInputs()
    {
        if (SystemInfo.deviceType == DeviceType.Desktop)
        {
            input = new Vector3(Input.GetAxisRaw("Vertical"), 0, -Input.GetAxisRaw("Horizontal")); //Get Desktop User Input
            input *= desktopAcceleration;
        }
        else
        {
            input = new Vector3(Input.acceleration.y, 0, -Input.acceleration.x); //Get Mobile User Input
            input *= mobileAcceleration;
        }

        if (Input.GetKeyDown(respawnKey))
        {
            Respawn();
        }

        if(Input.GetKeyDown(abortKey))
        {
            EnableFinishScreen();
        }
    }

    public void Respawn()
    {
        transform.position = respawnLocation.position; //reset ball position
        rb.velocity = Vector3.zero; //reset ball velocity
        maxReachedCheckpoints = Mathf.Max(maxReachedCheckpoints, checkPscr.reachedCheckpoints.Count); //update maximum reached checkpoints
        checkPscr.reachedCheckpoints.Clear(); //reset reached checkpoints
        timeSinceRespawn = 0f; //reset Timer
        respawns += 1;
    }

    private static Quaternion GyroToUnity(Quaternion q)
    {
        return new Quaternion(q.x, q.y, -q.z, -q.w);
    }
}
