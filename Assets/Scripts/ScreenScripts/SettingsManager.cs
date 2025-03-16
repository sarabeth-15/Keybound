using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections; 

public class SettingsManager : MonoBehaviour {

    public static SettingsManager Instance { get; private set; }

    public bool letterOverlaysEnabled = true;

    private void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
            return; 
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void SetLetterOverlaysEnabled(bool enabled) {
        letterOverlaysEnabled = enabled;
        LetterOverlay.SyncAllOverlays(enabled);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        // Sync overlays when a new scene is loaded
        LetterOverlay.SyncAllOverlays(letterOverlaysEnabled);
    }

}
