using UnityEngine;

public class RoomCheck : MonoBehaviour
{
    public bool playerInRoom = false;
    private float timeEntered;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            timeEntered = Time.time; 
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !playerInRoom && Time.time - timeEntered >= 1f)
            playerInRoom = true;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            playerInRoom = false;
    }
}
