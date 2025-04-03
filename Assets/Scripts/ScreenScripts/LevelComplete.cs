using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelComplete : MonoBehaviour {

    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI levelNameText; 
    [SerializeField] private AudioClip buttonClip;
    [SerializeField] private AudioClip completeClip;

    void Start() {
        DisplayTime();
        DisplayLevelName(); 
        SoundFXManager.instance.PlaySound(completeClip);
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.R)) {
            SoundFXManager.instance.PlaySound(buttonClip);
            SceneManager.LoadScene("Level1"); // Restart current level
        }

        if (Input.GetKeyDown(KeyCode.N)) {
            SoundFXManager.instance.PlaySound(buttonClip);
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
        string levelDisplay = ""; 

        switch (sceneName) {
            case "Level1": levelDisplay = "LEVEL 1"; break;
            case "Level2": levelDisplay = "LEVEL 2"; break;
            case "Level3": levelDisplay = "LEVEL 3"; break;
            case "Level4": levelDisplay = "LEVEL 4"; break;
            default: levelDisplay = "LEVEL"; break;
        }

        levelNameText.text = levelDisplay;
    }


    public void GoToLevelSelection() {

        string prevScene = LevelTracker.PreviousSceneName;

        int levelNum = 1;

        if (prevScene.StartsWith("Level")) {
            int.TryParse(prevScene.Substring(5), out levelNum); 
        }

        int nextLevel = Mathf.Min(levelNum + 1, 4); 
        int previousUnlocked = PlayerPrefs.GetInt("UnlockedLevel", 1);

        if (nextLevel > previousUnlocked) {
            PlayerPrefs.SetInt("UnlockedLevel", nextLevel);
        }

        PlayerPrefs.SetInt("PointerTargetLevel", nextLevel);

        SceneManager.LoadScene("Level Selection Map");
    }
}
