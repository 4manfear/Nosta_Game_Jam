using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mole_or_Ant_distruction_Script : MonoBehaviour
{
    public Money_manager manager;

    private void Update()
    {
        manager = FindObjectOfType<Money_manager>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Guard"))
        {
            Debug.Log("collision happend");

            //manager.current_Money_holds += 20;

            //Destroy(this.gameObject);
        }
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if(collision.gameObject.CompareTag("Guard"))
    //    {
    //        Debug.Log("collision happend");


    //    }
    //}
}
