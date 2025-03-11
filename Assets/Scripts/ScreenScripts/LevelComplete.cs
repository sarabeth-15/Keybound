using UnityEngine;
using UnityEngine.SceneManagement; 

public class LevelComplete : MonoBehaviour {
    void Update() {
        if (Input.GetKeyDown(KeyCode.N)) {
            Debug.Log("Next Level Unavailable");
        }

        if (Input.GetKeyDown(KeyCode.R)) {
            SceneManager.LoadScene("Level0"); 
        }
    }
}
