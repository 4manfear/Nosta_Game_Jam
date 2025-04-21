using System.Collections;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float lifetime = 3f;
    private Transform player;
    private Vector2 direction;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        if (player != null)
        {
            direction = (player.position - transform.position).normalized;
        }
        else
        {
            direction = Vector2.right; // Default direction if player is not found
        }

        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        transform.position += (Vector3)direction * speed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Wall")) 
        {
            Destroy(gameObject);
        }
    }
}