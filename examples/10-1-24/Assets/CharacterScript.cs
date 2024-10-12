using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CharacterScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject character;
    void Start()
    {
        Vector3 startPosition = transform.position;
        for (int i = 0; i < 5; i++) {
            Vector3 position = startPosition + transform.forward * i;
            position += transform.forward * i;

            GameObject newCharacter = Instantiate(character, position, Quaternion.identity);
            Renderer rend = newCharacter.GetComponentInChildren<Renderer>();

        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
