using UnityEngine;

public class PlayerMovement : MonoBehaviour 
{
    [SerializeField] private float speed = 5f; 
    [SerializeField] private float jumpForce = 10f; 
    private Rigidbody2D body;

    private void Awake() {
        body = GetComponent<Rigidbody2D>(); 
    }

    private void Update() {

        body.linearVelocity = new Vector2(Input.GetAxis("Horizontal") * speed, body.linearVelocity.y); 

        if (Input.GetKey(KeyCode.UpArrow))
            body.linearVelocity = new Vector2(body.linearVelocity.x, jumpForce); 
    } 
}
