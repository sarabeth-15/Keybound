using TMPro;
using UnityEngine;

public class OptionsCanvasSync : MonoBehaviour {

    [SerializeField] GameObject optionsMenu;
    [SerializeField] TMP_Text letterOverlayText;


    [SerializeField] Transform musicBarStart; 
    [SerializeField] Transform musicBarEnd;
    [SerializeField] private Transform musicTriangle; 
    [SerializeField] private float volumeStep = 0.1f;

    private void Update() {
        if (SettingsManager.Instance == null || optionsMenu == null) return;

        if (optionsMenu.activeSelf) {
            // Press O to toggle letter overlays 
            if (Input.GetKeyDown(KeyCode.O)) {
                bool current = SettingsManager.Instance.letterOverlaysEnabled;
                SettingsManager.Instance.SetLetterOverlaysEnabled(!current);

                UpdateOverlayUI();
            }
        }

        if (Input.GetKeyDown(KeyCode.M)) {
            float currentVolume = SettingsManager.Instance.musicVolume; 
            currentVolume += volumeStep;

            if (currentVolume > 1f)
                currentVolume = 0f;

            SettingsManager.Instance.SetMusicVolume(currentVolume); 
            UpdateMusicSlider(currentVolume);
        }
    }

    private void UpdateMusicSlider(float volume) {
        if (musicTriangle == null || musicBarStart == null || musicBarEnd == null) return;

        Vector3 start = musicBarStart.position;
        Vector3 end = musicBarEnd.position;
        Vector3 target = Vector3.Lerp(start, end, volume);

        // Lock Y and Z position to the triangle's original value
        target.y = musicTriangle.position.y;
        target.z = musicTriangle.position.z;

        musicTriangle.position = target;
    }

    private void OnEnable() {
        UpdateOverlayUI();
        UpdateMusicSlider(SettingsManager.Instance.musicVolume);
    }

    public void UpdateOverlayUI() {
        if (letterOverlayText == null) return;

        bool isEnabled = SettingsManager.Instance.letterOverlaysEnabled;
        letterOverlayText.text = isEnabled ? "FF" : "N";
    }
}
