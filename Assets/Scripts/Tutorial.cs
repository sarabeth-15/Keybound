using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public GameObject[] popUps;
    private int popUpIndex;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for(int i=0; i<popUps.Length;i++){
            if(i==popUpIndex){
                popUps[popUpIndex].SetActive(true);
            }else{
                popUps[popUpIndex].SetActive(false);
            }
        }
        if(popUpIndex == 0){
            foreach (KeyCode key in System.Enum.GetValues(typeof(KeyCode))){
                if (Input.GetKeyDown(key) && ((key >= KeyCode.A && key <= KeyCode.Z) || (key >= KeyCode.Alpha0 && key <= KeyCode.Alpha9)))
                {
                    popUpIndex++; //first press of the bricks allows the player to move to next tutorial
                }
            }
            if(popUpIndex == 1){
                if(Input.GetKeyDown(KeyCode.KeypadEnter)){
                    popUpIndex++;
                }
            }
            else if(popUpIndex == 1){
                if(Input.GetKeyDown(KeyCode.KeypadEnter)){
                    popUpIndex++;
                }
            }
        }
    }
}
