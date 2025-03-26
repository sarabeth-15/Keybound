using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    // Singleton instance to access from other scripts
    public static PlayerInventory instance;

    // Variable to check if the player has collected the key
    public bool hasKey = false;

    private void Awake()
    {
        // Ensure only one instance of PlayerInventory exists
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void CollectKey()
    {
        hasKey = true; // Set key status to collected
        Debug.Log("Key collected!"); // Output message for testing
    }
}
