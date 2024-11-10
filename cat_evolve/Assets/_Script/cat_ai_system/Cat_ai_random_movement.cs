using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat_ai_random_movement : MonoBehaviour
{
    public enum CatType { BaseCat, WiseCat, WaterCat, EarthCat }
    public CatType currentCatType = CatType.BaseCat;

    [Header("Evolution Settings")]
    public int xp;
    public int waterxp;
    public int grassxp;
    public int xpToEvolve = 100;
    public string currentBiome = "None"; // Can be "Water" or "Earth" or "None"
    private bool canUpgrade = false; // Flag to check if cat can evolve

    [Header("Movement Settings")]
    public float baseRoamRadius = 5f;
    public float wiseCatRoamRadius = 10f;
    public float specialCatRoamRadius = 15f;
    private float roamRadius;

    [Header("Sprites for Evolutions")]
    public Sprite baseCatSprite;
    public Sprite wiseCatSprite;
    public Sprite waterCatSprite;
    public Sprite earthCatSprite;
    private SpriteRenderer spriteRenderer;

    private Vector2 startPosition;

    void Start()
    {
        startPosition = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
        UpdateRoamRadius();
        UpdateSprite(); // Set initial sprite based on starting type
        StartCoroutine(Roam());
    }

    void Update()
    {
        CheckEvolution();

        // Check if the player clicks on the cat and it can evolve
        if (Input.GetMouseButtonDown(0) && canUpgrade)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                Debug.Log(hit.collider.gameObject);
                EvolveCat();
            }
        }
    }

    private void UpdateRoamRadius()
    {
        if (currentCatType == CatType.BaseCat)
            roamRadius = baseRoamRadius;
        else if (currentCatType == CatType.WiseCat)
            roamRadius = wiseCatRoamRadius;
        else
            roamRadius = specialCatRoamRadius;
    }

    private void CheckEvolution()
    {
        if (currentCatType == CatType.BaseCat && xp >= xpToEvolve)
        {
            canUpgrade = true;
            Debug.Log("Cat is ready to evolve to Wise Cat! Click on the cat to upgrade.");
        }
        else if (currentCatType == CatType.WiseCat && xp >= xpToEvolve && currentBiome == "Water")
        {
            canUpgrade = true;
            Debug.Log("Cat is ready to evolve to Water Cat! Click on the cat to upgrade.");
        }
        else if (currentCatType == CatType.WiseCat && xp >= xpToEvolve && currentBiome == "Earth")
        {
            canUpgrade = true;
            Debug.Log("Cat is ready to evolve to Earth Cat! Click on the cat to upgrade.");
        }
        else if ((currentCatType == CatType.WaterCat || currentCatType == CatType.EarthCat) && currentBiome != currentCatType.ToString())
        {
            currentCatType = CatType.WiseCat;
            xp = 0; // Reset XP
            canUpgrade = false;
            UpdateRoamRadius();
            UpdateSprite();
            Debug.Log("Cat has been demoted to Wise Cat.");
        }
        else
        {
            canUpgrade = false;
        }
    }

    private void EvolveCat()
    {
        if (!canUpgrade) return;

        if (currentCatType == CatType.BaseCat)
        {
            currentCatType = CatType.WiseCat;
            xp = 0;
            UpdateRoamRadius();
            UpdateSprite();
            Debug.Log("Cat has evolved to Wise Cat.");
        }
        else if (currentCatType == CatType.WiseCat && currentBiome == "Water")
        {
            currentCatType = CatType.WaterCat;
            xp = 0;
            UpdateRoamRadius();
            UpdateSprite();
            Debug.Log("Cat has evolved to Water Cat.");
        }
        else if (currentCatType == CatType.WiseCat && currentBiome == "Earth")
        {
            currentCatType = CatType.EarthCat;
            xp = 0;
            UpdateRoamRadius();
            UpdateSprite();
            Debug.Log("Cat has evolved to Earth Cat.");
        }

        canUpgrade = false; // Reset upgrade flag after evolving
    }

    private void UpdateSprite()
    {
        if (currentCatType == CatType.BaseCat)
            spriteRenderer.sprite = baseCatSprite;
        else if (currentCatType == CatType.WiseCat)
            spriteRenderer.sprite = wiseCatSprite;
        else if (currentCatType == CatType.WaterCat)
            spriteRenderer.sprite = waterCatSprite;
        else if (currentCatType == CatType.EarthCat)
            spriteRenderer.sprite = earthCatSprite;
    }

    private IEnumerator Roam()
    {
        while (true)
        {
            Vector2 randomPoint = startPosition + Random.insideUnitCircle * roamRadius;
            transform.position = Vector2.MoveTowards(transform.position, randomPoint, 1f * Time.deltaTime);
            yield return new WaitForSeconds(2f);
        }
    }

    public void GainXP(int amount)
    {
        xp += amount;
    }

    public void SetBiome(string biome)
    {
        currentBiome = biome;
    }

}
