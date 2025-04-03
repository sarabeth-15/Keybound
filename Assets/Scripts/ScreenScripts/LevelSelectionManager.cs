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

    private int lastCompletedLevel = -1;

    [SerializeField] private AudioClip buttonClip;
    [SerializeField] private GameObject bannerLayoutLevel1;
    [SerializeField] private GameObject bannerLayoutStandard;

    [SerializeField] private TMP_Text levelNameText_Level1;
    [SerializeField] private TMP_Text levelNameText_Standard;

    [SerializeField] private Image level1CenterKeyImage; // BannerLayout_Level1/Key2
    [SerializeField] private Image leftKeyImage;         // BannerLayout_Standard/Key1
    [SerializeField] private Image centerKeyImage;       // BannerLayout_Standard/Key2
    [SerializeField] private Image rightKeyImage;        // BannerLayout_Standard/Key3

    [SerializeField] private Sprite keyOn;
    [SerializeField] private Sprite keyOff;

    void Start() {
        // Only reset data if flagged for a new game
        bool isNewGame = PlayerPrefs.GetInt("NewGame", 0) == 1;
        if (isNewGame) {
            for (int i = 1; i <= 4; i++) {
                PlayerPrefs.DeleteKey("KeysCollected_Level" + i);
            }
            PlayerPrefs.DeleteKey("LastCompletedLevel");
            PlayerPrefs.DeleteKey("UnlockedLevel");
            PlayerPrefs.SetInt("NewGame", 0); // clear the flag
        }

        maxUnlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);

        lastCompletedLevel = PlayerPrefs.GetInt("LastCompletedLevel", -1);

        bool startFromMainMenu = PlayerPrefs.GetInt("StartFromMainMenu", 0) == 1;
        PlayerPrefs.DeleteKey("StartFromMainMenu");

        int savedTargetLevel = PlayerPrefs.GetInt("PointerTargetLevel", 1);

        if (startFromMainMenu) {
            currentLevel = 1;
            pointer.transform.position = levelPositions[0].position;
            UpdateUI();
            UpdateLevelName();
            UpdateKeyDisplay(currentLevel);
            return;
        }

        if (savedTargetLevel > 1) {
            int previousLevel = savedTargetLevel - 1;
            pointer.transform.position = levelPositions[previousLevel - 1].position;

            currentLevel = savedTargetLevel;
            isWaitingToJump = true;
            jumpTimer = jumpDelay;

            if (lastCompletedLevel != -1) {
                UpdateKeyDisplay(lastCompletedLevel); // Show keys from last completed level during jump
            }
        }
        else {
            currentLevel = maxUnlockedLevel;
            pointer.transform.position = levelPositions[currentLevel - 1].position;
            UpdateUI();
            UpdateLevelName();
            UpdateKeyDisplay(currentLevel);
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
                UpdateKeyDisplay(currentLevel);
                PlayerPrefs.DeleteKey("PointerTargetLevel");
            }
        }
        else {
            HandleInput();
        }
    }

    void HandleInput() {
        if (Input.GetKeyDown(KeyCode.Alpha1)) {SelectLevel(1); SoundFXManager.instance.PlaySound(buttonClip);}
        else if (Input.GetKeyDown(KeyCode.Alpha2)) {SelectLevel(2); SoundFXManager.instance.PlaySound(buttonClip);}
        else if (Input.GetKeyDown(KeyCode.Alpha3)) {SelectLevel(3); SoundFXManager.instance.PlaySound(buttonClip);}
        else if (Input.GetKeyDown(KeyCode.Alpha4)) {SelectLevel(4); SoundFXManager.instance.PlaySound(buttonClip);}

        if (Input.GetKeyDown(KeyCode.E) && currentLevel <= maxUnlockedLevel)
            SoundFXManager.instance.PlaySound(buttonClip);
            LoadLevel();
    }

    void SelectLevel(int level) {
        currentLevel = level;
        pointer.transform.position = levelPositions[currentLevel - 1].position;

        UpdateUI();
        UpdateLevelName();
        UpdateKeyDisplay(currentLevel); // Show keys for selected level
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
        string name = currentLevel switch {
            1 => "LEVEL 1",
            2 => "LEVEL 2",
            3 => "LEVEL 3",
            4 => "LEVEL 4",
            _ => "LEVEL"
        };

        levelNameText_Level1.text = name;
        levelNameText_Standard.text = name;
    }

    void UpdateKeyDisplay(int levelToDisplay) {
        string levelName = "Level" + levelToDisplay;
        int keysCollected = PlayerPrefs.GetInt("KeysCollected_" + levelName, 0);

        bool isLevel1 = levelToDisplay == 1;

        bannerLayoutLevel1.SetActive(isLevel1);
        bannerLayoutStandard.SetActive(!isLevel1);

        if (isLevel1) {
            level1CenterKeyImage.sprite = (keysCollected >= 1) ? keyOn : keyOff;
        }
        else {
            leftKeyImage.sprite = (keysCollected >= 1) ? keyOn : keyOff;
            rightKeyImage.sprite = (keysCollected >= 2) ? keyOn : keyOff;
            centerKeyImage.sprite = (keysCollected >= 3) ? keyOn : keyOff;
        }
    }

    public void LoadLevel() {
        SceneManager.LoadScene("Level" + currentLevel);
    }
}
