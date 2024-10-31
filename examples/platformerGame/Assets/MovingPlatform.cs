using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    float moveSpeed = 0.5f;
    float moveDistance = 5f; // The distance the platform moves from its starting position
    private Vector3 startPosition;

    void Start()
    {
        // Save the starting position of the platform
        startPosition = transform.position;
    }

    void Update()
    {
        // Calculate the new position using a sine wave for smooth side-to-side movement
        float offset = Mathf.Sin(Time.time * moveSpeed) * moveDistance;
        transform.position = startPosition + new Vector3(offset, 0, 0); // Move along the x-axis
    }
}