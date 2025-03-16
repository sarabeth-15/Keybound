using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject optionsMenu;
    public static bool IsPaused { get; private set; }
    public static bool IsOptions { get; private set; }

    public static HashSet<KeyCode> blockedKeys = new HashSet<KeyCode>(); 
    public static float resumeM = 0; 

    private void Update() {

        if (resumeM > 0f) {
            resumeM -= Time.unscaledDeltaTime; 
        }

        if (Input.GetKeyDown(KeyCode.Escape)) {

            if (optionsMenu.activeSelf) {
                optionsMenu.SetActive(false);
                pauseMenu.SetActive(true);
                IsOptions = false; 
                return; 
            }

            if (IsPaused) {
                pauseMenu.SetActive(false);
                IsPaused = false;
                Time.timeScale = 1;
                blockedKeys.Clear(); 
            } else {
                pauseMenu.SetActive(true);
                IsPaused = true;
                IsOptions = false; 
                Time.timeScale = 0;
            }

        }

        if (IsPaused && !IsOptions) {

            if (Input.GetKeyDown(KeyCode.T)) {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
                IsPaused = false; 
                Time.timeScale = 1;
            }

            if (Input.GetKeyDown(KeyCode.S)) {
                optionsMenu.SetActive(true);
                IsOptions = true; 
                Time.timeScale = 1; 
            }

            if (Input.GetKeyDown(KeyCode.N)) {
                SceneManager.LoadScene("MainMenu");
                Time.timeScale = 1;
            }

            if (Input.GetKeyDown(KeyCode.R)) {
                pauseMenu.SetActive(false);
                IsPaused = false;
                IsOptions = false; 
                Time.timeScale = 1;
                resumeM = 0.2f; 
            }

        }
        
    }

    public static bool IsKeyBlocked(KeyCode key) {

        if (SceneManager.GetActiveScene().name == "MainMenu") return false; 

        if (IsPaused || IsOptions) return true;
        if (key == KeyCode.R && resumeM > 0f) return true;
        return false; 
    }
    
  

}


