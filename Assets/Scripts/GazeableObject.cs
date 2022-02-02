using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GazeableObject : MonoBehaviour
{
    public bool isTransformable = false;

    private Vector3 initialObjectRotation;
    private Vector3 initialPlayerRotation;
    private Vector3 initialObjectScale;

    [SerializeField] private float rotationSpeed = 5.0f;
    [SerializeField] private float scaleSpeed = 0.1f;
    [SerializeField] private float rotationIncrement = 15.0f;

    private int objectLayer;
    private const int IGNORE_RAYCAST_LAYER = 2;
    // Start is called before the first frame update
    public void OnGazeEnter(RaycastHit objectHit)
    {
        Debug.Log("On Gaze Enter");
    }

    public void OnGaze(RaycastHit objectHit)
    {
        Debug.Log("On Gaze");
    }

    public void OnGazeExit()
    {
        Debug.Log("On Gaze Exit");
    }

    //INPUT METHODS

    public virtual void OnPress(RaycastHit objectHit)
    {
        Debug.Log("Gaze Press");
        Debug.Log("Object Selected: " + this.name);
        if(Player.instance.activeMode == InputMode.DELETE && isTransformable){
            // GameObject target = this as GameObject;
            Debug.Log("Deleting: " + this.name);
            GameObject.Destroy(gameObject);
        }
        if (isTransformable)
        {
            objectLayer = gameObject.layer;
            gameObject.layer = IGNORE_RAYCAST_LAYER;

            initialObjectRotation = transform.rotation.eulerAngles;
            initialPlayerRotation = Camera.main.transform.rotation.eulerAngles;
            initialObjectScale = transform.localScale;
        }
    }

    public virtual void OnHold(RaycastHit objectHit)
    {
        Debug.Log("Gaze Hold");
        if (isTransformable)
        {
            GazeTransform(objectHit);
        }
    }

    public virtual void OnRelease(RaycastHit objectHit)
    {
        Debug.Log("Gaze Release");
        if (isTransformable)
        {
            gameObject.layer = objectLayer;
        }
    }

    public virtual void GazeTransform(RaycastHit objectHit)
    {
        // Call the correct transformation function
        switch (Player.instance.activeMode)
        {
            // Call different cases for active mode
            case InputMode.TRANSLATE:
                GazeTranslate(objectHit);
                break;

            case InputMode.ROTATE:
                GazeRotate(objectHit);
                break;

            case InputMode.SCALE:
                GazeScale(objectHit);
                break;
        }
    }

    public virtual void GazeTranslate(RaycastHit objectHit)
    {
        // Move the object (position)
        if (objectHit.collider != null && objectHit.collider.GetComponent<Floor>())
        {
            transform.position = objectHit.point;
        }
    }

    public virtual void GazeRotate(RaycastHit objectHit)
    {
        // Change the object's orientation (rotation)
        Vector3 currentPlayerRotation = Camera.main.transform.rotation.eulerAngles;
        Vector3 currentObjectRotatation = transform.rotation.eulerAngles;

        Vector3 rotationDelta = currentPlayerRotation - initialPlayerRotation;

        Vector3 newRotation;
        if(Player.instance.snapModeOn == false){
            Debug.Log("Condition A");
            Debug.Log("Rotation Value: " + rotationDelta.y * rotationSpeed);
            newRotation = new Vector3(currentObjectRotatation.x, (initialObjectRotation.y + (rotationDelta.y * rotationSpeed)), currentObjectRotatation.z);
        }else{
            Debug.Log("Condition B");
            newRotation = new Vector3(currentObjectRotatation.x, (initialObjectRotation.y + (rotationIncrement * rotationSpeed)), currentObjectRotatation.z);
        }
        

        transform.rotation = Quaternion.Euler(newRotation);
    }

    public virtual void GazeScale(RaycastHit objectHit)
    {
        // Make the object bigger/smaller (scale)

        float scaleFactor = 1;

        Vector3 currentPlayerRotation = Camera.main.transform.rotation.eulerAngles;
        Vector3 rotationDelta = currentPlayerRotation - initialPlayerRotation;

        // If looking up
        if (rotationDelta.x < 0 && rotationDelta.x > -180.0f || rotationDelta.x > 180.0f && rotationDelta.x < 360.0f)
        {
            // If greater than 180, map it between 0 - 180
            if (rotationDelta.x > 180.0f)
            {
                rotationDelta.x = 360.0f - rotationDelta.x;
            }
            scaleFactor = 1.0f + Mathf.Abs(rotationDelta.x) * scaleSpeed;
        }
        else
        {
            if (rotationDelta.x < -180.0f)
            {
                rotationDelta.x = 360.0f + rotationDelta.x;
            }

            //adjusting since scale value needs to be between 0 and 1 at all times, and not too small
            scaleFactor = Mathf.Max(0.1f, 1.0f - (Mathf.Abs(rotationDelta.x) * (1 / scaleSpeed)) / 180.0f);
        }

        transform.localScale = scaleFactor * initialObjectScale;

    }

}
