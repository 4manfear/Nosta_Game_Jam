using UnityEngine;
using UnityEngine.SceneManagement; // Only if you want to load a new scene

public class TapToContinue : MonoBehaviour
{
    public string nextSceneName; // Set the name of the next scene in the Inspector

    void Update()
    {
        // Check if there is at least one touch on the screen
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // Check if the touch has just begun
            if (touch.phase == TouchPhase.Began)
            {
                // Call the Continue function to proceed
                Continue();
            }
        }
    }

    void Continue()
    {
        // Optional: Load the next scene, disable UI, or trigger your next action
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            Debug.Log("Tapped to continue!");
        }
    }
}
