using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money_manager : MonoBehaviour
{
    public int current_Money_holds;

    public Drag_and_Drop drag_And_Drop;

    


    private void Start()
    {
        current_Money_holds = 100;
    }

    private void Update()
    {
        if (current_Money_holds >= 0)
        {
            drag_And_Drop.gameObject.SetActive(false);
        }

        

    }


    

    public void Recurtering_guard()
    {
        
    }

    public void buying_new_cat()
    {

    }

    public void cat_evolved()
    {

    }

    public void buy_trap()
    {

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
