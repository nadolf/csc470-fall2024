using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlatformerController : MonoBehaviour
{
    public CharacterController cc;
    private Animator animator;
    public GameObject cameraObject;
    public TMP_Text directionsText;
    public TMP_Text timerText;
    public TMP_Text counterText;

    float rotateSpeed = 100;
    float moveSpeed = 10;
    float yVelocity = 0;
    float jumpVelocity = 8;
    float gravity = -12f;
    int score = 0;
    public float timeRemaining;
    public Vector3 cameraOffset = new Vector3(0, 2, -5);

    private int jumpCount = 0;
    private int maxJumps = 3;
    private float[] jumpPowers;

    void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(ChangeTextAfterDelay());
        timeRemaining = 38f;
        timerText.text = "Time: " + timeRemaining;
        counterText.text = "Donuts: " + score;

        jumpPowers = new float[maxJumps];
        for (int i = 0; i < maxJumps; i++)
        {
            jumpPowers[i] = jumpVelocity * (1f - i * 0.1f);
        }
    }

    IEnumerator ChangeTextAfterDelay()
    {
        // Wait for 3 seconds
        yield return new WaitForSeconds(3f);

        // Change the text to "Hi"
        if (directionsText != null)
        {
            directionsText.text = "Your first task is to collect all 6 donuts!";
        }

        yield return new WaitForSeconds(3f);
        if (directionsText != null)
        {
            directionsText.text = "Your 30 Seconds Starts now!";
        }

        yield return new WaitForSeconds(2f);
        if (directionsText != null)
        {
            directionsText.text = "";
        }

    }

    void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            timeRemaining = Mathf.Max(timeRemaining, 0);
        }
        timerText.text = "Time: " + Mathf.RoundToInt(timeRemaining);

        if (score >= 6)
        {
            WinGame();
        }
        else if (score <= 6 && timeRemaining <= 0)
        {
            LooseGame();
        }

        float hAxis = Input.GetAxis("Horizontal");
        float vAxis = Input.GetAxis("Vertical");

        transform.Rotate(0, rotateSpeed * hAxis * Time.deltaTime, 0);
        Vector3 amountToMove = transform.forward * moveSpeed * vAxis;

        if (!cc.isGrounded)
        {
            yVelocity += gravity * Time.deltaTime;
        }
        else
        {
            yVelocity = -2;
            jumpCount = 0;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                yVelocity = jumpPowers[0];
                jumpCount++;
            }
        }

        if (!cc.isGrounded && jumpCount < maxJumps)
        {
            if (Input.GetKeyDown(KeyCode.Space) && yVelocity <= 0)
            {
                yVelocity = jumpPowers[jumpCount];
                jumpCount++;
            }
        }

        bool isRunning = vAxis > 0;
        bool isRunningBackwards = vAxis < 0;

        animator.SetBool("IsRunning", isRunning);
        animator.SetBool("IsRunningBackwards", isRunningBackwards);

        if (vAxis == 0)
        {
            animator.SetBool("IsRunning", false);
            animator.SetBool("IsRunningBackwards", false);
        }

        amountToMove.y += yVelocity;
        amountToMove *= Time.deltaTime;

        if (cc != null)
        {
            cc.Move(amountToMove);
        }

        // Camera positioning
        if (cameraObject != null)
        {
            Vector3 desiredCameraPosition = transform.position + transform.rotation * cameraOffset;
            cameraObject.transform.position = desiredCameraPosition;
            cameraObject.transform.LookAt(transform.position + Vector3.up * 1.5f);
        }
    }


    public void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent != null && other.transform.parent.name == "Donuts" && other.name != "platform1 (2)")
        {
            Destroy(other.gameObject);
            score++;
            counterText.text = "Donuts: " + score;
        }
    }

    void WinGame()
    {
        if (directionsText != null)
        {
            directionsText.text = "Great Job Rookie, you'll make a fine officer :)!";
        }

        animator.SetBool("IfWin", true);
        moveSpeed = 0;
    }

    void LooseGame()
    {
        if (directionsText != null)
        {
            directionsText.text = "Looks like your last day here Rookie :(";
        }

        animator.SetBool("IfLose", true);
        moveSpeed = 0;
    }
}