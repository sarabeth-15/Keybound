using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    [SerializeField] private AudioClip buttonClip;
    [SerializeField] GameObject optionsMenu;
    [SerializeField] OptionsCanvasSync optionsCanvasSync; 

    void Update() {
        if (Input.GetKeyDown(KeyCode.S)) {
            SoundFXManager.instance.PlaySound(buttonClip);
            PauseMenu.ForceUnpause();

            PlayerPrefs.SetInt("StartFromMainMenu", 1); 
            PlayerPrefs.DeleteKey("PointerTargetLevel"); 
            SceneManager.LoadScene("Level Selection Map");
        }

        if (Input.GetKeyDown(KeyCode.O)) {
            SoundFXManager.instance.PlaySound(buttonClip);
            optionsMenu.SetActive(true);
            optionsCanvasSync.UpdateOverlayUI();
        }

        if (Input.GetKeyDown(KeyCode.Escape)) {
            SoundFXManager.instance.PlaySound(buttonClip);
            if (optionsMenu.activeSelf) {
                optionsMenu.SetActive(false);
            }
        }

        if (Input.GetKeyDown(KeyCode.Q)) {
            SoundFXManager.instance.PlaySound(buttonClip);
            QuitGame();
        }
    }

    void QuitGame() {
        Application.Quit();
        Debug.Log("Game has quit.");
    }
}
