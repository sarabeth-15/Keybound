using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsManager : MonoBehaviour {

    public static SettingsManager Instance { get; private set; }

    public bool letterOverlaysEnabled = true;

    [Header("Audio Settings")]
    [Range(0f, 1f)]
    public float musicVolume = 0.5f;
    [SerializeField] private AudioSource musicSource;

    private void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;

        // Apply volume at startup
        ApplyMusicVolume();
    }

    private void Start() {
        ApplyMusicVolume(); // In case AudioSource starts in Start()
    }

    public void SetLetterOverlaysEnabled(bool enabled) {
        letterOverlaysEnabled = enabled;
        LetterOverlay.SyncAllOverlays(enabled);
    }

    public void SetMusicVolume(float value) {
        musicVolume = Mathf.Clamp01(value);
        ApplyMusicVolume();
    }

    private void ApplyMusicVolume() {
        if (musicSource != null) {
            musicSource.volume = musicVolume;
            Debug.Log($"Volume applied: {musicVolume}"); 
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        LetterOverlay.SyncAllOverlays(letterOverlaysEnabled);
        ApplyMusicVolume(); // Reapply volume in case audio resets on scene load
    }
}

