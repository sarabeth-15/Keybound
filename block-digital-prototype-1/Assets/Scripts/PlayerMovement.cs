using UnityEngine;

public class PlayerMovement : MonoBehaviour 
{
    [SerializeField] private float speed; 
    private Rigidbody2D body;

    private void Awake() {
        body = GetComponent<Rigidbody2D>(); 
    }

    private void Update() {
        body.linearVelocity = new Vector2(Input.GetAxis("Horizontal"), body.linearVelocity.y); 

        if (Input.GetKey(KeyCode.Space))
            body.linearVelocity = new Vector2(body.linearVelocity.x, speed); 
    }
}
