using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneScript : MonoBehaviour
{
    float forwardSpeed = 0.01f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float vAxis = Input.GetAxis("Vertical");
        float hAxis = Input.GetAxis("Horixontal");
        transform.Rotate(vAxis, hAxis , 0, Space.Self);
        transform.position += transform.forward * forwardSpeed; //Move forward

    }
}
