using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformScript : MonoBehaviour
{
    float moveSpeed = 1;
    float freq = 1;
    float amp = 1;
    float offset;

    Vector3 startPosition;
    void Start()
    {
        startPosition = transform.position;
        offset = Random.Range(0, Mathf.PI * 2);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = startPosition + Vector3.up * Mathf.Sin((offset + Time.time) * freq) * amp;
        transform.position = pos;
    }
}
