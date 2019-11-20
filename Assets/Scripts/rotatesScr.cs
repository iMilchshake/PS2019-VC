using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotatesScr : MonoBehaviour
{
    public float mp;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(Input.GetAxis("Vertical")* mp, 0, Input.GetAxis("Horizontal")* -mp);
    }
}
