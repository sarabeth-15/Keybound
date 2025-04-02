using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelComplete : MonoBehaviour {

    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI levelNameText; 

    void Start() {
        DisplayTime();
        DisplayLevelName(); 
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

    void DisplayLevelName() {
        string sceneName = LevelTracker.PreviousSceneName;

        string displayName;

        switch (sceneName) {
            case "Level1":
                displayName = "LEVEL 1";
                break;
            case "Level2":
                displayName = "LEVEL 2";
                break;
            case "Level3":
                displayName = "LEVEL 3";
                break;
            case "Level4":
                displayName = "LEVEL 4";
                break;
            default:
                displayName = sceneName.ToUpper(); // fallback if unmapped
                break;
        }

        levelNameText.text = displayName;
    }

    public void GoToLevelSelection() {

        string prevScene = LevelTracker.PreviousSceneName;

        int levelNum = 1;

        if (prevScene.StartsWith("Level")) {
            int.TryParse(prevScene.Substring(5), out levelNum); 
        }

        int nextLevel = levelNum + 1;
        int previousUnlocked = PlayerPrefs.GetInt("UnlockedLevel", 1);

        if (nextLevel > previousUnlocked) {
            PlayerPrefs.SetInt("UnlockedLevel", nextLevel);
        }

        PlayerPrefs.SetInt("PointerTargetLevel", nextLevel);

        SceneManager.LoadScene("Level Selection Map");
    }
}
