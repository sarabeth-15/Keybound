using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float wallJumpPowerX = 7f;
    [SerializeField] private float wallJumpPowerY = 12f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;

    private Rigidbody2D body;
    private BoxCollider2D boxCollider; 
    private float wallJumpCooldown;
    private float horizontalInput;
    private bool isWallSliding; 

    private void Awake() {
        body = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update() {
        horizontalInput = 0f;

        if (Input.GetKey(KeyCode.LeftArrow)) {
            horizontalInput = -1f;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            horizontalInput = 1f;
        } 

        if (wallJumpCooldown > 0.2f) {
            body.linearVelocity = new Vector2(horizontalInput * speed, body.linearVelocity.y);

            if (onWall() && !isGrounded()) {
                isWallSliding = true;
                body.gravityScale = 2; // Reduce gravity while sliding

                // If moving into the wall, prevent upward movement
                if (body.linearVelocity.y > 0)
                {
                    body.linearVelocity = new Vector2(0, body.linearVelocity.y * 0.5f);
                }
                else
                {
                    body.linearVelocity = new Vector2(0, -2f); // Ensure smooth sliding down
                }
            }
            else
            {
                isWallSliding = false;
                body.gravityScale = 8;
            }

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                Jump();
            }
        }
        else {
            wallJumpCooldown += Time.deltaTime;
        }
    }

    private void Jump()
    {
        if (isGrounded())
        {
            body.linearVelocity = new Vector2(body.linearVelocity.x, jumpForce);
        }
        else if (onWall() && isWallSliding)
        {
            int wallDirection = GetWallDirection();
            body.linearVelocity = new Vector2(wallDirection * wallJumpPowerX, wallJumpPowerY);
            wallJumpCooldown = 0;
        }
    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    private bool onWall()
    {
        return Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.left, 0.1f, wallLayer) ||
               Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.right, 0.1f, wallLayer);
    }

    private int GetWallDirection()
    {
        bool onLeftWall = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.left, 0.1f, wallLayer);
        return onLeftWall ? 1 : -1; 
    }
} 