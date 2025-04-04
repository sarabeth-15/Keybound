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
    private PlatformEffector2D effector;

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
        effector = GetComponent<PlatformEffector2D>(); // Get Platform Effector component

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
        if (Time.unscaledTime < inputDelayUntil)
            return;
    
        if (InputSuppressor.SuppressInput)
            return;

        if (PauseMenu.IsKeyBlocked(toggleKey))
            return;

        // By default, disables any bricks that are off-screen, in a different room, or not assigned a key
        if (toggleKey == KeyCode.None || (!spriteRenderer.isVisible && rb.bodyType != RigidbodyType2D.Dynamic)) return;

        if (!room.playerInRoom) {
            if (falling && used && rb != null) {
                // Immediately make the brick fall when the player exits the room
                rb.bodyType = RigidbodyType2D.Dynamic;
                rb.gravityScale = 5;
                rb.mass = 300;
                spriteRenderer.sprite = brickOFF;
                transform.localScale = originalScale;
                spriteRenderer.sortingOrder += 1;
            }
            else {
                platformCollider.enabled = false;
                spriteRenderer.sprite = brickOFF;
                transform.localScale = originalScale;
            }
            return;
        }

        // True if key is being held down, false otherwise
        bool isKeyHeld = Input.GetKey(toggleKey);
        bool isKeyDown = Input.GetKeyDown(toggleKey);
        bool isKeyUp = Input.GetKeyUp(toggleKey);

        if (isKeyUp)
            keyWasReleased = true;

        if (isKeyDown)
            SoundFXManager.instance.PlaySound(brickActiveClip);

        if (falling) {
            if (keyWasReleased)
                fallingBrick(isKeyHeld, isKeyDown);
        }
        else
        {
            if (keyWasReleased) {
                platformCollider.enabled = isKeyHeld;
                spriteRenderer.sprite = isKeyHeld ? brickON : brickOFF;
                transform.localScale = isKeyHeld ? originalScale * 1.1f : originalScale;
            }

            if (!isKeyHeld) 
                keyWasReleased = true;
            
            else if (isKeyDown)
                keyWasReleased = false;
            
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
    private void OnCollisionEnter2D(Collision2D collision) {
        if (falling && collision.collider.CompareTag("Player")) {
            Vector2 contactPoint = collision.GetContact(0).point;
            Vector2 playerTop = collision.collider.bounds.center + new Vector3(0, collision.collider.bounds.extents.y, 0);

            if (contactPoint.y > playerTop.y) {
                rb.constraints = RigidbodyConstraints2D.None; // Allow rotation
                rb.linearVelocity = new Vector2(Random.Range(-2f, 2f), rb.linearVelocity.y); // Apply a slight push
            }
        }

        if (falling && collision.collider.CompareTag("Ground")) 
            effector.enabled = false; // Disable the slippery effect when the brick hits the ground  
    }

    private void OnCollisionExit2D(Collision2D collision) {
        if (falling && collision.collider.CompareTag("Ground")) 
            effector.enabled = true; // Re-enable the slippery effect once the brick leaves the ground
    }
}