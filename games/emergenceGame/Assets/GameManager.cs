using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject cellPrefab;
    public TMP_Text gameText;
    public TMP_Text timerText; 

    CellScript[,] grid;
    float spacing = 1.1f;

    public bool isGameOver = false; 
    public float gameTime = 20f;

    void Start()
    {
        grid = new CellScript[5, 5];
        for (int x = 0; x < 5; x++)
        {
            for (int y = 0; y < 5; y++)
            {
                Vector3 pos = transform.position;
                pos.x += x * spacing;
                pos.z += y * spacing;
                GameObject cell = Instantiate(cellPrefab, pos, Quaternion.identity);
                
                CellScript cellScript = cell.GetComponent<CellScript>();
                grid[x, y] = cellScript;
                
                cellScript.xIndex = x;
                cellScript.yIndex = y;

                float randomValue = Random.value;
                if (randomValue < 0.33f)
                {
                    cellScript.currentState = CellScript.CellState.Dirt;
                }
                else if (randomValue < 0.66f)
                {
                    cellScript.currentState = CellScript.CellState.Grass;
                }
                else
                {
                    cellScript.currentState = CellScript.CellState.Plant;
                }

                cellScript.SetMaterial();
            }
        }

        gameText.text = "Water every plot into flowers before time runs out!";
        timerText.text = "Timer: " + gameTime;
    }

    public int CountNeighbors(int xIndex, int yIndex)
    {
        int count = 0;

        for (int x = xIndex - 1; x <= xIndex + 1; x++)
        {
            for (int y = yIndex - 1; y <= yIndex + 1; y++)
            {
                if (x >= 0 && x < 5 && y >= 0 && y < 5)
                {
                    if (!(x == xIndex && y == yIndex))
                    {
                        if (grid[x, y].currentState == CellScript.CellState.Plant)
                        {
                            count++;
                        }
                    }
                }
            }
        }

        return count;
    }

    public void CheckIfWin()
    {
        foreach (var cell in grid)
        {
            if (cell.currentState != CellScript.CellState.Plant)
            {
                return;
            }
        }

        gameText.text = "You Win!";
        isGameOver = true;
    }

    public void CheckIfLose()
    {
        gameText.text = "You Lose!";
        isGameOver = true;
    }

    void Update()
    {
        if (!isGameOver)
        {
            gameTime -= Time.deltaTime;
            int timeRemaining = Mathf.FloorToInt(gameTime);
            timerText.text = "Time Left: " + Mathf.Max(0, timeRemaining);
            CheckIfWin();

            if (gameTime <= 0)
            {
                CheckIfLose();
            }
        }
    }
}
