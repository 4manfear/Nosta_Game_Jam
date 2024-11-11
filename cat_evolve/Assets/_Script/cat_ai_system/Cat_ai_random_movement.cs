using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.SceneManagement;


public class Cat_ai_random_movement : MonoBehaviour
{
    public The_xp_holder xp_holder;

    public Animator anim;

    public enum CatType { BaseCat, WiseCat, WaterCat, EarthCat }
    public CatType currentCatType = CatType.BaseCat;
    public bool cat_is_the_wise_cat;

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
    public AnimatorController bace_cat_clip;
    public Sprite wiseCatSprite;
    public AnimatorController wise_cat_clip;
    public Sprite waterCatSprite;
    public AnimatorController water_cat_clip;
    public Sprite earthCatSprite;
    public AnimatorController earth_cat_clip;
    private SpriteRenderer spriteRenderer;

    private Vector2 startPosition;

    void Start()
    {

        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            // Reset XP values in the ScriptableObject
            if (xp_holder != null)
            {
                xp_holder.ResetXP();
            }
            else
            {
                Debug.LogError("XP Holder ScriptableObject is not assigned!");
            }
        }

        anim = GetComponent<Animator>();

        startPosition = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
        UpdateRoamRadius();
        UpdateSprite(); // Set initial sprite based on starting type
        StartCoroutine(Roam());
    }

    void Update()
    {
        xp = xp_holder.xp;

        if(currentCatType == CatType.WiseCat)
        {
            cat_is_the_wise_cat = true;
        }

        CheckEvolution();

        // Check if the player clicks on the cat and it can evolve
        if (Input.GetMouseButtonDown(0) && canUpgrade)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                EvolveCat();
            }
        }
    }

    void changing_the_animator_cuntroller()
    {
        if(currentCatType == CatType.BaseCat)
        {
            anim.runtimeAnimatorController = bace_cat_clip;
        }
        if (currentCatType == CatType.WiseCat)
        {
            anim.runtimeAnimatorController = wise_cat_clip;
        }
        if (currentCatType == CatType.WaterCat)
        {
            anim.runtimeAnimatorController = water_cat_clip;
        }
        if (currentCatType == CatType.EarthCat)
        {
            anim.runtimeAnimatorController = earth_cat_clip;
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
            xp -= xpToEvolve;
            xp_holder.xp = xp;
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
