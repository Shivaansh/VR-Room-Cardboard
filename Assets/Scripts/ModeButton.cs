using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeButton : GazeableButton
{

    [SerializeField] private InputMode mode;
    public override void OnPress(RaycastHit objectHit){
        base.OnPress(objectHit);
        if (parentPanel.currentActiveButton != null){
            Player.instance.activeMode = mode;
        }
    }
}
