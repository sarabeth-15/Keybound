using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object colliding with the key has the "Player" tag
        if (other.CompareTag("Player"))
        {
            // Call the inventory function to register key collection
            PlayerInventory.instance.CollectKey();
            
            // Destroy the key GameObject after collecting
            Destroy(gameObject);
        }
    }
}
