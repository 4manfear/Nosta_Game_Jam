using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class cat_in_danger_Range : MonoBehaviour
{
    [Header("Insect or Mole Game Objects")]
    public GameObject[] insect_or_Mole;

    [Header("Range to Detect Danger")]
    [SerializeField] float range;

    [Header("Bool Statement")]
    public bool cat_in_Danger;

    [Header("Position of the Closest Danger Source")]
    public Transform dangerSource;  // Transform of the closest object in range


    public Guard_Ai aioffuard;

     // Reference to the Guard_Near_to_the script

    private void Update()
    {
        aioffuard = GameObject.FindObjectOfType<Guard_Ai>();

        DetectDanger();

        // Only try to move the nearest guard if the cat is in danger and the guard exists
        if (cat_in_Danger)
        {
           aioffuard.target = dangerSource;
        }
    }

    private void DetectDanger()
    {
        insect_or_Mole = GameObject.FindGameObjectsWithTag("danger");
        cat_in_Danger = false;  // Reset the danger flag
        dangerSource = null;    // Reset the danger source

        foreach (GameObject dangerObj in insect_or_Mole)
        {
            float distance_from_the_cat = Vector3.Distance(transform.position, dangerObj.transform.position);

            if (distance_from_the_cat <= range)
            {
                cat_in_Danger = true;
                dangerSource = dangerObj.transform;  // Store the position of the first object in range
                break;
            }
        }
    }
}


