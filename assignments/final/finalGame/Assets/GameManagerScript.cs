using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    int jellyfishScore = 0;
    float energy = 100f;
    float energyDrainRate = 1f;
    float movementSpeed = 3f;
    private bool isGameOver = false;
    private bool ifGameStarted = false;
    float gameDuration = 60f;
    private float timer;

    public TMP_Text scoreText;
    public TMP_Text energyText;
    public TMP_Text gameStatusText;
    public TMP_Text resultsText;
    public TMP_Text timerText;
    public GameObject startButton;
    public GameObject startPanel;

    void Start()
    {
        // Singleton pattern to ensure only one instance of GameManager exists
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject); // Make this persist across scenes
    }

    void Update()
    {
        if (!ifGameStarted) return; // Prevent game actions if not started

        if (!isGameOver)
        {
            // Handle energy drain
            energy -= energyDrainRate * Time.deltaTime;
            energy = Mathf.Clamp(energy, 0, 100); // Ensure energy stays within bounds
            if (energyText != null)
                energyText.text = "Energy: " + Mathf.FloorToInt(energy);

            if (energy <= 0)
                GameOver("Out of Energy");

            // Handle timer countdown
            timer -= Time.deltaTime;
            timer = Mathf.Clamp(timer, 0, gameDuration); // Make sure timer doesn't go below 0

            if (timerText != null)
            {
                timerText.text = "Time: " + Mathf.FloorToInt(timer);
            }

            if (timer <= 0)
            {
                GameOver("Times Up!");
            }

            movementSpeed = 3f * (energy / 100f);
        }

        if (scoreText != null)
            scoreText.text = "Score: " + jellyfishScore;
    }

    public void CollectKrabbyPatty()
    {
        energy += 10f;
        energy = Mathf.Clamp(energy, 0, 100);
    }

    public void CollectJellyfish()
    {
        jellyfishScore++;
    }

    private void GameOver(string gameStatusMessage)
    {
        isGameOver = true;

        if (gameStatusText != null)
            gameStatusText.text = gameStatusMessage;

        if (resultsText != null)
            resultsText.text = "Jellyfish Collected: " + jellyfishScore;

        Time.timeScale = 0f;
    }

    public void StartGame()
    {
        ifGameStarted = true;
        timer = gameDuration;
        if (startButton != null)
            startButton.SetActive(false);
        if (startPanel != null)
            startPanel.SetActive(false);

        Time.timeScale = 1f;
    }

}
