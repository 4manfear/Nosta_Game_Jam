using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "The_xp_holder", menuName = "The_xp_holder")]
public class The_xp_holder : ScriptableObject
{
    public int xp;

    public int waterxp;

    public int grassxp;

    public void ResetXP()
    {
        xp = 0;
        waterxp = 0;
        grassxp = 0;
    }



}
