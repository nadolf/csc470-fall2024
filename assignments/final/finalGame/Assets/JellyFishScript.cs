using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyFishScript : MonoBehaviour
{
    public GameObject jellyFish; // The GameObject representing the jellyfish
    float floatStrength = 0.2f; // Strength of the floating effect
    float moveSpeed = 2f; // Speed of random movement
    float movementRange = 2f; // Range of random movement

    private Vector3 initialPosition; // To store the initial position of the jellyfish
    private float floatTimer; // Timer for the floating effect

    void Start()
    {
        initialPosition = jellyFish.transform.position;
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
        jellyFish.transform.position = new Vector3(initialPosition.x, initialPosition.y + floatY, initialPosition.z);
    }

    void MoveObjectRandomly()
    {
        float randomX = Random.Range(-movementRange, movementRange) * Time.deltaTime * moveSpeed;
        float randomY = Random.Range(-movementRange, movementRange) * Time.deltaTime * moveSpeed;
        float randomZ = Random.Range(-movementRange, movementRange) * Time.deltaTime * moveSpeed;
        jellyFish.transform.Translate(randomX, randomY, randomZ, Space.World);
    }
}
