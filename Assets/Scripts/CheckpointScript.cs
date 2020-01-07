using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class CheckpointScript : MonoBehaviour
{

    public Stack<int> reachedCheckpoints;
    public GameObject nextCheckpoint;
    public GameObject CheckpointCursor;
    AudioSource audioData;

    void Start()
    {
        reachedCheckpoints = new Stack<int>();
        audioData = GetComponent<AudioSource>();
        nextCheckpoint = findNextCheckpoint();
        CheckpointCursor.transform.position = nextCheckpoint.transform.position;
    }

    void Update()
    {
        
    }

    public GameObject findNextCheckpoint()
    {
        GameObject next;
        if (reachedCheckpoints.Count == 0)
        {
            next = GameObject.Find("1"); //First!
        }
        else
        {
            try
            {
                next = GameObject.Find((reachedCheckpoints.Peek() + 1).ToString()); //Find next checkpoint
            } catch(System.Exception e)
            {
                next = null;
            }
        }

        return next;     
    }

    private void OnTriggerStay(Collider other)
    {
        bool newCheckpointAdded = false;

        int checkpointNumber = -1;
        int.TryParse(other.gameObject.name, out checkpointNumber);

        if(checkpointNumber != -1) //hit a checkpoint
        {
            if(reachedCheckpoints.Count==0) //first checkpoint
            {
                reachedCheckpoints.Push(checkpointNumber);
                newCheckpointAdded = true;
            }
            else 
            {
                if(reachedCheckpoints.Peek()+1 == checkpointNumber) //the next checkpoint was hit
                {
                    reachedCheckpoints.Push(checkpointNumber);
                    newCheckpointAdded = true;
                }
            }

            if(newCheckpointAdded) 
            {
                audioData.pitch = ((float)checkpointNumber / 36) + 1;
                audioData.Play(0);
                Debug.Log(checkpointNumber);
                nextCheckpoint = findNextCheckpoint();
                if(nextCheckpoint!=null)
                    CheckpointCursor.transform.position = nextCheckpoint.transform.position;
            }
        }

    }
}
