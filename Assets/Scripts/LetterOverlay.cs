using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class LetterOverlay : MonoBehaviour {
    private SpriteRenderer letterRenderer;
    private GameObject letterObject;
    private TogglePlatform togglePlatform;
    private RoomCheck room;

    [SerializeField] private Sprite letterSprite;
    [SerializeField] private Color32 colorOFF;
    [SerializeField] private Color32 colorON;

    private float inputDelayUntil = 0f;

    private void Start() {

        togglePlatform = GetComponentInParent<TogglePlatform>();

        if (togglePlatform == null) {
            Debug.LogError("LetterOverlay must be attached to a child of an object with TogglePlatform!");
            return;
        }

        room = togglePlatform.room;

        // Create a new object for the letter overlay
        letterObject = new GameObject("LetterOverlay");
        letterObject.transform.SetParent(transform);
        letterObject.transform.localPosition = Vector3.zero;

        // Add and configure the sprite renderer
        letterRenderer = letterObject.AddComponent<SpriteRenderer>();
        letterRenderer.sprite = letterSprite;
        letterRenderer.sortingOrder = GetComponentInParent<SpriteRenderer>().sortingOrder + 1;

        colorOFF = new Color32(32, 25, 29, 255);
        colorON = new Color32(48, 37, 44, 255);
        letterRenderer.color = colorOFF;

        inputDelayUntil = Time.unscaledTime + 0.2f; 

        StartCoroutine(InitializeOverlayVisibility());
    }

    private void Update() {
        if (togglePlatform == null || room == null || letterRenderer == null) return;

        if (SettingsManager.Instance != null && letterObject != null) {
            letterObject.SetActive(SettingsManager.Instance.letterOverlaysEnabled);
        }

        StartCoroutine(UpdateOverlayColorNextFrame());
    }

    private IEnumerator UpdateOverlayColorNextFrame() {
        yield return null; // wait 1 frame so key input has time to clear

        if (Time.unscaledTime < inputDelayUntil) yield break;
        if (InputSuppressor.SuppressInput) yield break; 
        if (PauseMenu.IsKeyBlocked(togglePlatform.toggleKey)) yield break;

        if (!room.playerInRoom) {
            letterRenderer.color = colorOFF;
            yield break;
        }

        bool isKeyHeld = Input.GetKey(togglePlatform.toggleKey);
        Color32 currentColor = isKeyHeld ? colorON : colorOFF;
        letterRenderer.color = currentColor;
    }

    public static void SyncAllOverlays(bool enabled) {
        var overlays = Object.FindObjectsByType<LetterOverlay>(FindObjectsSortMode.None);
        foreach (var overlay in overlays) {
            if (overlay.letterObject != null) {
                overlay.letterObject.SetActive(enabled);
            }
        }
    }

    private IEnumerator InitializeOverlayVisibility() {
        yield return null; // wait one frame
        if (SettingsManager.Instance != null && letterObject != null) {
            letterObject.SetActive(SettingsManager.Instance.letterOverlaysEnabled);
        }
    }
}

