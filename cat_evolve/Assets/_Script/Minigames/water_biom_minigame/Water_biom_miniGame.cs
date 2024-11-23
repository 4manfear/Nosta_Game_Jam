using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water_biom_miniGame : MonoBehaviour
{
    // Lane variables
    public float laneDistance = 2f; // Distance between lanes
    private int targetLane = 1; // Current lane (0: Left, 1: Center, 2: Right)

    // Swipe detection
    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;
    private bool isSwiping = false;
    [SerializeField] private float swipeThreshold = 50f;

    void Update()
    {
        // Detect swipes for lane switching
        DetectSwipe();

        // Smoothly move the player to the target lane
        Vector3 targetPosition = new Vector3(targetLane * laneDistance - laneDistance, transform.position.y, 0);
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 10);
    }

    private void DetectSwipe()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    startTouchPosition = touch.position;
                    isSwiping = true;
                    break;

                case TouchPhase.Ended:
                    endTouchPosition = touch.position;

                    if (isSwiping)
                    {
                        Vector2 swipeVector = endTouchPosition - startTouchPosition;

                        if (Mathf.Abs(swipeVector.x) > swipeThreshold)
                        {
                            if (swipeVector.x > 0) // Right swipe
                            {
                                SwitchLane(1);
                            }
                            else // Left swipe
                            {
                                SwitchLane(-1);
                            }
                        }
                    }

                    isSwiping = false;
                    break;

                case TouchPhase.Canceled:
                    isSwiping = false;
                    break;
            }
        }
    }

    private void SwitchLane(int direction)
    {
        targetLane += direction;
        targetLane = Mathf.Clamp(targetLane, 0, 2); // Restrict to 3 lanes
        Debug.Log("Switched to lane: " + targetLane);
    }
}
