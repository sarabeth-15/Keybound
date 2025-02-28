using UnityEngine;

public class TogglePlatform : MonoBehaviour
{
    private Collider2D platformCollider;
    private SpriteRenderer spriteRenderer;
    private Vector3 originalScale;
    [SerializeField] public KeyCode toggleKey;
    [SerializeField] private Sprite brickON;
    [SerializeField] private Sprite brickOFF;
    
    //[SerializeField] public CameraController cam; //!


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

        //if (gameObject.root != cam.currentRoom) return; //!

        bool isKeyHeld = Input.GetKey(toggleKey);
        platformCollider.enabled = isKeyHeld;

        if (spriteRenderer != null)
        {
            spriteRenderer.sprite = isKeyHeld ? brickON : brickOFF;
        }

        // Scale up when active, reset when inactive
        transform.localScale = isKeyHeld ? originalScale * 1.1f : originalScale;
    }
}
