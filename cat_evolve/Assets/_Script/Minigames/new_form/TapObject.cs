using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapObject : MonoBehaviour
{
    public bool isScoreObject; // Set to true for score objects, false for bombs
    private GameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    void OnMouseDown()
    {
        if (isScoreObject)
        {
            gameManager.IncreaseScore(5);
            Destroy(gameObject); // Remove object after it's tapped
        }
        else
        {
            gameManager.GameOver();
            Destroy(gameObject);
        }
    }
}
