using UnityEngine;

public class InputSuppressor : MonoBehaviour {
    public static bool SuppressInput { get; private set; } = false;
    private static bool suppressNextFrame = false;
    public static float suppressTimer = 0f;

    
    public static void SuppressForOneFrame() {
        suppressNextFrame = true;
        SuppressInput = true;
    }

    
    public static void SuppressForSeconds(float seconds) {
        suppressTimer = seconds;
        SuppressInput = true;
    }

    private void Update() {
        
        if (suppressTimer > 0f) {
            suppressTimer -= Time.unscaledDeltaTime;
            if (suppressTimer <= 0f) {
                suppressTimer = 0f;
                SuppressInput = false;
            }
        }
    }

    private void LateUpdate() {
        
        if (suppressNextFrame) {
            suppressNextFrame = false;
        }
        else if (suppressTimer <= 0f) {
            SuppressInput = false;
        }
    }
}


