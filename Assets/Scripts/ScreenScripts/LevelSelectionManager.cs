using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelectionManager : MonoBehaviour {
    public int currentLevel = 1;
    public int maxUnlockedLevel = 1;

    public TMP_Text enterButtonText;
    public TMP_Text lockedButtonText;
    public Image enterButtonImage;

    [SerializeField] private TMP_Text levelNameText;

    public GameObject pointer;
    public Transform[] levelPositions;

    private bool isWaitingToJump = false;
    private float jumpDelay = 0.5f;
    private float jumpTimer = 0f;

    void Start() {
        maxUnlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);
        int savedTargetLevel = PlayerPrefs.GetInt("PointerTargetLevel", 1);

        if (savedTargetLevel > 1) {
            int previousLevel = savedTargetLevel - 1;
            pointer.transform.position = levelPositions[previousLevel - 1].position;

            currentLevel = savedTargetLevel;
            isWaitingToJump = true;
            jumpTimer = jumpDelay;
        }
        else {
            currentLevel = maxUnlockedLevel;
            pointer.transform.position = levelPositions[currentLevel - 1].position;
            UpdateUI();
            UpdateLevelName();
        }
    }

    void Update() {
        if (isWaitingToJump) {
            jumpTimer -= Time.deltaTime;

            if (jumpTimer <= 0f) {
                pointer.transform.position = levelPositions[currentLevel - 1].position;
                isWaitingToJump = false;
                UpdateUI();
                UpdateLevelName();
                PlayerPrefs.DeleteKey("PointerTargetLevel");
            }
        }
        else {
            HandleInput();
        }
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
        pointer.transform.position = levelPositions[currentLevel - 1].position;
        UpdateUI();
        UpdateLevelName();
    }

    void UpdateUI() {
        if (currentLevel <= maxUnlockedLevel) {
            enterButtonText.gameObject.SetActive(true);
            enterButtonImage.gameObject.SetActive(true);
            lockedButtonText.gameObject.SetActive(false);
        }
        else {
            enterButtonText.gameObject.SetActive(false);
            enterButtonImage.gameObject.SetActive(false);
            lockedButtonText.gameObject.SetActive(true);
        }
    }

    void UpdateLevelName() {
        switch (currentLevel) {
            case 1:
                levelNameText.text = "LEVEL 1";
                break;
            case 2:
                levelNameText.text = "LEVEL 2";
                break;
            case 3:
                levelNameText.text = "LEVEL 3";
                break;
            case 4:
                levelNameText.text = "LEVEL 4";
                break;
            default:
                levelNameText.text = "LEVEL";
                break;
        }
    }

    public void LoadLevel() {
        SceneManager.LoadScene("Level" + currentLevel);
    }
}
