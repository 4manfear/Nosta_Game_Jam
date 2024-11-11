using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public The_xp_holder xp_Holder;


    [Header("Score Settings")]
    public int score = 0;
    public Text scoreText;

    [Header("Game Over Settings")]
    public GameObject gameOverPanel;

    [Header("Timer Settings")]
    public float timeLimit = 30f; // Set the timer duration in seconds
    private float currentTime;
    public Text timerText;

    [Header("Mini Game Settings")]
    public int miniGameSceneIndex; // Index of the mini-game scene to load

    private Camera mainSceneCamera;
    private bool isGameOver = false;

    MainSceneManager mainSceneManager;

    public bool cameraonker;

    void Start()
    {

       

        currentTime = timeLimit;
        UpdateScoreText();
        UpdateTimerText();
        gameOverPanel.SetActive(false); // Hide Game Over panel at the start

        // Assume the main camera is the one in the main scene
        mainSceneCamera = Camera.main;

        mainSceneManager = FindObjectOfType<MainSceneManager>();
    }

    void Update()
    {
        //xp_Holder = Resources.Load<The_xp_holder>("The_xp_holder");

        //// Check if the asset was successfully loaded
        //if (xp_Holder == null)
        //{
        //    Debug.LogError("The_xp_holder ScriptableObject not found! Ensure it is in the Resources folder.");
        //    return;
        //}


        if (!isGameOver)
        {
            currentTime -= Time.deltaTime;
            UpdateTimerText();

            if (currentTime <= 0)
            {
                GameOver();
            }
        }
    }

    public void LoadMiniGame()
    {
        // Disable main scene camera to "pause" the main scene
        mainSceneCamera.enabled = false;

        // Load the mini-game scene additively
        SceneManager.LoadSceneAsync(miniGameSceneIndex, LoadSceneMode.Additive);
    }

    public void IncreaseScore(int amount)
    {
        if (!isGameOver)
        {
            score += amount;
            UpdateScoreText();
        }
    }

    public void GameOver()
    {
        isGameOver = true;
        gameOverPanel.SetActive(true); // Show Game Over panel

        // Start coroutine to unload the mini-game scene after 2 seconds
        StartCoroutine(UnloadMiniGameSceneAfterDelay());
    }

    private IEnumerator UnloadMiniGameSceneAfterDelay()
    {
        cameraonker = true;

        yield return new WaitForSeconds(0.5f); // Wait for 0.5 seconds
        mainSceneCamera.enabled = true;
        // Unload mini-game scene
        SceneManager.UnloadSceneAsync(miniGameSceneIndex);

        mainSceneManager.cooldown = true;
        // Re-enable main scene camera and reset game over state
        mainSceneManager.mainCamera.enabled = true;
      
        gameOverPanel.SetActive(false); // Hide Game Over panel
        isGameOver = false;
    }

    void UpdateScoreText()
    {
        scoreText.text = "Score: " + score;
        xp_Holder.xp = xp_Holder.xp + score;
    }

    void UpdateTimerText()
    {
        timerText.text = "Time: " + Mathf.CeilToInt(currentTime);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
