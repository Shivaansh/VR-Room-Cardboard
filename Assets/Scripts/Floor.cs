using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : GazeableObject
{
    // Start is called before the first frame update
    public override void OnPress(RaycastHit objectHit){
        base.OnPress(objectHit);
        if(Player.instance.activeMode == InputMode.TELEPORT){
            Vector3 playerDest = objectHit.point;
            playerDest.y = Player.instance.transform.position.y;
            Player.instance.transform.position = playerDest;
        }else if (Player.instance.activeMode == InputMode.ADDOBJECT && Player.instance.activeFurniturePrefab != null){
            GameObject newObject = GameObject.Instantiate(Player.instance.activeFurniturePrefab) as GameObject;
            newObject.transform.position = objectHit.point;
        }
    }
}
