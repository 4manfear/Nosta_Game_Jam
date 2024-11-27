using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water_biom_miniGame : MonoBehaviour
{
    [Header("Float valriables")]
    [SerializeField]
    private float jump_force;
    [SerializeField]
    private float Distance_calcutaor_for_hit_the_obsical;
    [SerializeField]
    private float jumpdiatnace;

    [Header("references")]
    [SerializeField]
    private Rigidbody2D rb;

    public void catjump()
    {
        rb.AddForce(Vector2.up * jump_force * Time.deltaTime, ForceMode2D.Impulse);
        Debug.Log("jump_called");
    }


}
