using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject startScreen;
    public GameObject gameOverScreen;
    public GameObject mainPanel;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;

    private bool gameStarted = false;
    private float score = 0f;
    private int highScore = 0;

    void Start()
    {
        Time.timeScale = 0;
        LoadHighScore();
        highScoreText.text = "High Score: " + highScore;
        startScreen.SetActive(true);
        mainPanel.SetActive(false);
        gameOverScreen.SetActive(false);
    }

    void Update()
    {
        if (!gameStarted && Input.GetKeyDown(KeyCode.Space))
        {
            StartGame();
        }

        if (gameStarted)
        {
            UpdateScore();
        }
    }

    void StartGame()
    {
        gameStarted = true;
        startScreen.SetActive(false);
        gameOverScreen.SetActive(false);
        mainPanel.SetActive(true);
        Time.timeScale = 1;
        score = 0f;
    }

    public void GameOver()
    {
        gameStarted = false;
        gameOverScreen.SetActive(true);
        mainPanel.SetActive(false);
        Time.timeScale = 0;
        CheckHighScore();
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    void UpdateScore()
    {
        score += Time.deltaTime;
        scoreText.text = "Score: " + Mathf.FloorToInt(score).ToString();
    }

    void CheckHighScore()
    {
        int finalScore = Mathf.FloorToInt(score);
        if (finalScore > highScore)
        {
            highScore = finalScore;
            highScoreText.text = "High Score: " + highScore;
            SaveHighScore();
        }
    }

    void SaveHighScore()
    {
        PlayerPrefs.SetInt("HighScore", highScore);
        PlayerPrefs.Save();
    }

    void LoadHighScore()
    {
        if (PlayerPrefs.HasKey("HighScore"))
        {
            highScore = PlayerPrefs.GetInt("HighScore");
        }
    }
}
