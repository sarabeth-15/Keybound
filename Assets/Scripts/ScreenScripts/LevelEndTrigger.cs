using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEndTrigger : MonoBehaviour {

    private bool completed = false;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (completed) return;

        if (collision.CompareTag("Player")) {
            completed = true;

            if (LevelTimer.Instance != null) {
                LevelTimer.Instance.StopTimer();
                LevelResult.finalTime = LevelTimer.Instance.GetElapsedTime();
            }

            // Save the number of keys collected in this level
            string levelName = SceneManager.GetActiveScene().name;
            int keys = PlayerInventory.instance != null ? PlayerInventory.instance.keysCollected : 0;
            PlayerPrefs.SetInt("KeysCollected_" + levelName, keys);

            // Mark this as the last completed level
            PlayerPrefs.SetInt("LastCompletedLevel", GetLevelNumberFromSceneName(levelName));

            LevelTracker.PreviousSceneName = levelName; 

            SceneManager.LoadScene("LevelComplete");
        }
    }

    private int GetLevelNumberFromSceneName(string sceneName) {
        if (sceneName == "Level1") return 1;
        if (sceneName == "Level2") return 2;
        if (sceneName == "Level3") return 3;
        if (sceneName == "Level4") return 4;
        return -1; // fallback
    }
}


