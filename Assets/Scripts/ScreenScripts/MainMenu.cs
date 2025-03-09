using UnityEngine;
using UnityEngine.SceneManagement; 

public class MainMenu : MonoBehaviour {
    void Update() {
        if (Input.GetKeyDown(KeyCode.T)) {
            SceneManager.LoadScene("Level0"); 
        }

        if (Input.GetKeyDown(KeyCode.S)) {
            SceneManager.LoadScene("Settings"); 
        }

        if (Input.GetKeyDown(KeyCode.V)) {
            QuitGame();
        }    
    }

    void QuitGame() {
        Application.Quit();
        Debug.Log("Game has quit."); 
    }
}
