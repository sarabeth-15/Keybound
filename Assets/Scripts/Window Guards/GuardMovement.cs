using UnityEngine;

public class GuardMovement : MonoBehaviour
{
    [SerializeField] public float speed = 5f;
    bool switc = true;
    private SpriteRenderer spriteRenderer;

    public bool window = false;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Guard Position: " + transform.position.x + " | Moving: " + (switc ? "Right" : "Left"));
        if(switc){
            moveRight();
        }
        if(!switc){
            moveLeft();
        }
        if(transform.position.x>= 7f){
            switc = false;
            spriteRenderer.flipX = true;
        }
        if(transform.position.x <= -13f){
            switc = true;
            spriteRenderer.flipX = false;
        }
        
    }
    void moveRight(){
        transform.Translate(speed*Time.deltaTime, 0, 0);
    }
    void moveLeft(){
        transform.Translate(-speed*Time.deltaTime, 0, 0);
    }
    public void inWindow(bool state){
        window = state;
    }
    
}

