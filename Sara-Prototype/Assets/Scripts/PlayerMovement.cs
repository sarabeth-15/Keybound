using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public Rigidbody2D body;
    public BoxCollider2D groundCheck;
    public LayerMask groundMask;

    public float acceleration; 
    [Range(0f, 1f)]
    public float groundDecay;
    public float groundSpeed;
    public float jumpSpeed;

    public bool grounded;
    public float horizontalInput; 
    float xInput;
    float yInput; 


    void Update() {
        CheckInput();
        HandleJump(); 
    }

    void FixedUpdate() {
        CheckGround();
        HandleMovement(); 
        ApplyFriction();
    }

    void CheckInput() {
        xInput = Input.GetAxis("Horizontal");
        yInput = Input.GetAxis("Vertical"); 
    }

    void HandleMovement() {
        horizontalInput = 0f;

        if (Input.GetKey(KeyCode.LeftArrow)) {
            horizontalInput = -1f;
        }

        if (Input.GetKey(KeyCode.RightArrow)) {
            horizontalInput = 1f;
        }

        body.linearVelocity = new Vector2(horizontalInput * groundSpeed, body.linearVelocity.y); 
    } 

    void HandleJump() {
        
        if (Input.GetKeyDown(KeyCode.UpArrow) && grounded) {
            body.linearVelocity = new Vector2(body.linearVelocity.x, jumpSpeed);
        }
    }

    void CheckGround() {
        grounded = Physics2D.OverlapAreaAll(groundCheck.bounds.min, groundCheck.bounds.max, groundMask).Length > 0; 
    }


    void ApplyFriction() {
        if (grounded && xInput == 0 && body.angularVelocity <= 0) {
            body.angularVelocity *= groundDecay;
        }
    } 

} 