using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapCameraScript : MonoBehaviour
{
        public CharacterController character;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(character.transform.position.x, 40, character.transform.position.z);
    }
}
