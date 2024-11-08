using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class Drag_and_Drop : MonoBehaviour
{
    public bool guards_bool, cats_bool, traps_bool;


    [SerializeField]
    public GameObject guard, cat, trap;

    [SerializeField]
    private bool clickes;

    [SerializeField]
    private Camera Main_Camera;


    private const string ground = "GroundLayer";

    Ray ray;
    RaycastHit hit;

    private void Update()
    {
        throw_ray_to_the_point_of_touch_or_click();
    }

    void throw_ray_to_the_point_of_touch_or_click()
    {
      
        if (Input.GetKeyDown(KeyCode.Mouse0)) // Mouse click (left button)
        {
            Vector2 postionofthemouse = Input.mousePosition;

            ray = Main_Camera.ScreenPointToRay(postionofthemouse);

           
            if(Physics.Raycast(ray, out hit))
            {
                if(hit.collider.gameObject.layer == LayerMask.NameToLayer(ground))
                {
                    Vector2 position = hit.collider.transform.position;

                    spawn_the_guard(position);
                }
            }

        }
       
        
    }

    void spawn_the_guard(Vector2 positon)
    {
        if(guards_bool)
        {
            Instantiate(guard, positon, Quaternion.identity);
        }
        if(cats_bool)
        {

        }
        if(traps_bool)
        {

        }
       
    }


}
