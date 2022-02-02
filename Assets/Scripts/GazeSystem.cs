using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GazeSystem : MonoBehaviour
{

    [SerializeField] GameObject reticle;

    [SerializeField] Color activeColor;
    [SerializeField] Color inactiveColor;

    private GazeableObject currentGazeObject;
    private GazeableObject currentSelectedObject;

    private RaycastHit lastHit;

    // Start is called before the first frame update
    void Start()
    {
        SetReticleColor(inactiveColor);
    }

    // Update is called once per frame
    void Update()
    {
        ProcessGaze();
        CheckForInput(lastHit);
    }

    void ProcessGaze()
    {

        Ray raycastRay  = new Ray(transform.position, transform.forward);
        RaycastHit objectHit;

        Debug.DrawRay(raycastRay.origin, raycastRay.direction * 100, Color.yellow);
        if(Physics.Raycast(raycastRay, out objectHit)){
            // Do something to the object

            // Check if the object is interactable / is a gazeable object
            GameObject hitObject = objectHit.collider.gameObject;
            GazeableObject gazeObject = hitObject.GetComponent<GazeableObject>();

            // Check if the object is a new object (first time looking)
            

            // Set reticle color based on object
            if(gazeObject != null){
                if(gazeObject != currentGazeObject){
                    Debug.Log("New Gazeable object hit!");
                    ClearCurrentGazeObject();
                    currentGazeObject = gazeObject;
                    currentGazeObject.OnGazeEnter(objectHit);
                    SetReticleColor(activeColor);
                }else{
                    Debug.Log("Some Gazeable object hit!");
                    currentGazeObject.OnGaze(objectHit);
                }
            }else{
                ClearCurrentGazeObject();
            }
            lastHit = objectHit;

        }else{
            ClearCurrentGazeObject();
        }
    }

    public void SetReticleColor(Color reticleColor){
        Debug.Log("Changing reticle color!");
        reticle.GetComponent<Renderer>().material.SetColor("_Color", reticleColor);
    }

    private void ClearCurrentGazeObject(){
        if(currentGazeObject != null){
            currentGazeObject.OnGazeExit();
            SetReticleColor(inactiveColor);
            currentGazeObject = null;
        }
    }

    private void CheckForInput(RaycastHit objectHit){
        //Check down
        if(Input.GetMouseButtonDown(0) && currentGazeObject != null){
            currentSelectedObject = currentGazeObject;
            currentSelectedObject.OnPress(objectHit);
        }else if(Input.GetMouseButton (0) && currentSelectedObject != null) {
            currentSelectedObject.OnHold (objectHit);
        }
        else if (Input.GetMouseButtonUp (0) && currentSelectedObject != null) {
            currentSelectedObject.OnRelease (objectHit);
            currentSelectedObject = null;
        } 
    }


}
