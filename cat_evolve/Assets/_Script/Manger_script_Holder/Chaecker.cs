using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chaecker : MonoBehaviour
{
  
    public Money_manager manager;


    public items_holder holder;

    private void Update()
    {
        manager = FindObjectOfType<Money_manager>();
        holder = FindObjectOfType<items_holder>();
    }

    public void amout_guard_checker()
    {
        if(manager.current_Money_holds > manager.minimum_amount_for_guard)
        {
            manager.guards_bool = true;
            manager.cats_bool = false;
            manager.traps_bool = false;
        }
        else
        {
            manager.guards_bool = false;
            Debug.Log("not enough money for guard");
        }
    }
    public void amount_cats_checker()
    {
        if (manager.current_Money_holds >= manager.minimum_amount_for_cats)
        {
            manager.cats_bool = true;
            manager.guards_bool = false;
            manager.traps_bool = false;
        }
        else
        {
            manager.cats_bool = false;
            Debug.Log("not enough money for cats");
        }
    }
    public void amount_traps_checker()
    {
        if (holder.cancraftpithole)
        {
            manager.traps_bool = true;
            manager.guards_bool = false;
            manager.cats_bool = false;

            holder.crating_pitholes();

        }
        else
        {
            manager .traps_bool = false;
            Debug.Log("not enough money for traps");
        }
    }

    public void canceling_the_Selection()
    {
        manager.guards_bool = false;
        manager.traps_bool = false;
        manager.cats_bool = false;
    }

}
