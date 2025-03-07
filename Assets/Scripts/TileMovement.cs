using UnityEngine;

    public class TileMovement : MonoBehaviour {
        private Rigidbody2D rb;
        private float destroyDelay = 2f; // delay before block is destroyed 
        // need current coordinates variable? (to respawn it back to original position)
        private SpriteRenderer spriteRenderer;
        [SerializeField] public KeyCode currentKey;
        [SerializeField] private Sprite brickON;

        private void Awake() {
            spriteRenderer = GetComponent<SpriteRenderer>();
            rb = GetComponent<Rigidbody2D>();
            rb.gravityScale = 0; // Initially disable gravity
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