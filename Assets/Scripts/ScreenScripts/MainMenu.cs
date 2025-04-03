using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    [SerializeField] GameObject optionsMenu;
    [SerializeField] OptionsCanvasSync optionsCanvasSync; 

    void Update() {
        if (Input.GetKeyDown(KeyCode.S)) {
            PauseMenu.ForceUnpause();

            PlayerPrefs.SetInt("StartFromMainMenu", 1); 
            PlayerPrefs.DeleteKey("PointerTargetLevel"); 
            SceneManager.LoadScene("Level Selection Map");
        }

        if (Input.GetKeyDown(KeyCode.O)) {
            optionsMenu.SetActive(true);
            optionsCanvasSync.UpdateOverlayUI();
        }

        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (optionsMenu.activeSelf) {
                optionsMenu.SetActive(false);
            }
        }

        if (Input.GetKeyDown(KeyCode.Q)) {
            QuitGame();
        }
    }

    void QuitGame() {
        Application.Quit();
        Debug.Log("Game has quit.");
    }
}
