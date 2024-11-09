using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money_manager : MonoBehaviour
{
    public int current_Money_holds;

    public Drag_and_Drop drag_And_Drop;

    [Header("minimum amount needed")]
    public int minimum_amount_for_guard, minimum_amount_for_cats, minimum_amount_for_traps;

    [Header("bool statement")]
    public bool guards_bool, cats_bool, traps_bool;


    private void Start()
    {
        current_Money_holds = 100;
        drag_And_Drop = FindAnyObjectByType<Drag_and_Drop>();
    }

    private void Update()
    {
        if (current_Money_holds > 0)
        {
            drag_And_Drop.enabled = true;
        }
        else
        {
            drag_And_Drop.enabled=false;
        }

        

    }


    

    public void Recurtering_guard()
    {
        current_Money_holds -= minimum_amount_for_guard;
    }

    public void buying_new_cat()
    {
        current_Money_holds -= minimum_amount_for_cats;
    }

    public void cat_evolved()
    {

    }

    public void buy_trap()
    {
        current_Money_holds -= minimum_amount_for_traps;
    }

    public void buy_pesticide()
    {

    }
    public void buy_food()
    {

    }
    public void trash_disposial()
    {

    }


}
