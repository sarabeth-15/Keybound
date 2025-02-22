using UnityEngine;

public class PlayerMovement : MonoBehaviour 
{
    //get reference
    private Rigidbody2D body;
    private Animator anim;
    private bool grounded;
    private float jumpForce = 10; 
    private bool jumpPressed = false;
    
    [SerializeField] private float speed;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        //prevent rotation of the player
        body.freezeRotation = true;
    }
    private void Update() 
    {
        float horizontalInput = 0f;

        if (Input.GetKey(KeyCode.LeftArrow))
            horizontalInput = -1f;
        else if (Input.GetKey(KeyCode.RightArrow))
            horizontalInput = 1f;

        body.linearVelocity = new Vector2(horizontalInput*speed, body.linearVelocity.y);
        
        //flip player left or right
        if(horizontalInput>0.01f && Input.GetKey(KeyCode.RightArrow))
            transform.localScale = new Vector3(1,1,1);
        else if(horizontalInput<-0.01f&& Input.GetKey(KeyCode.LeftArrow))
            transform.localScale = new Vector3(1,1,1);
        
        if(Input.GetKeyDown(KeyCode.UpArrow) && grounded && !jumpPressed)
            Jump();

        //animation parameters
        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("grounded", grounded);
    }
    private void Jump()
    {
        body.linearVelocity = new Vector2(body.linearVelocity.x, 3);
        body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        anim.SetTrigger("jump");
        grounded = false;
        jumpPressed = true;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag =="Ground")
        {
            grounded = true;
            //Prevent vertical bouncing after landing
            if (body.linearVelocity.y < 0) 
                body.linearVelocity = new Vector2(body.linearVelocity.x, 0);
            jumpPressed = false;
        }
    }

}