using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotatesScr : MonoBehaviour
{
    public float tiltMultiplier;

    void Update()
    { 
        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal") * -tiltMultiplier, 0, Input.GetAxisRaw("Vertical") * -tiltMultiplier);
        transform.rotation = Quaternion.Euler(input); 
    }
}
