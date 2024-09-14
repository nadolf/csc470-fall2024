using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereScript : MonoBehaviour
{
    public Rigidbody rb;
    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.useGravity = true;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        
        Destroy(other.gameObject);

    }

}