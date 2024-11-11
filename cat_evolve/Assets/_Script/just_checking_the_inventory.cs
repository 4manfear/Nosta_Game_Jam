using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class just_checking_the_inventory : MonoBehaviour 
{
    public The_xp_holder xp_Holder;

    private void Update()
    {
        if (xp_Holder == null)
        {
            xp_Holder = Resources.Load<The_xp_holder>("The_xp_holder");
        }
    }


}
