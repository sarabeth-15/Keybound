using UnityEngine;

public class TogglePlatform : MonoBehaviour
{
    private Collider2D platformCollider;
    private SpriteRenderer spriteRenderer;
    private Vector3 originalScale;
    private Rigidbody2D rb;
    private bool used = false;
    private bool keyWasReleased = true;
    private float inputDelayUntil = 0f; 

    [SerializeField] private AudioClip brickActiveClip;
    [SerializeField] public KeyCode toggleKey;
    [SerializeField] private Sprite brickON;
    [SerializeField] private Sprite brickOFF;
    [SerializeField] public RoomCheck room;
    [SerializeField] public bool falling; 

    private void Start()
    {
        inputDelayUntil = Time.unscaledTime + 0.2f;

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

        if (Time.unscaledTime < inputDelayUntil) {
            return;
        }

        if (InputSuppressor.SuppressInput) {
            return;
        }

        if (PauseMenu.IsKeyBlocked(toggleKey)) {
            return;
        }

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
        bool isKeyUp = Input.GetKeyUp(toggleKey); 

        if (isKeyUp) {
            keyWasReleased = true; 
        }

        if (isKeyDown){
            SoundFXManager.instance.PlaySound(brickActiveClip);
        }

        // Checks type of brick, reacts accordingly
        if (falling) {
            if (keyWasReleased)
                fallingBrick(isKeyHeld, isKeyDown);
        }
        else {
            if (keyWasReleased) {
                platformCollider.enabled = isKeyHeld;
                spriteRenderer.sprite = isKeyHeld ? brickON : brickOFF;
                transform.localScale = isKeyHeld ? originalScale * 1.1f : originalScale;
            }

            if (!isKeyHeld) {
                keyWasReleased = true; // key has been let go, good to allow reactivation
            }
            else if (isKeyDown) {
                keyWasReleased = false; // key is being pressed again
            }
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