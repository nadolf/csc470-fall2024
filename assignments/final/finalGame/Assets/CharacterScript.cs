using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterScript : MonoBehaviour
{
    public GameObject character;
    public GameObject cameraObject;
    public Animator animator;
    public AudioSource footstepAudio;

    float movementSpeed = 3f;
    float rotateSpeed = 50f;
    float jumpVelocity = 4f;
    float gravity = -9.8f;
    float verticalVelocity = 0f;
    bool isGrounded = true;

    // Energy
    float energy = 100f;
    float drainRate = 1f;

    // Score
    int jellyfishScore = 0;

    void Start()
    {
        animator.SetBool("IsJumping", false);
        animator.SetBool("IsRunning", false);
        animator.SetBool("IsRunningBackwards", false);
    }


    void Update()
    {
        if (energy > 0)
        {
            energy -= drainRate * Time.deltaTime;
            energy = Mathf.Clamp(energy, 0, 100);
        }

        movementSpeed = 3f * (energy / 100f);
        HandleMovement();
        HandleJumping();
        HandleCamera();

        // If energy reaches 0, stop movement
        if (energy <= 0)
        {
            movementSpeed = 0f;
        }
    }

    void HandleMovement()
    {
        if (energy > 0)
        {
            Vector3 moveDirection = Vector3.zero;
            bool isMoving = false;

            // Move forward
            if (Input.GetKey(KeyCode.UpArrow))
            {
                moveDirection += transform.forward;
                animator.SetBool("IsRunning", true);
                animator.SetBool("IsRunningBackwards", false);
                isMoving = true;
            }
            // Move backward
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                moveDirection += -transform.forward;
                animator.SetBool("IsRunningBackwards", true);
                animator.SetBool("IsRunning", false);
                isMoving = true;
            }
            else
            {
                animator.SetBool("IsRunning", false);
                animator.SetBool("IsRunningBackwards", false);
            }

            // Rotate left or right
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                transform.Rotate(0, -rotateSpeed * Time.deltaTime, 0);
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);
            }
            transform.position += moveDirection * movementSpeed * Time.deltaTime;

            // Sound effect when Walking

            if (isMoving && isGrounded && !footstepAudio.isPlaying)
            {
                footstepAudio.Play();
            }
            else if (!isMoving || !isGrounded)
            {
                footstepAudio.Stop();
            }
        }
    }

    void HandleJumping()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            verticalVelocity = jumpVelocity;
            isGrounded = false;
            animator.SetBool("IsJumping", true); // Play jumping animation
            footstepAudio.Stop();
        }
        if (!isGrounded)
        {
            verticalVelocity += gravity * Time.deltaTime;
        }
        transform.position += Vector3.up * verticalVelocity * Time.deltaTime;

        if (transform.position.y <= 0)
        {
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
            verticalVelocity = 0;
            isGrounded = true;
            animator.SetBool("IsJumping", false);
        }
    }


    void HandleCamera()
    {
        Vector3 cameraPosition = transform.position;
        cameraPosition += -transform.forward * 3f;
        cameraPosition += Vector3.up * 1.5f;
        cameraObject.transform.position = Vector3.Lerp(cameraObject.transform.position, cameraPosition, 5f * Time.deltaTime);
        cameraObject.transform.LookAt(transform.position + Vector3.up * 1f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("jellyfish"))
        {
            jellyfishScore++;
            Debug.Log("Score: " + jellyfishScore);
            Destroy(other.gameObject);

            GameManager.instance.CollectJellyfish();
        }

        if (other.gameObject.CompareTag("KrabbyPatty"))
        {
            energy += 10f;
            energy = Mathf.Clamp(energy, 0, 100);
            Debug.Log("Energy restored! Current energy: " + Mathf.FloorToInt(energy));
            Destroy(other.gameObject);

            GameManager.instance.CollectKrabbyPatty();
        }
    }
}
