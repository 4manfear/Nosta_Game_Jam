using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneManager : MonoBehaviour
{
    public Camera mainCamera;  // Reference to the main camera
    public int miniGameSceneIndex;  // Scene index of the mini-game scene (set in Inspector)

    public bool miniGameActive = false;
    public bool cooldown = false;
    public float cooldownDuration ; // Set cooldown duration in seconds

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !miniGameActive && !cooldown)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider != null && hit.collider.CompareTag("MiniGameTrigger"))  // Tag for mini-game trigger object
            {
                StartMiniGame();
            }
        }
    }

    void StartMiniGame()
    {
        miniGameActive = true;
        mainCamera.enabled = false;  // Disable main scene camera to "pause" the main scene

        SceneManager.LoadSceneAsync(miniGameSceneIndex, LoadSceneMode.Additive);
    }

    public void StartCooldown()
    {
        cooldown = true;
        StartCoroutine(CooldownCoroutine());
    }

    private IEnumerator CooldownCoroutine()
    {
        float cooldownTimer = cooldownDuration;

        while (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
            yield return null;
        }

        cooldown = false;
    }


}
