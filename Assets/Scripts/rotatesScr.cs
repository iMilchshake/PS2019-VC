using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotatesScr : MonoBehaviour
{
    public float tiltMultiplier;

    void Update()
    {
        transform.rotation = Quaternion.Euler(Input.GetAxis("Horizontal") * -tiltMultiplier, 0, Input.GetAxis("Vertical")* -tiltMultiplier); 
    }
}
