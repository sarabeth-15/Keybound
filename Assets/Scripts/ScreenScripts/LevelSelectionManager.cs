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

    public GameObject pointer;
    public Transform[] levelPositions;

    private bool isWaitingToJump = false;
    private float jumpDelay = 0.5f;
    private float jumpTimer = 0f;

    // ?? Add delay to show the locked text first before switching
    private bool showDelayedUnlock = false;
    private float unlockDelay = 0.3f;
    private float unlockTimer = 0f;

    void Start() {
        maxUnlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);

        int savedTargetLevel = PlayerPrefs.GetInt("PointerTargetLevel", 1);
        if (savedTargetLevel > 1) {
            currentLevel = 1;
            pointer.transform.position = levelPositions[0].position;

            currentLevel = savedTargetLevel;
            isWaitingToJump = true;
            jumpTimer = jumpDelay;
        }
        else {
            currentLevel = maxUnlockedLevel;
            pointer.transform.position = levelPositions[currentLevel - 1].position;
            UpdateUI();
        }
    }

    void Update() {
        if (isWaitingToJump) {
            jumpTimer -= Time.deltaTime;

            if (jumpTimer <= 0f) {
                pointer.transform.position = levelPositions[currentLevel - 1].position;
                isWaitingToJump = false;

                // Step 1: Show locked UI first
                ShowLockedUI();

                // Step 2: Then wait and show true unlock state
                showDelayedUnlock = true;
                unlockTimer = unlockDelay;

                PlayerPrefs.DeleteKey("PointerTargetLevel");
            }
        }
        else if (showDelayedUnlock) {
            unlockTimer -= Time.deltaTime;
            if (unlockTimer <= 0f) {
                showDelayedUnlock = false;
                UpdateUI(); // Now show the true unlocked UI
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

    void ShowLockedUI() {
        enterButtonText.gameObject.SetActive(false);
        enterButtonImage.gameObject.SetActive(false);
        lockedButtonText.gameObject.SetActive(true);
    }

    public void LoadLevel() {
        SceneManager.LoadScene("Level" + currentLevel);
    }
}