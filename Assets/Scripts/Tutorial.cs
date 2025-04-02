using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public GameObject[] popUps;
    private int popUpIndex = 0;

    void Update()
    {
        // Update which tutorial pop-up is active
        for (int i = 0; i < popUps.Length; i++)
        {
            if(i== popUpIndex){
                popUps[i].SetActive(true);
            }
            else{
                popUps[i].SetActive(false);
            
        }
        }
        if (popUpIndex ==0)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow)||Input.GetKeyDown(KeyCode.LeftArrow))
            {
                popUpIndex++; // Move to next tutorial step
            }
        }
        else if (popUpIndex == 1)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                popUpIndex++; // Move to next tutorial step
            }
        }

        // First tutorial step: Check for specific key presses (z, x, c, v, b, n, m)
        else if (popUpIndex == 2)
        {
            if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.X) ||
                Input.GetKeyDown(KeyCode.C) || Input.GetKeyDown(KeyCode.V) ||
                Input.GetKeyDown(KeyCode.B) || Input.GetKeyDown(KeyCode.N) ||
                Input.GetKeyDown(KeyCode.M))
            {
                popUpIndex++; // Move to next tutorial step
            }
        }
        // Second tutorial step: Wait for KeypadEnter
        else if (popUpIndex == 3)
        {
            if (Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                popUpIndex++;
            }
        }
        // Third tutorial step: Wait for KeypadEnter again
    }
}
