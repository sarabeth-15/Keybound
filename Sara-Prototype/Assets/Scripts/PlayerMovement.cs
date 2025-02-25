using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    [SerializeField] Rigidbody2D body;

    public float groundSpeed;
    public float jumpSpeed; 


    [Range(0f, 1f)]
    public float groundDecay; 
    public BoxCollider2D groundCheck;
    public LayerMask groundMask; 
    public bool grounded;
    float xInput;
    float yInput; 

    void Start() {
        
    }

    void Update() {

        GetInput();
        MoveWithInput();
    } 

    void MoveWithInput() {
        if (Mathf.Abs(xInput) > 0) {
            body.linearVelocity = new Vector2(xInput * groundSpeed, body.linearVelocity.y);
        } 
        if (Mathf.Abs(yInput) > 0 && grounded) {
            body.linearVelocity = new Vector2(body.linearVelocity.x, yInput * jumpSpeed);
        }
    }


    void GetInput() {
        xInput = Input.GetAxis("Horizontal");
        yInput = Input.GetAxis("Vertical");
    }

    void FixedUpdate() {
        CheckGround();
        ApplyFriction(); 
    }

    void CheckGround() {
        grounded = Physics2D.OverlapAreaAll(groundCheck.bounds.min, groundCheck.bounds.max, groundMask).Length > 0; 
    }

    void ApplyFriction() {
        if (grounded && xInput == 0 && yInput == 0) {
            body.angularVelocity *= groundDecay;
        }
    }





} 