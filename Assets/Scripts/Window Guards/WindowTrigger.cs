using UnityEngine;

public class WindowTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Guard"))
        {
            GuardMovement guard = collision.GetComponent<GuardMovement>();
            if (guard != null)
            {
                guard.inWindow(true);
                Debug.Log("Guard in Window!");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Guard"))
        {
            GuardMovement guard = collision.GetComponent<GuardMovement>();
            if (guard != null)
            {
               guard.inWindow(false);
            }
        }
    }
}