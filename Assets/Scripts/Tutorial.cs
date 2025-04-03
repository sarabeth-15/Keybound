using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public GameObject[] popUps;
    private int popUpIndex = 0;
    [SerializeField]private PlayerMovement player;
    private PlayerInventory inv;

    private float delay = 1f; // Delay before moving to the next tutorial step
    private float nextStepTime = 0f; // Tracks when we can advance


    void Start()
    {
        player.SetJumpPower(0f);
        inv = PlayerInventory.instance;
    }
    void Update()
    {
        if (Time.time < nextStepTime) return;

        // Update which tutorial pop-up is active
        for (int i = 0; i < popUps.Length; i++)
        {
            if(i == popUpIndex){
                popUps[i].SetActive(true);
            }
            else{
                popUps[i].SetActive(false);
            
        }
        }
        if (popUpIndex == 0)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow)||Input.GetKeyDown(KeyCode.LeftArrow))
            {
                AdvanceTutorial(); // Move to next tutorial step
            }
        }
        else if (popUpIndex == 1)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                player.SetJumpPower(15f);
                AdvanceTutorial(); // Move to next tutorial step

            }
        }

        // First tutorial step: Check for specific key presses (z, x, c, v, b, n, m)
        else if (popUpIndex == 2)
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                popUpIndex++; // Move to next tutorial step
            }   
        }
        // Second tutorial step: Wait for any key to be pressed
        else if (popUpIndex == 3)
        {
            if (inv != null && inv.hasKey)
            {
                AdvanceTutorial();
            }
        }
        else if (popUpIndex == 4)
        {
            foreach (char c in Input.inputString)
            {
                if (char.IsLetterOrDigit(c)) // Check if it's A-Z or 0-9
                {
                    AdvanceTutorial(); // Move to next tutorial step
                    break;
                }
            }
        }
        else{
            AdvanceTutorial();
        }
       
    }
    void AdvanceTutorial()
    {
        popUpIndex++;
        nextStepTime = Time.time + delay; // Set delay before next step
    }
}
