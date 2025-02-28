using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private Transform previousRoom;
    [SerializeField] private CameraController cam;
    private void OnTriggerEnter2D (Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if(collision.transform.position.y > transform.position.y) //player coming from below
            {
                cam.MoveToNewRoom(previousRoom);
            }
        }
    }
}