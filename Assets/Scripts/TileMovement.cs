using UnityEngine;

    public class TileMovement : MonoBehaviour {
        private Rigidbody2D rb;
        private float destroyDelay = 2f; // delay before block is destroyed 
        // need current coordinates variable? (to respawn it back to original position)
        private SpriteRenderer spriteRenderer;
        private SpriteRenderer letterRenderer;
        [SerializeField] private Sprite letterSprite;
        [SerializeField] public KeyCode currentKey; // blank brick
        [SerializeField] private Sprite brickON;
        [SerializeField] private Sprite brickOFF;

        private void Awake() {
            spriteRenderer = GetComponent<SpriteRenderer>();
            rb = GetComponent<Rigidbody2D>();
            rb.gravityScale = 0; // Initially disable gravity

            // Create a new object for the letter overlay
            GameObject letterObject = new GameObject("LetterOverlay");
            letterObject.transform.SetParent(transform);
            letterObject.transform.localPosition = Vector3.zero; // Align with the platform

            // Add and configure the sprite renderer
            letterRenderer = letterObject.AddComponent<SpriteRenderer>();
            letterRenderer.sprite = letterSprite;
            letterRenderer.sortingOrder = GetComponentInParent<SpriteRenderer>().sortingOrder + 1; // Ensure it's on top
        }

        private void Update() {

            if(Input.GetKeyDown(currentKey)){
                rb.gravityScale = 6; // Enable gravity on click
                // change sprite to brick on
                if (spriteRenderer != null)
                {
                    spriteRenderer.sprite = brickON;
                }
                Destroy(gameObject, destroyDelay);
            }
            
        }
    }