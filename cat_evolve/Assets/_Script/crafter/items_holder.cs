using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class items_holder : MonoBehaviour
{
    public int leaves;
    public int sticks;

    public int stones;
    public int froots;
    public int mushroom;


    public int pitholes;


    public bool cancraftpithole;

    private void Update()
    {
        if (leaves > 0 && sticks > 0)
        {
            cancraftpithole = true;
        }
        else
        {
            cancraftpithole= false;
        }
    }

    public void crating_pitholes()
    {
        if (cancraftpithole)
        {
            leaves--;
            sticks--;
            pitholes++;
        }
    }
}
