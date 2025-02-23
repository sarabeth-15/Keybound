using UnityEngine; 
using UnityEngine.UI; 

public class KeyBackgroundManager : MonoBehaviour {
  [SerializeField] private Sprite m_keyPressBackgroundSprite;
  [SerializeField] private Image[] m_keyBackgrounds; // Array of backgrounds, 0-9, in that order
  private Collider2D objectCollider; 

  private void Update() {
    // Handle number keys (0-9)
    for (int keyIndex = 0; keyIndex <= 9; ++keyIndex) {
      KeyCode keyCode = (KeyCode)((int)KeyCode.Alpha0 + keyIndex); // Assuming the order stays the same, otherwise use a Dictionary<int, KeyCode>
      if (Input.GetKey(keyCode))
      {
        m_keyBackgrounds[keyIndex].sprite = m_keyPressBackgroundSprite;
        AddCollider(m_keyBackgrounds[keyIndex]); 
      }
    }

    // Handle letter keys (A-Z)
    for (int keyIndex = 0; keyIndex < 26; ++keyIndex) {
      KeyCode keyCode = (KeyCode)((int)KeyCode.A + keyIndex); 
      if (Input.GetKey(keyCode))
      {
        m_keyBackgrounds[keyIndex + 10].sprite = m_keyPressBackgroundSprite;
        AddCollider(m_keyBackgrounds[keyIndex + 10]); 
      }
    }
  }

    private void AddCollider(Image keyImage) {
      BoxCollider2D collider = keyImage.GetComponent<BoxCollider2D>();
      
      if (collider == null) {
        collider = keyImage.gameObject.AddComponent<BoxCollider2D>();
      }

      // Adjust the collider's size and position to match the Image
      RectTransform rectTransform = keyImage.GetComponent<RectTransform>();
      collider.size = rectTransform.rect.size;
      collider.offset = Vector2.zero;
      collider.enabled = true;
  }
}