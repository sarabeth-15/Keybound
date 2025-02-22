using UnityEngine;
using System.Collections;

public class TileMovement : MonoBehaviour
{
    private Collider2D platformCollider;
    private SpriteRenderer blockRenderer;
    private SpriteRenderer letterRenderer;
    private Vector2 originalScale;
    private float fallDelay = 3f; //delay before the block falls
    private float destroyDelay = 2f; //delay befoer block is destroyed

    [SerializeField] private KeyCode key; //which key on keyboard
    [SerializeField] private Sprite brick1;
    [SerializeField] private Sprite brick0;
    [SerializeField] private Sprite letter; //which cooresponding letter
    [SerializeField] private Rigidbody2D rb; //which cooresponding block

    private void Awake()
    {
        platformCollider = GetComponent<Collider2D>();
        blockRenderer = GetComponent<SpriteRenderer>();

        originalScale = transform.localScale;
        platformCollider.enabled = false;

        //set intitial conditions for the letters
        letterRenderer = new GameObject().AddComponent<SpriteRenderer>();
        letterRenderer.transform.SetParent(transform, false); 
        letterRenderer.transform.localScale = originalScale * 1.5f;
        letterRenderer.sprite = letter;
        letterRenderer.sortingOrder = blockRenderer.sortingOrder + 1; //make sure letter is above block
        letterRenderer.color = new Color(1, 1, 1, 0.2f);
    }
    private void Update()
    {
        if (key == KeyCode.None) return; //if nothing is pressed

        //Change block and letter appearance, activate falling block
        if(Input.GetKey(key))
        {   
            platformCollider.enabled = true;
            blockRenderer.sprite = brick1;
            transform.localScale = originalScale * 1.1f;
            letterRenderer.color = new Color(1, 1, 1, 1);
            StartCoroutine(Fall());
        }
    }
    private IEnumerator Fall()
    {
        //fall and detroy block after delay 
        yield return new WaitForSeconds(fallDelay);
        platformCollider.enabled = false;
        rb.bodyType = RigidbodyType2D.Dynamic;
        Destroy(gameObject, destroyDelay);
    }
}
