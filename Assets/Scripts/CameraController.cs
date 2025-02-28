using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float speed;
    private float currentPosY;
    private Vector3 velocity = Vector3.zero;
    public Transform currentRoom; //!

    private void Update ()
    {
        transform.position = Vector3.SmoothDamp(transform.position, new Vector3(transform.position.x, currentPosY, transform.position.z), ref velocity, speed);
    }

    public void MoveToNewRoom(Transform _newRoom)
    {
        currentPosY = _newRoom.position.y;
        currentRoom = _newRoom; //!
    }
}