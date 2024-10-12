using System.Collections;
using UnityEngine;

public class CellScript : MonoBehaviour
{
    public Renderer cubeRenderer;
    public enum CellState { Dirt, Grass, Plant }
    public CellState currentState;

    public int xIndex = -1;
    public int yIndex = -1;

    public Material dirtMaterial;
    public Material grassMaterial;
    public Material plantMaterial;

    private GameManager gameManager;
    public GameObject plantPrefab;

    private GameObject instantiatedPlant;
    private CellState previousState;
    private Coroutine revertCoroutine;

    void Start()
    {
        SetMaterial();
        GameObject gmObj = GameObject.Find("GameManagerObject");
        if (gmObj != null)
        {
            gameManager = gmObj.GetComponent<GameManager>();
        }
    }

    void Update()
    {
    }

    void OnMouseDown()
    {
        if (!gameManager.isGameOver) {
        previousState = currentState;
        UpdateState();
        SetMaterial();

        int neighborCount = gameManager.CountNeighbors(xIndex, yIndex);
        gameManager.CheckIfWin();

        if (revertCoroutine != null)
        {
            StopCoroutine(revertCoroutine);
        }
        revertCoroutine = StartCoroutine(RevertStateAfterDelay(15f));
        }
    }

    public void SetMaterial()
    {
        switch (currentState)
        {
            case CellState.Dirt:
                cubeRenderer.material = dirtMaterial;
                DestroyPlant();
                break;
            case CellState.Grass:
                cubeRenderer.material = grassMaterial;
                DestroyPlant();
                break;
            case CellState.Plant:
                cubeRenderer.material = plantMaterial;
                CreatePlant();
                break;
        }
    }

    void UpdateState()
    {
        if (currentState == CellState.Dirt)
        {
            currentState = CellState.Grass;
        }
        else if (currentState == CellState.Grass)
        {
            currentState = CellState.Plant;
        }
        else
        {
            currentState = CellState.Dirt;
        }
    }

    private IEnumerator RevertStateAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (gameManager.isGameOver){
            yield break;
        }
        currentState = previousState;
        SetMaterial();
    }

    void CreatePlant()
    {
        if (instantiatedPlant == null)
        {
            instantiatedPlant = Instantiate(plantPrefab, transform.position + Vector3.up, Quaternion.identity);
            instantiatedPlant.transform.SetParent(this.transform);
        }
    }

    void DestroyPlant()
    {
        if (instantiatedPlant != null)
        {
            Destroy(instantiatedPlant);
            instantiatedPlant = null;
        }
    }
}