using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard_Near_to_the : MonoBehaviour
{
    [Header("Guard game object holder")]
    [SerializeField] private GameObject[] No_Of_Guards;

    [Header("Cat position")]
    [SerializeField] private Transform catTransform;  // Reference to the cat's position

    [Header("Nearest Guard")]
    public GameObject nearestGuard;  // Store the nearest guard

    private void Update()
    {
        catTransform = this.transform;

        // Update the list of guards
        No_Of_Guards = GameObject.FindGameObjectsWithTag("Guard");

        // Find the nearest guard
        FindNearestGuard();
    }

    private void FindNearestGuard()
    {
        float closestDistance = Mathf.Infinity;
        GameObject closestGuard = null;

        foreach (GameObject guard in No_Of_Guards)
        {
            float distanceToCat = Vector3.Distance(guard.transform.position, catTransform.position);

            // Check if this guard is closer to the cat than the previously recorded closest guard
            if (distanceToCat < closestDistance)
            {
                closestDistance = distanceToCat;
                closestGuard = guard;
            }
        }

        nearestGuard = closestGuard;

        // (Optional) Debug to check if the nearest guard was found
        if (nearestGuard != null)
        {
            Debug.Log("Nearest guard to the cat is: " + nearestGuard.name);
        }
    }

}
