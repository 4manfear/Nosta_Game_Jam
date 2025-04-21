using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class health_manager : MonoBehaviour
{
    [SerializeField] private Image healthBar;
    [SerializeField] private float maxHealth;

    public float currentHealth;

    private bool isHealthDecreasing = false; // Bool to track slow health decrease
    public float healthDecreaseRate ; // Amount of health lost per second

    void Start()
    {
        currentHealth = maxHealth;
        updateHealth();

    }


    

    public void damageHealth(float damage)
    {
        if (currentHealth > 0)
        {
            // Deduct health
            currentHealth -= damage;

            // Display health
            updateHealth();

            // Check Death
            checkDeath();

        }
    }

    public void healHealth(float heal)
    {
        if (currentHealth < maxHealth)
        {
            // Add health
            currentHealth += heal;

            // Display health
            updateHealth();

        }

    }

    private void updateHealth()
    {
        // Clamp health 0-max
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        // Set Bar
        healthBar.fillAmount = currentHealth / maxHealth;

    }


    private void checkDeath()
    {
        // Check death
        if (currentHealth <= 0)
        {
            // Handle death
            Destroy(gameObject);

        }


    }

    public void time_slow_health_decreaser(bool enable)
    {
        isHealthDecreasing = enable;

        if (isHealthDecreasing)
        {
            StartCoroutine(DecreaseHealthOverTime());
        }
        else
        {
            StopCoroutine(DecreaseHealthOverTime());
        }
    }

    private IEnumerator DecreaseHealthOverTime()
    {
        while (isHealthDecreasing)
        {
            damageHealth(healthDecreaseRate * Time.unscaledDeltaTime); // Use unscaled time
            yield return null; // Wait for the next frame
        }
    }

}
