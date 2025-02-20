using UnityEngine; 
using UnityEngine.UI; 

public class KeyBackgroundManager : MonoBehaviour
{
  [SerializeField] private Sprite m_keyPressBackgroundSprite;
  [SerializeField] private Image[] m_keyBackgrounds; // Array of backgrounds, 0-9, in that order

  private void Update()
  {
    for (int keyIndex = 0; keyIndex <= 9; ++keyIndex)
    {
      KeyCode keyCode = (KeyCode)((int)KeyCode.Alpha0 + keyIndex); // Assuming the order stays the same, otherwise use a Dictionary<int, KeyCode>
      if (Input.GetKey(keyCode))
      {
        m_keyBackgrounds[keyIndex].sprite = m_keyPressBackgroundSprite;
      }
    }

    // Handle letter keys (A-Z)
    for (int keyIndex = 0; keyIndex < 26; ++keyIndex)
    {
      KeyCode keyCode = (KeyCode)((int)KeyCode.A + keyIndex); 
      if (Input.GetKey(keyCode))
      {
        m_keyBackgrounds[keyIndex + 10].sprite = m_keyPressBackgroundSprite;
      }
    }
  }
}