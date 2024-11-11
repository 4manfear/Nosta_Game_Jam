using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat_selection_script : MonoBehaviour
{
    public bool catIsSelected = false; // Boolean to track if the cat is selected
    public GameObject indicator; // The GameObject to activate when the cat is selected

    private void Start()
    {
        // Ensure the indicator is inactive at the start
        if (indicator != null)
        {
            indicator.SetActive(false);
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
            if(hit.collider.CompareTag("LULU"))
            {
                Debug.Log("ter maaa ki chu untiy c#");
                OnMouseDown();
            }
        }
    }

    private void OnMouseDown()
    {
        // This function is called when the object is clicked
        catIsSelected = true;

        // Activate the indicator when cat is selected
        if (indicator != null)
        {
            indicator.SetActive(true);
        }
    }
}
