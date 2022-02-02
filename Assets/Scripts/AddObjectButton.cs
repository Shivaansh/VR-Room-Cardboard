using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddObjectButton : GazeableButton
{
    // Start is called before the first frame update
    public Object prefab;

    public override void OnPress(RaycastHit objectHit){
        base.OnPress(objectHit);

        if (parentPanel.currentActiveButton != null){
            Player.instance.activeMode = InputMode.ADDOBJECT;
        }
        Player.instance.activeFurniturePrefab = prefab;
    }
}
