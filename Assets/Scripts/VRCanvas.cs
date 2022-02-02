using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRCanvas : MonoBehaviour
{
    public GazeableButton currentActiveButton;

    public Color activeColor = Color.yellow;
    public Color inactiveColor = Color.white;

    


    // Start is called before the first frame update
    void Start()
    {
        LookAtPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log("current Active Button is " + currentActiveButton.name);
    }

    public void SetActiveButton(GazeableButton activeButton){
        if(currentActiveButton != null){
            currentActiveButton.SetButtonColor(inactiveColor);
        }
        if(activeButton != null && currentActiveButton != activeButton){
            currentActiveButton = activeButton;
            currentActiveButton.SetButtonColor(activeColor);
        }else{
            Debug.Log("Resetting currentActiveButton");
            currentActiveButton = null;
            Player.instance.activeMode = InputMode.NONE;
        }
    }

    public void LookAtPlayer() {
        Vector3 playerPosition = Player.instance.transform.position;
        Vector3 vectorToPlayer = playerPosition - transform.position;
        Vector3 targetPoint = transform.position - vectorToPlayer;
        transform.LookAt(targetPoint);
    }
}
