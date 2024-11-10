using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bioam_unlocker : MonoBehaviour
{
    
    public Cat_ai_random_movement cat_script;

    public GameObject water_bioame;
    public GameObject grass_bioame;

    private void Update()
    {
        if(cat_script==null)
        {
            cat_script = FindObjectOfType<Cat_ai_random_movement>();
        }

        if (cat_script.cat_is_the_wise_cat)
        {
            water_bioame.SetActive(true);
            grass_bioame.SetActive(true);
        }
        else
        {
            water_bioame.SetActive(false);
            grass_bioame.SetActive(false);
        }
    }

}
