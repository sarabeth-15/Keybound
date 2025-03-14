using UnityEngine;

public class LetterOverlay : MonoBehaviour
{
    private SpriteRenderer letterRenderer;
    private TogglePlatform togglePlatform;
    private RoomCheck room;
    [SerializeField] private Sprite letterSprite;
    [SerializeField] private Color32 colorOFF; 
    [SerializeField] private Color32 colorON; 

    private void Start()
    {
        togglePlatform = GetComponentInParent<TogglePlatform>();
        if (togglePlatform == null)
        {
            Debug.LogError("LetterOverlay must be attached to a child of an object with TogglePlatform!");
            return;
        }

        room = togglePlatform.room;

        // Create a new object for the letter overlay
        GameObject letterObject = new GameObject("LetterOverlay");
        letterObject.transform.SetParent(transform);
        letterObject.transform.localPosition = Vector3.zero; // Align with the platform

        // Add and configure the sprite renderer
        letterRenderer = letterObject.AddComponent<SpriteRenderer>();
        letterRenderer.sprite = letterSprite;
        letterRenderer.sortingOrder = GetComponentInParent<SpriteRenderer>().sortingOrder + 1; // Ensure it's on top

        colorOFF = new Color32 (32, 25, 29, 255);
        colorON = new Color32 (48, 37, 44, 255); 

        // Set initial color to Yellow
        letterRenderer.color = colorOFF;
    }

    private void Update()
    {
        
        if (togglePlatform == null || room == null || letterRenderer == null) return;
        if (PauseMenu.IsKeyBlocked(togglePlatform.toggleKey)) return;

        bool isKeyHeld = false;

        if (room.playerInRoom) // Only run if player is in the room
        {
            isKeyHeld = Input.GetKey(togglePlatform.toggleKey);
            Color32 currentColor = isKeyHeld ? colorON : colorOFF;
            letterRenderer.color = currentColor;
        }
        else {
            letterRenderer.color = colorOFF;
        }
    }
}
