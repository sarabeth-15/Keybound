using UnityEngine;

public class LevelTimer : MonoBehaviour {

    public static LevelTimer Instance { get; private set; }

    private float startTime;
    private float endTime;
    private bool timerRunning = false;

    private void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject); // Keeps it across scenes
    }

    public void StartTimer() {
        startTime = Time.time;
        timerRunning = true;
    }

    public void StopTimer() {
        endTime = Time.time;
        timerRunning = false;
    }

    public float GetElapsedTime() {
        return timerRunning ? Time.time - startTime : endTime - startTime;
    }
}

