using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelectionManager : MonoBehaviour {
    public int currentLevel = 1;
    public int maxUnlockedLevel = 1;

    public Button enterButtoText;
    public Text enterButtonImage;

    public GameObject pointer;

    // Positions for the pointer corresponding to each level
    public Transform[] levelPositions;

    void Start() {
        UpdateUI();
    }

    void Update() {
        HandleInput();
    }

    void HandleInput() {
        if (Input.GetKeyDown(KeyCode.Alpha1)) SelectLevel(1);
        else if (Input.GetKeyDown(KeyCode.Alpha2)) SelectLevel(2);
        else if (Input.GetKeyDown(KeyCode.Alpha3)) SelectLevel(3);
        else if (Input.GetKeyDown(KeyCode.Alpha4)) SelectLevel(4);

        if (Input.GetKeyDown(KeyCode.E) && currentLevel <= maxUnlockedLevel)
            LoadLevel();
    }

    void SelectLevel(int level) {
        currentLevel = level;
        UpdateUI();
    }

    void UpdateUI() {
        // Move pointer to the correct position
        pointer.transform.position = levelPositions[currentLevel - 1].position;

        if (currentLevel <= maxUnlockedLevel) {
            enterButtonText.text = "NTER";
            enterButtonImage.gameObject.SetActive(true); 
        }
        else {
            enterButtonText.text = "LOCKED";
            enterButtonImage.gameObject.SetActive(false);   
        }
    }

    public void LoadLevel() {
        SceneManager.LoadScene("Level" + currentLevel);
    }
}

