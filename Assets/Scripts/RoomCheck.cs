using UnityEngine;

public class RoomCheck : MonoBehaviour
{
    public bool playerInRoom = false;
    void OnTriggerEnter2D (Collider2D collision) 
    //NOTE: maybe add like a 2 second cooldown so jumping and hitting your head into the next room doesn't count as being in the room
    {
        if (collision.tag == "Player")
        {
            playerInRoom = true;
        }
    }
    void OnTriggerExit2D (Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerInRoom = false;
        }
    }
}
