using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitScript : MonoBehaviour
{
    public string unitName;
    public NavMeshAgent nma;
    public Vector3 destination;
    public bool selected = false;
    float rotateSpeed;
    void Start()
    {
        GameManager.instance.units.Add(this);
        transform.Rotate(0, Random.Range(0,360), 0);
        rotateSpeed = Random.Range(20,60);

    }

    void OnDestroy() 
    {
        GameManager.instance.units.Remove(this);
        
    }

    void Update()
    {
        // transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);        
    }

    void OnMouseDown() {
        GameManager.instance.SelectUnit(this);
    }
}
