using UnityEngine;

public class OptionsCanvasSync : MonoBehaviour {

    private void Update() {
        if (SettingsManager.Instance == null) return;

        // Press F to disable letter overlays 
        if (Input.GetKeyDown(KeyCode.F)) {
            SettingsManager.Instance.SetLetterOverlaysEnabled(false);
            Debug.Log("Letter overlays disabled."); 
        }

        // Press O to enable letter overlays
        if (Input.GetKeyDown(KeyCode.N)) {
            SettingsManager.Instance.SetLetterOverlaysEnabled(true);
            Debug.Log("Letter overlays enabled."); 
        }
    }
    
}
