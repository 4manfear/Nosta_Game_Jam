using System;
using Unity.Mathematics;
using UnityEngine;

public class patteren_spawner : MonoBehaviour
{
    [SerializeField]
    private int Random_Number_gunrated;


    //private void Update()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        random_pattern_number_junrator();
    //    }
    //}

    void random_pattern_number_junrator()
    {
        Random_Number_gunrated = UnityEngine.Random.Range(0, 6);
        Debug.Log(Random_Number_gunrated);
    }
}
