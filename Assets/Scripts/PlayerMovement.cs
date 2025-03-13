using UnityEngine;
public class PlayerMovement : MonoBehaviour {
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpPower = 10f;
    [SerializeField] private LayerMask groundLayer;
    private Rigidbody2D body;
    private CapsuleCollider2D boxCollider;
    private float horizontalInput;
    private Animator anim;

    private void Awake() {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<CapsuleCollider2D>();
    }

    private void Update() {
        horizontalInput = 0f;

        // Left/Right movement and sprite flipping
        if (Input.GetKey(KeyCode.LeftArrow)) {
            horizontalInput = -1f;
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (Input.GetKey(KeyCode.RightArrow)) {
            horizontalInput = 1f;
            GetComponent<SpriteRenderer>().flipX = false;            
        }

        // Set animator parameters
        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("grounded", isGrounded());

        body.linearVelocity = new Vector2(horizontalInput * speed, body.linearVelocity.y);

        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            Jump();
        }
    }

    private void Jump() {
        if (isGrounded()) {
            body.linearVelocity = new Vector2(body.linearVelocity.x, jumpPower);
            anim.SetTrigger("jump");
        }
    }

    public bool isGrounded() {
        // Reduce the box size to only check the bottom of the player
        float extraHeight = 0.1f;
        Vector2 boxSize = new Vector2(boxCollider.bounds.size.x * 0.8f, 0.1f);
        Vector2 boxCenter = new Vector2(boxCollider.bounds.center.x, boxCollider.bounds.min.y);

        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCenter, boxSize, 0, Vector2.down, extraHeight, groundLayer);
        return raycastHit.collider != null;
    }
}
