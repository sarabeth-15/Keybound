using UnityEngine;

public class LetterOverlay : MonoBehaviour
{
    private SpriteRenderer letterRenderer;
    private TogglePlatform togglePlatform;

    [SerializeField] private Sprite letterSprite;

    private void Start()
    {
        togglePlatform = GetComponentInParent<TogglePlatform>();
        if (togglePlatform == null)
        {
            Debug.LogError("LetterOverlay must be attached to a child of an object with TogglePlatform!");
            return;
        }

        // Create a new object for the letter overlay
        GameObject letterObject = new GameObject("LetterOverlay");
        letterObject.transform.SetParent(transform);
        letterObject.transform.localPosition = Vector3.zero; // Align with the platform

        // Add and configure the sprite renderer
        letterRenderer = letterObject.AddComponent<SpriteRenderer>();
        letterRenderer.sprite = letterSprite;
        letterRenderer.sortingOrder = GetComponentInParent<SpriteRenderer>().sortingOrder + 1; // Ensure it's on top

        // Set initial opacity to 10% (platform starts OFF)
        letterRenderer.color = new Color(1, 1, 1, 0.1f);
    }

    private void Update()
    {
        if (togglePlatform == null) return;

        bool isKeyHeld = Input.GetKey(togglePlatform.toggleKey);

        // Change letter opacity: 50% when active, 10% when inactive
        float letterOpacity = isKeyHeld ? 0.5f : 0.1f;
        letterRenderer.color = new Color(1, 1, 1, letterOpacity);
    }
}
