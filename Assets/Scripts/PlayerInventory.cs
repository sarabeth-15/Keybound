using UnityEngine;

public class PlayerInventory : MonoBehaviour {
    public static PlayerInventory instance;

    // Number of keys collected in the current level
    public int keysCollected = 0;

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
        else {
            Destroy(gameObject);
        }
    }

    private void Start() {
        // Reset key count at start of each level
        keysCollected = 0;
    }

    public void CollectKey() {
        keysCollected++;
        Debug.Log("Key collected! Total now: " + keysCollected);
    }
}

