using UnityEngine;

public class TogglePlatform : MonoBehaviour
{
    private Collider2D platformCollider;
    private SpriteRenderer spriteRenderer;
    private Vector3 originalScale;
    private bool used = false;
    [SerializeField] public KeyCode toggleKey;
    [SerializeField] private Sprite brickON;
    [SerializeField] private Sprite brickOFF;
    [SerializeField] private Sprite brickBROKEN;
    [SerializeField] public RoomCheck room;
    [SerializeField] public bool broken;
    
    

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
        if (toggleKey == KeyCode.None || used) return;

        if (!room.playerInRoom) 
        {
            platformCollider.enabled = false;
            transform.localScale = originalScale;
            spriteRenderer.sprite = brickOFF;
            return;
        }

        bool isKeyHeld = Input.GetKey(toggleKey);

        if (!isKeyHeld && platformCollider.enabled && broken)
        {
            used = true;
            platformCollider.enabled = false;
            transform.localScale = originalScale;
            spriteRenderer.sprite = brickBROKEN;
            return;
        } 
        
        platformCollider.enabled = isKeyHeld;

        if (spriteRenderer != null)
        {
            spriteRenderer.sprite = isKeyHeld ? brickON : brickOFF;
        }

        // Scale up when active, reset when inactive
        transform.localScale = isKeyHeld ? originalScale * 1.1f : originalScale;
        
    }
}
