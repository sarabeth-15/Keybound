using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    public static bool IsPaused { get; private set; }

    public static HashSet<KeyCode> blockedKeys = new HashSet<KeyCode>();
    public static float resumeM = 0; 

    private void Update() {

        if (resumeM > 0f) {
            resumeM -= Time.unscaledDeltaTime; 
        }

        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (IsPaused) {
                pauseMenu.SetActive(false);
                IsPaused = false;
                Time.timeScale = 1;
                blockedKeys.Clear(); 
            } else {
                pauseMenu.SetActive(true);
                IsPaused = true;
                Time.timeScale = 0;
            }
             
        }

        if (IsPaused) {

            if (Input.GetKeyDown(KeyCode.R)) {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
                IsPaused = false; 
                Time.timeScale = 1;
            }

            if (Input.GetKeyDown(KeyCode.A)) {
                SceneManager.LoadScene("MainMenu");
                Time.timeScale = 1;
            }

            if (Input.GetKeyDown(KeyCode.M)) {
                pauseMenu.SetActive(false);
                IsPaused = false;
                Time.timeScale = 1;
                resumeM = 0.2f; 
            }

        }
        
    }

    public static bool IsKeyBlocked(KeyCode key) {
        if (IsPaused) return true;
        if (key == KeyCode.M && resumeM > 0f) return true;
        return false; 
    }
    
  

}


