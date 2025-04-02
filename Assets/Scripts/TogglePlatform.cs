using UnityEngine;

public class TogglePlatform : MonoBehaviour
{
    private Collider2D platformCollider;
    private SpriteRenderer spriteRenderer;
    private Vector3 originalScale;
    private Rigidbody2D rb;
    private bool used = false;

    [SerializeField] public KeyCode toggleKey;
    [SerializeField] private Sprite brickON;
    [SerializeField] private Sprite brickOFF;
    [SerializeField] public RoomCheck room;
    [SerializeField] public bool falling; 

    private void Start()
    {
        platformCollider = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        // Brick starts with disabled sprite, collider, and gravity
        platformCollider.enabled = false;
        originalScale = transform.localScale;
        if (rb != null) {
            rb.bodyType = RigidbodyType2D.Kinematic;
            rb.linearVelocity = Vector2.zero;
        }
        spriteRenderer.sprite = brickOFF;
    }
    private void Update() {

        if (InputSuppressor.SuppressInput) return;
        if (PauseMenu.IsKeyBlocked(toggleKey)) return;

        // By default, disables any bricks that are off-screen, in a different room, or not assigned a key
        if (toggleKey == KeyCode.None || (!spriteRenderer.isVisible && rb.bodyType != RigidbodyType2D.Dynamic)) return;
        if (!room.playerInRoom && rb.bodyType != RigidbodyType2D.Dynamic) {
            platformCollider.enabled = false;
            spriteRenderer.sprite = brickOFF;
            transform.localScale = originalScale;
            return;
        }

        // True if key is being held down, false otherwise
        bool isKeyHeld = Input.GetKey(toggleKey);
        bool isKeyDown = Input.GetKeyDown(toggleKey);

        // Checks type of brick, reacts accordingly
        if (falling) {
            fallingBrick(isKeyHeld, isKeyDown);
        }
        else {
            platformCollider.enabled = isKeyHeld;
            spriteRenderer.sprite = isKeyHeld ? brickON : brickOFF;
            transform.localScale = isKeyHeld ? originalScale * 1.1f : originalScale;
        }
    }
    private void fallingBrick(bool isKeyHeld, bool isKeyDown) {
        if (!used && isKeyDown) {
            used = true;
            platformCollider.enabled = true;
            spriteRenderer.sprite = brickON;
            transform.localScale = originalScale * 1.1f;
        }
        else if (used && !isKeyHeld && rb != null) {
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.gravityScale = 5;
            rb.mass = 300;
            spriteRenderer.sprite = brickOFF;
            transform.localScale = originalScale;
            spriteRenderer.sortingOrder += 1;
        }
    }
}