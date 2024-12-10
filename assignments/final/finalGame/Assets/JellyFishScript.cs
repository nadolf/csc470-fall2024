using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyFishScript : MonoBehaviour
{
    public GameObject jellyFish;
    float floatStrength = 0.5f; 
    float moveSpeed = 0.5f;
    float movementRange = 3f;

    private Vector3 initialPosition; 
    private float floatTimer;
    private Vector3 randomOffset;

    void Start()
    {
        initialPosition = jellyFish.transform.position;
        randomOffset = new Vector3(Random.Range(0f, 100f), Random.Range(0f, 100f), Random.Range(0f, 100f));
    }

    void Update()
    {
        FloatEffect();
        MoveObjectRandomly();
    }

    void FloatEffect()
    {
        floatTimer += Time.deltaTime;
        float floatY = Mathf.Sin(floatTimer) * floatStrength;
        jellyFish.transform.position = new Vector3(
            jellyFish.transform.position.x,
            initialPosition.y + floatY,
            jellyFish.transform.position.z
        );
    }

    void MoveObjectRandomly()
    {
        float randomX = (Mathf.PerlinNoise(Time.time * moveSpeed, randomOffset.x) - 0.5f) * movementRange;
        float randomZ = (Mathf.PerlinNoise(Time.time * moveSpeed, randomOffset.z) - 0.5f) * movementRange;

        jellyFish.transform.position = new Vector3(
            initialPosition.x + randomX,
            jellyFish.transform.position.y,
            initialPosition.z + randomZ
        );
    }
}
