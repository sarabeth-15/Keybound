using UnityEngine;
using System.Collections;

public class FallingPlatforms : MonoBehaviour
{
    private Collider2D platformCollider;
    private SpriteRenderer blockRenderer;
    private Vector2 originalScale;
    private float fallDelay = 3f; //delay before the block falls
    private float destroyDelay = 2f; //delay befoer block is destroyed

    [SerializeField] private KeyCode key; //which key on keyboard
    [SerializeField] private Sprite brick1;
    [SerializeField] private Sprite brick0;
    [SerializeField] private Sprite brickCrumble;
    [SerializeField] private Rigidbody2D rb; //which cooresponding block

    private void Awake()
    {
        platformCollider = GetComponent<Collider2D>();
        blockRenderer = GetComponent<SpriteRenderer>();

        originalScale = transform.localScale;
        platformCollider.enabled = false;

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
            StartCoroutine(Fall());
        }
    }
    private IEnumerator Fall()
    {
        //fall and detroy block after delay 
        yield return new WaitForSeconds(fallDelay);
        blockRenderer.sprite = brickCrumble;
        platformCollider.enabled = false;
        rb.bodyType = RigidbodyType2D.Dynamic;
        Destroy(gameObject, destroyDelay);
    }
}