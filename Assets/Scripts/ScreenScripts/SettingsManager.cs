using UnityEngine;

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
    }

    public void SetLetterOverlaysEnabled(bool enabled) {
        letterOverlaysEnabled = enabled;
    }

}
