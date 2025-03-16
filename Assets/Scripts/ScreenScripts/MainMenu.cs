using UnityEngine;
using UnityEngine.SceneManagement; 

public class MainMenu : MonoBehaviour {

    [SerializeField] GameObject optionsMenu;

    void Update() {
        if (Input.GetKeyDown(KeyCode.S)) {
            SceneManager.LoadScene("Level0"); 
        }

        if (Input.GetKeyDown(KeyCode.O)) {
            optionsMenu.SetActive(true);    
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
