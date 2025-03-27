using UnityEngine;

public class OptionsCanvasSync : MonoBehaviour {

    [SerializeField] GameObject optionsMenu; 

    private void Update() {
        if (SettingsManager.Instance == null || optionsMenu == null) return;

        if (optionsMenu.activeSelf) {
            // Press O to disable letter overlays 
            if (Input.GetKeyDown(KeyCode.O)) {
                SettingsManager.Instance.SetLetterOverlaysEnabled(false);
                Debug.Log("Letter overlays disabled.");
            }

        }
       
    }
    
}
