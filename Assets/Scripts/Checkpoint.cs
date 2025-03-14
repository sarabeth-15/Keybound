using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private Transform previousRoom;
    [SerializeField] private Transform nextRoom; 
    [SerializeField] private CameraController cam;
    [SerializeField] private PlayerMovement player; // Reference to the PlayerMovement script

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Move the camera up only if the player is NOT jumping
            if (collision.transform.position.y > transform.position.y)
            {
                cam.MoveToNewRoom(nextRoom); // Move camera up
            }
            else if (collision.transform.position.y < transform.position.y)
            {
                cam.MoveToNewRoom(previousRoom); // Move camera down
            }
        }
    }
}