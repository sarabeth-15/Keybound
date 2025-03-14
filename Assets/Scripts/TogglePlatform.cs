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
        rb.isKinematic = true; 
        rb.linearVelocity = Vector2.zero;        
        spriteRenderer.sprite = brickOFF;
    }
    private void Update()
    {
        // By default, disables any bricks that are off-screen, in a different room, or not assigned a key
        if (toggleKey == KeyCode.None || !spriteRenderer.isVisible) return;
        if (!room.playerInRoom) 
        {
            platformCollider.enabled = false;
            spriteRenderer.sprite = brickOFF;
            transform.localScale = originalScale;
            return;
        }

        // True if key is being held down, false otherwise
        bool isKeyHeld = Input.GetKey(toggleKey);

        // Checks type of brick, reacts accordingly
        if (falling) fallingBrick(isKeyHeld); 
        else {
            platformCollider.enabled = isKeyHeld;
            spriteRenderer.sprite = isKeyHeld ? brickON : brickOFF;
            transform.localScale = isKeyHeld ? originalScale * 1.1f : originalScale;
        }
    }
    private void fallingBrick(bool isKeyHeld)
    {
        // If key pressed, platform is turned on but doesn't fall
        if (!used && isKeyHeld)
        {
            used = true;
            platformCollider.enabled = true;
            spriteRenderer.sprite = brickON;
            transform.localScale = originalScale * 1.1f;
        }
        // When key is released, brick falls
        else if (used && !isKeyHeld)
        {
            rb.isKinematic = false;
            rb.gravityScale = 5;                            
            rb.mass = 300;                          //CHANGE MASS OF FALLING BRICK HERE
            spriteRenderer.sprite = brickOFF;
            transform.localScale = originalScale;
            spriteRenderer.sortingOrder += 1; 
        }
    }
}