using UnityEngine;

public class PlayerMovement : MonoBehaviour 
{
    //get reference
    private Rigidbody2D body;
    private Animator anim;
    private bool grounded;
    
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
        float horizontalInput = Input.GetAxis("Horizontal");
        body.linearVelocity = new Vector2(horizontalInput*speed, body.linearVelocity.y);
        
        //flip player left or right
        if(horizontalInput>0.01f && Input.GetKey(KeyCode.RightArrow))
            transform.localScale = new Vector3(1,2,1);
        else if(horizontalInput<-0.01f&& Input.GetKey(KeyCode.LeftArrow))
            transform.localScale = new Vector3(1,2,1);
        
        if(Input.GetKey(KeyCode.UpArrow) && grounded)
            Jump();

        //animation parameters
        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("grounded", grounded);
    }
    private void Jump()
    {
        body.linearVelocity = new Vector2(body.linearVelocity.x, 6);
        anim.SetTrigger("jump");
        grounded = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag =="Ground")
            grounded = true;
    }

}