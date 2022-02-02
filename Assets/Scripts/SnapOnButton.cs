using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapOnButton : GazeableButton
{
    public override void OnPress(RaycastHit objectHit){
        base.OnPress(objectHit);

        if (parentPanel.currentActiveButton != null){
            Player.instance.snapModeOn = true;
        }        
    }
}
