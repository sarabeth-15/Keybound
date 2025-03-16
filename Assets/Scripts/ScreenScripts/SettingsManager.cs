using UnityEngine;

public class SettingsManager : MonoBehaviour {

    public static SettingsManager Instance { get; private set; }

    public bool letterOverlaysEnabled = true;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
    }

    public void SetLetterOverlaysEnabled(bool enabled) {
        letterOverlaysEnabled = enabled;
        Debug.Log("Letter overlays " + (enabled ? "enabled" : "disabled"));
    }

}
