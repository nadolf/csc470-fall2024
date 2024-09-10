using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class SphereScript : MonoBehaviour
{
    public  scoreText;
    // public Ridgidbody rb

    int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        // if (Input.GetKeyUp(KeyCode.Space)) 
        // {

        // }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other) {
        // Destory the coin
        Destroy (other.gameObject);
        score++ ;
        scoreText.text = "Score: " + score;
        
    }

    // public void OnCollisionEnter(Collision col) {
    //     Debug.Log("Hit the ground");
    // }
}
