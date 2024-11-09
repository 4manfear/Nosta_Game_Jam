using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_movement : MonoBehaviour
{
    [SerializeField] private float zoomSpeed = 0.5f;       // Speed of camera zoom
    [SerializeField] private float minZoom = 5f;           // Minimum zoom level
    [SerializeField] private float maxZoom = 20f;          // Maximum zoom level
    [SerializeField] private float dragSpeed = 0.1f;       // Speed of camera drag movement

    private Vector3 dragOrigin;                            // Origin point for camera drag
    private Camera cam;                                    // Camera component

    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        HandleCameraZoom();
        HandleCameraDrag();
    }

    // Method to handle pinch zoom
    private void HandleCameraZoom()
    {
        // Check if two fingers are touching the screen
        if (Input.touchCount == 2)
        {
            Touch touch0 = Input.GetTouch(0);
            Touch touch1 = Input.GetTouch(1);

            // Find the position in the previous frame of each touch
            Vector2 touch0PrevPos = touch0.position - touch0.deltaPosition;
            Vector2 touch1PrevPos = touch1.position - touch1.deltaPosition;

            // Calculate the previous and current distances between touches
            float prevTouchDeltaMag = (touch0PrevPos - touch1PrevPos).magnitude;
            float currentTouchDeltaMag = (touch0.position - touch1.position).magnitude;

            // Calculate the difference in the distances
            float deltaMagnitudeDiff = prevTouchDeltaMag - currentTouchDeltaMag;

            // Adjust camera's orthographic size based on the distance between touches
            cam.orthographicSize += deltaMagnitudeDiff * zoomSpeed;

            // Clamp the camera's zoom level
            cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, minZoom, maxZoom);
        }
    }

    // Method to handle camera drag
    private void HandleCameraDrag()
    {
        // Check if there is one touch on the screen
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            // When the touch begins, record the starting position
            if (touch.phase == TouchPhase.Began)
            {
                dragOrigin = cam.ScreenToWorldPoint(touch.position);
            }
            // When the touch is moving, adjust camera position based on drag distance
            else if (touch.phase == TouchPhase.Moved)
            {
                Vector3 difference = dragOrigin - cam.ScreenToWorldPoint(touch.position);
                cam.transform.position += difference * dragSpeed;
            }
        }
    }
}
