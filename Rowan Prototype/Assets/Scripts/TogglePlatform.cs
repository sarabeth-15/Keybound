using UnityEngine;

public class TogglePlatform : MonoBehaviour
{
    private Collider2D platformCollider;
    private SpriteRenderer spriteRenderer;
    private SpriteRenderer letterRenderer; // Renderer for the letter sprite
    private Vector3 originalScale;

    [SerializeField] private KeyCode toggleKey;
    [SerializeField] private Sprite brickON;
    [SerializeField] private Sprite brickOFF;
    [SerializeField] private Sprite letterSprite; // Overlay letter sprite

    private void Start()
    {
        platformCollider = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalScale = transform.localScale;

        platformCollider.enabled = false;

        if (spriteRenderer != null)
        {
            spriteRenderer.sprite = brickOFF;
        }

        // Create a child object to overlay the letter
        GameObject letterObject = new GameObject("LetterOverlay");
        letterObject.transform.SetParent(transform);
        letterObject.transform.localPosition = Vector3.zero; // Align with the platform

        // Add a SpriteRenderer for the letter
        letterRenderer = letterObject.AddComponent<SpriteRenderer>();
        letterRenderer.sprite = letterSprite;
        letterRenderer.sortingOrder = spriteRenderer.sortingOrder + 1; // Ensure it's on top

        // Set initial opacity to 10% (since platform is OFF)
        letterRenderer.color = new Color(1, 1, 1, 0.1f);
    }

    private void Update()
    {
        if (toggleKey == KeyCode.None) return;

        bool isKeyHeld = Input.GetKey(toggleKey);
        platformCollider.enabled = isKeyHeld;

        if (spriteRenderer != null)
        {
            spriteRenderer.sprite = isKeyHeld ? brickON : brickOFF;
        }

        // Change letter opacity: 50% when active, 10% when inactive
        float letterOpacity = isKeyHeld ? 0.5f : 0.1f;
        letterRenderer.color = new Color(1, 1, 1, letterOpacity);

        // Scale up when active, reset when inactive
        transform.localScale = isKeyHeld ? originalScale * 1.1f : originalScale;
    }
}
