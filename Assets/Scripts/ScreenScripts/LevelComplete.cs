using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelComplete : MonoBehaviour {

    [SerializeField] private GameObject levelCompleteImage; 
    [SerializeField] private TextMeshProUGUI timeText; 

    public void Show(float finalTime) {
        levelCompleteImage.SetActive(true); 
        timeText.text = "Time: " + finalTime.ToString("F2") + " seconds";
    }

    void Update() {

        if (Input.GetKeyDown(KeyCode.N)) {
            Debug.Log("Next Level Unavailable");
        }

        if (Input.GetKeyDown(KeyCode.R)) {
            SceneManager.LoadScene("Level0"); 
        }
    }
}
