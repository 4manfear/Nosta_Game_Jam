using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class Drag_and_Drop : MonoBehaviour
{
    [Header("References Script")]
    public Money_manager manager;

    [SerializeField]
    public GameObject guard, cat, trap;

    [SerializeField]
    private bool clicked;

    [SerializeField]
    private Camera Main_Camera;

    private const string ground = "GroundLayer";

    private RaycastHit2D hit;

    private void Update()
    {
        throw_ray_to_the_point_of_touch_or_click();
        manager = FindObjectOfType<Money_manager>();
    }

    void throw_ray_to_the_point_of_touch_or_click()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)) // Mouse click (left button)
        {
            Vector2 positionOfMouse = Main_Camera.ScreenToWorldPoint(Input.mousePosition); // Get the world position of the mouse click

            // Perform 2D Raycast using the world position of the mouse
            hit = Physics2D.Raycast(positionOfMouse, Vector2.zero); // Raycast from the mouse position, no direction, only checking if we hit something

            if (hit.collider != null)
            {
                if (hit.collider.gameObject.layer == LayerMask.NameToLayer(ground))
                {
                    // Use hit.point to get the exact world position where the ray hit
                    Vector2 positionToSpawn = hit.point;
                    spawn_the_guard(positionToSpawn);
                }
            }
        }
    }

    void spawn_the_guard(Vector2 position)
    {
        if (manager.guards_bool)
        {
            Instantiate(guard, position, Quaternion.identity);
            manager.Recurtering_guard();
            manager.guards_bool = false;
        }
        if (manager.cats_bool)
        {
            Instantiate(cat, position, Quaternion.identity);
            manager.buying_new_cat();
            manager.cats_bool = false;
        }
        if (manager.traps_bool)
        {
            Instantiate(trap, position, Quaternion.identity);
            manager.buy_trap();
            manager.traps_bool = false;
        }
    }

}
