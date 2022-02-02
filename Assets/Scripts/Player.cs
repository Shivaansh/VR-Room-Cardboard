using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InputMode {NONE, WALKING, TELEPORT, RUNNING, ADDOBJECT, TRANSLATE, ROTATE, SCALE, DELETE, DRAG}

public class Player : MonoBehaviour
{
    [SerializeField] private float playerWalkSpeed = 1.0f;
    [SerializeField] private float playerRunSpeed = 7.5f;
    public static Player instance;
    public InputMode activeMode = InputMode.NONE;

    public Object activeFurniturePrefab;

    public bool snapModeOn = true;

    public GameObject leftWall;
    public GameObject rightWall;
    public GameObject frontWall;
    public GameObject backWall;
    public GameObject ceiling;
    public GameObject floor;

    private Vector3 playerHeight;


    void Awake(){
        if(instance != null){GameObject.Destroy(instance.gameObject);}   
        instance = this;     
    }
    
    // Start is called before the first frame update
    void Start()
    {
        float playerHeight = Player.instance.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        TryWalk();
        TryRun();
    }

    public void TryWalk(){
        if (Input.GetMouseButton(0) && activeMode == InputMode.WALKING){
            Vector3 forward = Camera.main.transform.forward;

            Vector3 newPosition = transform.position + forward * Time.deltaTime * playerWalkSpeed;
            transform.position = newPosition;
        }
    }

    public void TryRun(){
        if (Input.GetMouseButton(0) && activeMode == InputMode.RUNNING){
            Vector3 forward = Camera.main.transform.forward;

            Vector3 newPosition = transform.position + forward * Time.deltaTime * playerRunSpeed;
            transform.position = newPosition;
        }
    }
}
