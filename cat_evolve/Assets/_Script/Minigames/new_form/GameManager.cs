using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    [Header("Score Settings")]
    public int score = 0;
    public Text scoreText;

    [Header("Game Over Settings")]
    public GameObject gameOverPanel;

    [Header("Timer Settings")]
    public float timeLimit = 30f; // Set the timer duration in seconds
    private float currentTime;
    public Text timerText;

    void Start()
    {
        currentTime = timeLimit;
        UpdateScoreText();
        UpdateTimerText();
        gameOverPanel.SetActive(false); // Hide Game Over panel at the start
    }

    void Update()
    {
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            UpdateTimerText();

            if (currentTime <= 0)
            {
                GameOver();
            }
        }
    }

    public void IncreaseScore(int amount)
    {
        score += amount;
        UpdateScoreText();
    }

    public void GameOver()
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f; // Pause the game
    }

    void UpdateScoreText()
    {
        scoreText.text = "Score: " + score;
    }

    void UpdateTimerText()
    {
        timerText.text = "Time: " + Mathf.CeilToInt(currentTime);
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
