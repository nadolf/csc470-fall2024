using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Camera mainCamera;
    public GameObject garbage;
    public UnitScript selectedUnit;
    public List<UnitScript> units = new List<UnitScript>();
    public GameObject popUpWindow;
    public TMP_Text progressText;
    public TMP_Text spawnSpeedText;
    public TMP_Text timerText; 
    private int initialUnits;
    private int destroyedUnits = 0;
    private int totalSpawnedUnits = 0; 

    LayerMask layerMask;

    private Collider garbageCollider;

    public GameObject throwerCharacter;
    public GameObject unitPrefab; 
    public float spawnRange = 5f;
    public float minSpawnTime = 15f; 
    public float maxSpawnTime = 20f;

    private float lastSpawnTime;
    private float spawnInterval;
    private bool gameEnded = false; 
    private float timeLimit = 90f; 
    private float remainingTime;
    void OnEnable()
    {
        if (GameManager.instance == null)
        {
            GameManager.instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    void Start()
    {
        layerMask = LayerMask.GetMask("ground");
        garbageCollider = garbage.GetComponent<Collider>();

        initialUnits = units.Count;
        totalSpawnedUnits = initialUnits; 
        UpdateProgressText();

        lastSpawnTime = Time.time; 
        remainingTime = timeLimit; 
        StartCoroutine(SpawnUnitAutomatically());
    }

    void Update()
    {
        if (gameEnded) return;

        if (Input.GetMouseButtonDown(0))
        {
            Ray mousePositionRay = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(mousePositionRay, out hitInfo, Mathf.Infinity, layerMask))
            {
                if (selectedUnit != null)
                {
                    selectedUnit.nma.SetDestination(hitInfo.point);
                }
            }
        }

        CheckGarbageArea();

        UpdateTimer();
    }

    private void CheckGarbageArea()
    {
        List<UnitScript> unitsToRemove = new List<UnitScript>();

        foreach (var unit in units)
        {
            if (garbageCollider.bounds.Contains(unit.transform.position))
            {
                unitsToRemove.Add(unit);
            }
        }

        foreach (var unit in unitsToRemove)
        {
            DestroyUnit(unit);
        }

        if (destroyedUnits == totalSpawnedUnits || CalculateCleanPercentage() >= 100f)
        {
            GameOver(true);
        }
    }

    private void DestroyUnit(UnitScript unit)
    {
        units.Remove(unit);
        Destroy(unit.gameObject); 
        destroyedUnits++;

        UpdateProgressText();
    }

    private void UpdateProgressText()
    {
        float progressPercentage = CalculateCleanPercentage();
        progressText.text = $"Clean: {progressPercentage:F0}%";
    }

    private float CalculateCleanPercentage()
    {
        return ((float)destroyedUnits / totalSpawnedUnits) * 100f;
    }

    public void SelectUnit(UnitScript unit)
    {
        foreach (UnitScript u in units)
        {
            u.selected = false;
        }
        selectedUnit = unit;
        unit.selected = true;
    }

    public void ClosePopUpWindow()
    {
        popUpWindow.SetActive(false);
    }

    IEnumerator SpawnUnitAutomatically()
    {
        while (!gameEnded)
        {
            float waitTime = Random.Range(minSpawnTime, maxSpawnTime);
            yield return new WaitForSeconds(waitTime);

            SpawnRandomUnit(); 
        }
    }

    void SpawnRandomUnit()
    {
        if (unitPrefab == null) return;

        Vector3 spawnPosition = throwerCharacter.transform.position + new Vector3(
            Random.Range(-spawnRange, spawnRange),
            0,
            Random.Range(-spawnRange, spawnRange)
        );

        GameObject newUnit = Instantiate(unitPrefab, spawnPosition, Quaternion.identity);

        UnitScript unitScript = newUnit.GetComponent<UnitScript>();

        Rigidbody unitRigidbody = newUnit.GetComponent<Rigidbody>();

        if (unitRigidbody != null)
        {
            Vector3 throwDirection = (spawnPosition - throwerCharacter.transform.position).normalized;
            float throwForce = 10f;

            unitRigidbody.AddForce(throwDirection * throwForce, ForceMode.VelocityChange);
        }

        if (unitScript != null)
        {
            unitScript.unitName = "Unit " + Random.Range(1, 1000).ToString();
        }

        units.Add(unitScript);

        totalSpawnedUnits++;

        UpdateProgressText();
    }

    private void UpdateTimer()
    {
        if (gameEnded) return;
        remainingTime -= Time.deltaTime;

        timerText.text = $"Time Left: {remainingTime:F0}s";

        // Check if time has run out
        if (remainingTime <= 0f)
        {
            GameOver(false);
        }
    }

    private void GameOver(bool allDestroyed)
    {
        gameEnded = true;

        if (allDestroyed)
        {
            spawnSpeedText.text = "Woahooo! The Park is all clean :) !";
        }
        else
        {
            spawnSpeedText.text = "Better Luck Next Time! Time's up!";
        }
    }
}
