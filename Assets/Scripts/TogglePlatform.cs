using UnityEngine;

public class TogglePlatform : MonoBehaviour
{
    private Collider2D platformCollider;
    private SpriteRenderer spriteRenderer;
    private Vector3 originalScale;
    [SerializeField] public KeyCode toggleKey;
    [SerializeField] private Sprite brickON;
    [SerializeField] private Sprite brickOFF;
    [SerializeField] public RoomCheck room;
    

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
    }

    private void Update()
    {
        if (toggleKey == KeyCode.None) return;

        bool isKeyHeld = false;

        if (!room.playerInRoom) return;
        
        if (room.playerInRoom) // Only run if player is in the room
        {
            isKeyHeld = Input.GetKey(toggleKey);
            platformCollider.enabled = isKeyHeld;
        }

        if (spriteRenderer != null)
        {
            spriteRenderer.sprite = isKeyHeld ? brickON : brickOFF;
        }

        // Scale up when active, reset when inactive
        transform.localScale = isKeyHeld ? originalScale * 1.1f : originalScale;
        
    }
}
