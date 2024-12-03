using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KrabbyPattyScript : MonoBehaviour
{
    float rotationSpeed = 50f; // Speed of rotation
    float floatSpeed = 2f;    // Speed of up-down motion
    float floatAmplitude = 0.1f; // Height of up-down motion

    private Vector3 initialPosition;

    void Start() {
        initialPosition = transform.position;
    }

    void Update()
    {
        // Rotate around the Y-axis
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);

        // Float up and down
        float floatOffset = Mathf.Sin(Time.time * floatSpeed) * floatAmplitude;
        transform.position = initialPosition + new Vector3(0, floatOffset, 0);
    }
}