using UnityEngine;
using UnityEngine.SceneManagement; 

public class MainMenu : MonoBehaviour {
    public void GoToScene(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }
}
