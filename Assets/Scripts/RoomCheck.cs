using UnityEngine;

public class RoomCheck : MonoBehaviour
{
    public bool playerInRoom = false;
    private float timeEntered = 0f;
    private bool isPlayerInside = false;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInside = true;
            timeEntered = Time.time; // Store the time when the player enters
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInside = false;
            playerInRoom = false; // Reset if the player leaves
        }
    }

    void Update()
    {
        if (isPlayerInside && !playerInRoom)
        {
            if (Time.time - timeEntered >= 1f) // Check if 2 seconds have passed
            {
                playerInRoom = true;
            }
        }
    }
}
