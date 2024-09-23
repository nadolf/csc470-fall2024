using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlaneScript : MonoBehaviour
{
    private bool gameOver = false;

    public TMP_Text scoreText;
    public TMP_Text timerText;
    public TMP_Text resultText;

    float forwardSpeed = 10f;
    float xRotationSpeed = 100f;
    float yRotationSpeed = 100f;


    private float timeRemaining;

    public GameObject cameraObject;

    float boostSpeed = 2f;
    float boostLength = 1f;
    float boostTimeRemaining = 0f;

    int score = 0;


    private Vector3 startingPosition;
    private Quaternion startingRotation;

    void Start()
    {
        startingPosition = transform.position;
        startingRotation = transform.rotation;

        scoreText.text = "Score: " + score;
        timeRemaining = 60f;
        timerText.text = "Time: " + timeRemaining;
        resultText.text = "Collect all 8 before time runs out to win.";
    }

    void Update()
    {
        if (gameOver)
        {
            return;  // Stops the game when its over
        }

        // Timer
        timeRemaining -= Time.deltaTime;
        timerText.text = "Time: " + Mathf.RoundToInt(timeRemaining);

        if (timeRemaining <= 0)
        {
            GameOver();  // End game when timer runs out
        }

        float vAxis = Input.GetAxis("Vertical");
        float hAxis = Input.GetAxis("Horizontal");

        Vector3 amountToRotate = new Vector3(vAxis * xRotationSpeed, hAxis * yRotationSpeed, 0) * Time.deltaTime;
        transform.Rotate(amountToRotate, Space.Self);

        float currentSpeed = forwardSpeed;
        if (boostTimeRemaining > 0)
        {
            currentSpeed *= boostSpeed;
            boostTimeRemaining -= Time.deltaTime;
        }

        transform.position += transform.forward * currentSpeed * Time.deltaTime;

        Vector3 cameraPosition = transform.position + (-transform.forward * 10f) + (Vector3.up * 8f);
        cameraObject.transform.position = cameraPosition;
        cameraObject.transform.LookAt(transform.position);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent != null && other.transform.parent.name == "Gas Containers")
        {
            Destroy(other.gameObject);
            score++;
            scoreText.text = "Score: " + score;

            boostTimeRemaining = boostLength;

            if (score == 8)
            {
                WinGame();
            }
        }
    }

    void WinGame()
    {
        resultText.text = "You Win!";
        gameOver = true;  // Stop the game once the player wins
    }

    void GameOver()
    {
        resultText.text = "You Lose!";
        gameOver = true;  // Stop the game once time runs out
        Destroy(gameObject);  // Destroy the player object
    }
}
