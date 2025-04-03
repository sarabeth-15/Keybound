using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class InputSuppressor : MonoBehaviour {
    public static bool SuppressInput { get; private set; } = false;

    private static InputSuppressor instance;

    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable() {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable() {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        SuppressInput = false;
    }

    public static void SuppressForSeconds(float seconds) {
        if (instance != null) {
            instance.StartCoroutine(instance.ClearAfterDelay(seconds));
        }
    }

    private IEnumerator ClearAfterDelay(float seconds) {
        SuppressInput = true;
        yield return new WaitForSecondsRealtime(seconds);
        SuppressInput = false;
    }

    public static void SuppressForOneFrame() {
        if (instance != null) {
            instance.StartCoroutine(instance.ClearAfterDelay(0.02f));
        }
    }
}

