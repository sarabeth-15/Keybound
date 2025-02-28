using UnityEngine;

public class SafePlatform : MonoBehaviour
{
    [SerializeField] private Transform nextRoom; 
    [SerializeField] private CameraController cam;

    private void OnTriggerEnter2D (Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            cam.MoveToNewRoom(nextRoom);   
        }
    }
}