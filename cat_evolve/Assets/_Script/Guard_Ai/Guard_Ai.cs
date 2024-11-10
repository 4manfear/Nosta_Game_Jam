using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard_Ai : MonoBehaviour
{
    [Header("AI Movement Settings")]
    [SerializeField] private LayerMask obstacleLayer;    // Obstacles to avoid
    [SerializeField] private LayerMask walkableLayer;    // Walkable areas
    [SerializeField] private float speedOfAi = 3f;       // Movement speed
    [SerializeField] private float detectionRadius = 5f; // Obstacle detection radius
    [SerializeField] private int rayCount = 8;           // Number of rays to cast
    [SerializeField] private float patrolRadius = 4f;    // Radius for patrolling
    [SerializeField] private float pauseDuration = 4f;   // Pause time between patrol points

    public Transform target;                             // Target to move toward (set by cat_in_danger_Range)
    private Vector3 patrolTarget;
    private bool isPatrolling = true;                    // Patrol mode flag
    private float pauseTimer;

    public Money_manager manager;


    void Start()
    {
        SetNewPatrolPoint();
    }

    void Update()
    {
        // Check if target is valid and on walkable layer
        if (target != null && IsOnWalkableLayer(target.position))
        {
            MoveTowardsTarget();
            AvoidObstacles();
        }
        else
        {
            // Reset target if it is destroyed or not on the walkable layer
            target = null;
            isPatrolling = true;
            Patrol();
            AvoidObstacles();
        }

        CheckAndDestroy();
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
        isPatrolling = false; // Stop patrolling when chasing a target
    }

    private void MoveTowardsTarget()
    {
        Vector3 directionToTarget = (target.position - transform.position).normalized;

        // Check if the next position is on the walkable layer
        Vector3 nextPosition = transform.position + directionToTarget * speedOfAi * Time.deltaTime;
        if (IsOnWalkableLayer(nextPosition))
        {
            transform.position = nextPosition;
        }
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
            Vector3 nextPosition = transform.position + directionToPatrol * speedOfAi * Time.deltaTime;

            // Ensure patrol movement only occurs on walkable areas
            if (IsOnWalkableLayer(nextPosition))
            {
                transform.position = nextPosition;
            }
            else
            {
                SetNewPatrolPoint(); // Find a new point if the current one isn't on a walkable area
            }
        }
    }

    private void SetNewPatrolPoint()
    {
        Vector3 randomDirection = Random.insideUnitCircle * patrolRadius;
        patrolTarget = transform.position + new Vector3(randomDirection.x, randomDirection.y, 0);

        // Ensure patrol point is on the walkable layer
        if (!IsOnWalkableLayer(patrolTarget))
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
                // Calculate avoidance direction
                Vector2 avoidDirection = (Vector2)(transform.position - new Vector3(hit.point.x, hit.point.y, transform.position.z)).normalized;

                // Apply obstacle avoidance by steering away from obstacles
                Vector3 nextPosition = transform.position + (Vector3)(avoidDirection * speedOfAi * Time.deltaTime);

                // Only move if the position is on the walkable layer
                if (IsOnWalkableLayer(nextPosition))
                {
                    transform.position = nextPosition;
                }

                Debug.DrawRay(transform.position, rayDirection, Color.red); // Visualize the raycast for debugging
            }
        }
    }

    private bool IsOnWalkableLayer(Vector3 position)
    {
        RaycastHit2D hit = Physics2D.Raycast(position, Vector2.down, Mathf.Infinity, walkableLayer);
        return hit.collider != null;
    }

    private void CheckAndDestroy()
    {
        if (target != null)
        {
            manager = FindObjectOfType<Money_manager>();

            float distanceToTarget = Vector2.Distance(transform.position, target.position);

            // Destroy target if close enough and tagged as "danger"
            if (distanceToTarget < 1f && target.gameObject.CompareTag("danger"))
            {

                Destroy(target.gameObject);
                manager.current_Money_holds += 20;
                target = null; // Reset target after destroying it
                isPatrolling = true;
            }
        }
    }

}
