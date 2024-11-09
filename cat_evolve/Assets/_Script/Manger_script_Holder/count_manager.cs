using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class count_manager : MonoBehaviour
{
    public int No_of_Guards;
    public int No_of_cats;
    
    public int No_of_Traps;

    private void Start()
    {
        // Initial count of all objects when the scene starts
        CountAllObjects();
    }

    // Method to count each object type in the scene
    public void CountAllObjects()
    {
        No_of_Guards = GameObject.FindGameObjectsWithTag("Guard").Length;
        No_of_cats = GameObject.FindGameObjectsWithTag("Cat").Length;
        No_of_Traps = GameObject.FindGameObjectsWithTag("Trap").Length;

        Debug.Log("Number of Guards: " + No_of_Guards);
        Debug.Log("Number of Cats: " + No_of_cats);
        Debug.Log("Number of Traps: " + No_of_Traps);
    }
}
