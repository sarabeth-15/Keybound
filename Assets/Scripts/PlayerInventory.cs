using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    // Singleton instance to access from other scripts
    public static PlayerInventory instance;

    // Variable to check if the player has collected the key
    public int keysCollected = 0;


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
        keysCollected += 1; // Set key status to collected
        Debug.Log("Key collected! Total: " + keysCollected); // Output message for testing
    }
}
