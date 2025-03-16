using UnityEngine;

public class LevelEndTimer : MonoBehaviour
{
    private bool completed = false;

    private void OnTriggerEnter2D(Collider2D other) {
        if (completed) return;

        if (other.CompareTag("Player")) {
            completed = true;

            if (LevelTimer.Instance != null) {
                LevelTimer.Instance.StopTimer();
                float finalTime = LevelTimer.Instance.GetElapsedTime();
                Debug.Log("Level Complete! Time: " + finalTime.ToString("F2") + "s");

                Object.FindFirstObjectByType<LevelComplete>()?.Show(finalTime);
            }
        }
    }
}
