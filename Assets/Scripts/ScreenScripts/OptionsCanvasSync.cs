using TMPro;
using UnityEngine;

public class OptionsCanvasSync : MonoBehaviour {

    [SerializeField] GameObject optionsMenu;
    [SerializeField] TMP_Text letterOverlayText;

    private void Update() {
        if (SettingsManager.Instance == null || optionsMenu == null) return;

        if (optionsMenu.activeSelf) {
            // Press O to toggle letter overlays 
            if (Input.GetKeyDown(KeyCode.O)) {
                bool current = SettingsManager.Instance.letterOverlaysEnabled;
                SettingsManager.Instance.SetLetterOverlaysEnabled(!current);

                Debug.Log("Letter overlays toggled: " + !current);
                UpdateOverlayUI();
            }
        }
    }

    private void OnEnable() {
        UpdateOverlayUI();
    }

    public void UpdateOverlayUI() {
        if (letterOverlayText == null) return;

        bool isEnabled = SettingsManager.Instance.letterOverlaysEnabled;
        letterOverlayText.text = isEnabled ? "N" : "FF";
    }
}
