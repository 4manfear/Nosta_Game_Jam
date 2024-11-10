using UnityEngine;

public class ItemMovement : MonoBehaviour
{
   [SerializeField] private float fallSpeed = 3f;

    public void SetFallSpeed(float speed)
    {
        fallSpeed = speed;
    }

    private void Update()
    {
        transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("BottomBoundary"))
        {
            Destroy(gameObject);
        }
    }
}
