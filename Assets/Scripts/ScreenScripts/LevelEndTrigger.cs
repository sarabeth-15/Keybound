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
        }
        string sceneName = SceneManager.GetActiveScene().name;
        int keys = PlayerInventory.instance != null ? PlayerInventory.instance.keysCollected : 0;
        PlayerPrefs.SetInt("KeysCollected_" + sceneName, keys);

        LevelTracker.PreviousSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("LevelComplete");
    }
}

