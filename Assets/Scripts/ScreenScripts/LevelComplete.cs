using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelComplete : MonoBehaviour {

    [SerializeField] private TextMeshProUGUI timeText;

    void Start() {
        DisplayTime();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.R)) {
            SceneManager.LoadScene("Level1"); // Restart current level
        }

        if (Input.GetKeyDown(KeyCode.N)) {
            GoToLevelSelection();
        }
    }

    void DisplayTime() {
        // Get the final time stored in LevelResult
        float time = LevelResult.finalTime;

        // Convert time to minutes and seconds
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);

        // Format and display the time in the UI
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void GoToLevelSelection() {
       
        int nextLevel = 2;
        int previousUnlocked = PlayerPrefs.GetInt("UnlockedLevel", 1);

        if (nextLevel > previousUnlocked) {
            PlayerPrefs.SetInt("UnlockedLevel", nextLevel);
        }

        PlayerPrefs.SetInt("PointerTargetLevel", nextLevel);
        SceneManager.LoadScene("Level Selection Map");
    }
}
