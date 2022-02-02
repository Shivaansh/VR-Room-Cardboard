using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapModeButton : GazeableButton
{
    // Start is called before the first frame update
    public Object prefab;

    public override void OnPress(RaycastHit objectHit){
        base.OnPress(objectHit);

        Player.instance.snapModeOn = true;
    }
}