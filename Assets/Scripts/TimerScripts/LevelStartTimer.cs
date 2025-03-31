using UnityEngine;

public class LevelStartTimer : MonoBehaviour {
    void Start() {
        if (LevelTimer.Instance != null) {
            LevelTimer.Instance.StartTimer();
        }
        else {
            Debug.LogWarning("LevelTimer instance not found!");
        }
    }
} 