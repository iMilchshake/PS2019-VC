using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class CheckpointScript : MonoBehaviour
{

    public Stack<int> reachedCheckpoints;
    AudioSource audioData;

    void Start()
    {
        reachedCheckpoints = new Stack<int>();
        audioData = GetComponent<AudioSource>();
    }

    void Update()
    {
        
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
                if(reachedCheckpoints.Peek() < checkpointNumber) //a larger checkpoint was hit
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
            }
        }

    }
}
