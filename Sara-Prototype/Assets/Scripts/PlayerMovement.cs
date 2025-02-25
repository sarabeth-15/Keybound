using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    [SerializeField] Rigidbody2D body;
    [SerializeField] float speed;

    void Start() {
        
    }

    private void Update() {
        float xInput = Input.GetAxis("Horizontal");
        float yInput = Input.GetAxis("Vertical");

        if (Mathf.Abs(xInput) > 0) {
            body.linearVelocity = new Vector2(xInput * speed, body.linearVelocity.y); 
        }

        if (Mathf.Abs(yInput) > 0) {
            body.linearVelocity = new Vector2(body.linearVelocity.x, yInput * speed);
        } 
    }







} 