using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard_Ai : MonoBehaviour
{
    [Header("AI Movement Settings")]
    [SerializeField]
    private LayerMask obstacleLayer; // Obstacles that the AI should avoid
    [SerializeField]
    private LayerMask walkableLayer; // Walkable areas for the AI
    [SerializeField]
    private GameObject target; // The target (could be the player or any other object)
    [SerializeField]
    private float speedOfAi = 3f; // AI movement speed
    [SerializeField]
    private float detectionRadius = 5f; // Radius for obstacle detection

    [SerializeField]
    private int rayCount = 8; // Number of rays to cast in the circle

    private Vector2 directionToTarget;

    void Update()
    {
        rangecreation();
    }

    void rangecreation()
    {
        // Iterate over a circle, casting rays at different angles
        for (int i = 0; i < rayCount; i++)
        {
            // Calculate the angle in radians for each ray
            float angle = i * Mathf.PI * 2 / rayCount;

            // Create a direction for the ray (based on angle)
            Vector2 rayDirection = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * detectionRadius;

            // Cast a ray from the current position in the calculated direction
            RaycastHit2D hit = Physics2D.Raycast(transform.position, rayDirection, detectionRadius, obstacleLayer | walkableLayer);

            // Debug visualization for the ray
            Debug.DrawRay(transform.position, rayDirection, Color.red);

            // If the ray hits an object, print out the layer of the object hit
            if (hit.collider != null)
            {
                Debug.Log("Hit object on Layer: " + LayerMask.LayerToName(hit.collider.gameObject.layer));
            }
        }
    }

   
}
