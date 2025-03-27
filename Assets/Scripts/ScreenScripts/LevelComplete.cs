using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelComplete : MonoBehaviour {

    [SerializeField] private TextMeshProUGUI timeText;

    void Start() {
        DisplayTime();
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
}
