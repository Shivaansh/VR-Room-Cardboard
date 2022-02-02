using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapOffButton : GazeableButton
{
    public override void OnPress(RaycastHit objectHit){
        base.OnPress(objectHit);

        if (parentPanel.currentActiveButton != null){
            Player.instance.snapModeOn = false;
        } 
    }
}
