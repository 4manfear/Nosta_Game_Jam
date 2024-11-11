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

    public bool can_chang_intowater;
    public bool can_change_intoGround;

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
        UpdateSprite();
        changing_the_animator_controller();

        if(can_chang_intowater)
        {
            currentCatType = CatType.WaterCat;
        }
        if (can_change_intoGround)
        {
            currentCatType = CatType.EarthCat;
        }

        xp = xp_holder.xp;

        if (currentCatType == CatType.WiseCat)
        {
            cat_is_the_wise_cat = true;
        }
        else
        {
            cat_is_the_wise_cat = false;
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

    void changing_the_animator_controller()
    {
        if (currentCatType == CatType.BaseCat)
        {
            anim.runtimeAnimatorController = bace_cat_clip;
        }
        else if (currentCatType == CatType.WiseCat)
        {
            anim.runtimeAnimatorController = wise_cat_clip;
        }
        else if (currentCatType == CatType.WaterCat)
        {
            anim.runtimeAnimatorController = water_cat_clip;
        }
        else if (currentCatType == CatType.EarthCat)
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
        else if (currentCatType == CatType.WiseCat && waterxp >= 100)
        {
            canUpgrade = true;
            Debug.Log("Cat is ready to evolve to Water Cat! Click on the cat to upgrade.");
        }
        else if (currentCatType == CatType.WiseCat && grassxp >= 100)
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
            
            
            Debug.Log("Cat has evolved to Wise Cat.");
        }
        else if (currentCatType == CatType.WiseCat && waterxp >= 100)
        {
           
            Debug.Log("Cat has evolved to Water Cat.");
        }
        else if (currentCatType == CatType.WiseCat && grassxp >= 100)
        {
            
            Debug.Log("Cat has evolved to Earth Cat.");
        }

        canUpgrade = false; // Reset upgrade flag after evolving
    }

    public void chang_water_form_button()
    {
        Debug.Log("Water button clicked");
        if (currentCatType == CatType.WiseCat && waterxp >= 100)
        {
            can_chang_intowater = true;
            cat_is_the_wise_cat = false;
            can_change_intoGround = false;
            currentCatType = CatType.WaterCat;
            waterxp -= 100;
            
            
         
            Debug.Log("Cat has changed to Water Cat form.");
        }
        else
        {
            Debug.Log("Cannot change to Water Cat form.");
        }
    }

    public void change_ground_form_button()
    {
        Debug.Log("Ground button clicked");
        if (currentCatType == CatType.WiseCat && grassxp >= 100)
        {
            can_chang_intowater = false;
            cat_is_the_wise_cat = false;
            can_change_intoGround = true;

            grassxp -= 100;
            
            
            

            Debug.Log("Cat has changed to Earth Cat form.");
        }
        else
        {
            Debug.Log("Cannot change to Earth Cat form.");
        }
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
