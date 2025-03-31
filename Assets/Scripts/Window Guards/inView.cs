using UnityEngine;
using UnityEngine.SceneManagement;

public class InView : MonoBehaviour
{
    public bool guardSpot = false; // Is player spotted?
    public bool triggerZone = false; //Is player in the danger zone (may be spotted)?
    private GuardMovement guard; //reference to the GuardMovement script

    void Start()
    {
        guard = FindFirstObjectByType<GuardMovement>(); // Finds the guard if only one exists
    }

    // Called when the player enters the trigger zone
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player entered trigger zone!");
            triggerZone = true;
            CheckForSpotting(); //check if player gets spotted as soon as they enter
        }
    }

    //called every frame the player is inside the trigger zone to constantly check if the player is spotted
    void OnTriggerStay2D(Collider2D collision)
    {
        if (triggerZone)
        {
            CheckForSpotting(); // Continuously check for spotting as long as the player is inside
        }
    }

    // Check if the player gets spotted based on guard being in the window
    private void CheckForSpotting()
    {
        if (guard == null) return;

        // If the guard is in the window, spot the player
        if (guard.window)
        {
            guardSpot = true;
            Debug.Log("Player Spotted!");
            // Restart the level
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            guardSpot = false;
            triggerZone = false;
            Debug.Log("Player left trigger zone.");
        }
    }
}
