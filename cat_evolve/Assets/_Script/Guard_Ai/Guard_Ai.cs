using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard_Ai : MonoBehaviour
{
    [Header("AI Movement Settings")]
    [SerializeField] private LayerMask obstacleLayer;  // Obstacles to avoid
    [SerializeField] private LayerMask walkableLayer;  // Walkable areas
    [SerializeField] private float speedOfAi = 3f;     // Movement speed
    [SerializeField] private float detectionRadius = 5f; // Obstacle detection radius
    [SerializeField] private int rayCount = 8;         // Number of rays to cast
    [SerializeField] private float patrolRadius = 4f;  // Radius for patrolling
    [SerializeField] private float pauseDuration = 4f; // Pause time between patrol points

    public Transform target;                          // Target to move toward (set by cat_in_danger_Range)
    private Vector3 patrolTarget;
    private bool isPatrolling = true;                  // Patrol mode flag
    private float pauseTimer;

    void Start()
    {
        SetNewPatrolPoint();
    }

    void Update()
    {
        if (target != null)
        {
            MoveTowardsTarget();
            AvoidObstacles();
        }
        else if (isPatrolling)
        {
            Patrol();
            AvoidObstacles();
        }
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
        isPatrolling = false; // Stop patrolling when chasing a target
    }

    private void MoveTowardsTarget()
    {
        Vector3 directionToTarget = (target.position - transform.position).normalized;
        transform.position += directionToTarget * speedOfAi * Time.deltaTime;
    }

    private void Patrol()
    {
        if (Vector3.Distance(transform.position, patrolTarget) <= 0.1f)
        {
            pauseTimer += Time.deltaTime;

            if (pauseTimer >= pauseDuration)
            {
                SetNewPatrolPoint();
                pauseTimer = 0;
            }
        }
        else
        {
            Vector3 directionToPatrol = (patrolTarget - transform.position).normalized;
            transform.position += directionToPatrol * speedOfAi * Time.deltaTime;
        }
    }

    private void SetNewPatrolPoint()
    {
        Vector3 randomDirection = Random.insideUnitCircle * patrolRadius;
        patrolTarget = transform.position + new Vector3(randomDirection.x, randomDirection.y, 0);

        // Ensure patrol point is on walkable area
        RaycastHit2D hit = Physics2D.Raycast(patrolTarget, Vector2.down, Mathf.Infinity, walkableLayer);
        if (hit.collider == null)
        {
            SetNewPatrolPoint(); // Retry if not walkable
        }
    }

    private void AvoidObstacles()
    {
        for (int i = 0; i < rayCount; i++)
        {
            float angle = i * Mathf.PI * 2 / rayCount;
            Vector2 rayDirection = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * detectionRadius;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, rayDirection, detectionRadius, obstacleLayer);

            if (hit.collider != null)
            {
                // Ensure you're only using the x and y components for 2D
                Vector2 avoidDirection = (Vector2)(transform.position - new Vector3(hit.point.x, hit.point.y, transform.position.z)).normalized;

                // Apply obstacle avoidance by steering away from obstacles
                transform.position += (Vector3)(avoidDirection * speedOfAi * Time.deltaTime);
                Debug.DrawRay(transform.position, rayDirection, Color.red); // Visualize the raycast for debugging
            }
        }
    }

}
