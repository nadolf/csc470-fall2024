using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

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
    public GameObject nextButton;
    public GameObject headerPanel;
    public GameObject HowToPlayPanel;
    public GameObject startButton;
    public GameObject gameOverPanel;
    public GameObject controlsPanel;
    public GameObject controlsButton;
    public Image EnergyBar;
    private float MaxEnergy = 100f;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
        UpdateEnergyBar();
    }

    void Update()
    {
        if (!ifGameStarted) return;

        if (!isGameOver)
        {
            // Handle energy drain
            energy -= energyDrainRate * Time.deltaTime;
            energy = Mathf.Clamp(energy, 0, MaxEnergy);
            if (energyText != null)
                energyText.text = "Energy: ";

            UpdateEnergyBar();

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
                GameOver("Game Over!");
            }

            movementSpeed = 3f * (energy / MaxEnergy);
        }

        if (scoreText != null)
            scoreText.text = "Score: " + jellyfishScore;
    }

    public void CollectKrabbyPatty()
    {
        energy += 10f;
        energy = Mathf.Clamp(energy, 0, MaxEnergy);
        UpdateEnergyBar();
    }

    public void CollectJellyfish()
    {
        jellyfishScore++;
    }

    private void GameOver(string gameStatusMessage)
    {
        isGameOver = true;
        gameOverPanel.SetActive(true);

        if (gameStatusText != null)
            gameStatusText.text = gameStatusMessage;

        if (resultsText != null)
            resultsText.text = "Jellyfish Collected: " + jellyfishScore;

        Time.timeScale = 0f;
    }

    public void CloseStartScreen()
    {
        if (nextButton != null)
            nextButton.SetActive(false);
        if (headerPanel != null)
            headerPanel.SetActive(false);
    }

    public void CloseControlsScreen()
    {
        if (controlsButton != null)
            controlsButton.SetActive(false);
        if (controlsPanel != null)
            controlsPanel.SetActive(false);
    }

    public void StartGame()
    {
        ifGameStarted = true;
        timer = gameDuration;
        if (startButton != null)
            startButton.SetActive(false);
        if (HowToPlayPanel != null)
            HowToPlayPanel.SetActive(false);

        Time.timeScale = 1f;
    }

    private void UpdateEnergyBar()
    {
        if (EnergyBar != null)
        {
            EnergyBar.fillAmount = energy / MaxEnergy;
        }
    }
}
