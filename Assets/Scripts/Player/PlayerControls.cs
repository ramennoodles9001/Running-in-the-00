using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
[RequireComponent(typeof(PlayerInfo))]
public class PlayerControls : NetworkBehaviour
{
    private Transform player, camera;
    //private CharacterController cc;
    private Vector3 moveDirection;
    private float speed = 1.0f, jumpSpeed = 8.0f, externalSpeed=1.0f;
    [Range(1, 50)]
    public float sensitivity_x = 1.0f;
    private float maxAngle = 60, minAngle = -20, tilt = 0;
    private float rotationY, rotationX, rotationZ;
    private CharacterController cc;
    private bool isGrounded;
    private float t = 0;
    private float recoil, compensation;
    private PlayerInfo pi;
    private float recoilRate = 20, compensationRate = 10;
    private float collisionAngle, slopeAngle = 40;
    private Vector3 velocityChange;
    private float tt = 0;
    private float fallSpeed;
    private float dodgeVelocity=1;
    private PlayerInfo playerInfo;
    private float defaultFOV=75;
    private Inventory inv;
    private Console console;
    private GameObject consoleObject;
    private Canvas inventoryCanvas;
    public bool allowInput = true;
    private GameObject weaponEquipped;
    private float testVel = 0.0f;
    private GameObject gamemanager;
    private GameObject dialogueManager;
    // Use this for initialization
    public float getRecoilRate()
    {
        return recoilRate;
    }
    public float getCompensationRate()
    {
        return compensationRate;
    }
    public float getExternalSpeed(){
        return externalSpeed;
    }
    public float getDefaultFOV(){
        return defaultFOV;
    }
    public void setExternalSpeed(float s){
        externalSpeed = s;
    }
    void OnCollisionStay(Collision collision)
    {
        Vector3 tempPos;
        tempPos = transform.position;
        tempPos.y = transform.position.y - 1;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 1.5f) || Physics.Raycast(transform.position, -transform.forward, out hit, 1.5f) || Physics.Raycast(transform.position, transform.right, out hit, 1.5f) || Physics.Raycast(transform.position, -transform.right, out hit, 1.5f))
        {
            Debug.DrawRay(tempPos, transform.forward, Color.black, 1.5f);
            collisionAngle = Mathf.Abs(-Vector3.Dot(tempPos, hit.normal) - 90);
            //collisionAngle = Vector3.Angle(hit.normal,transform.position)-90;        
        }
        else
        {
            collisionAngle = 0;
        }
    }

    void Start()
    {
        inventoryCanvas = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Canvas>();
        inv = GetComponent<Inventory>();
        consoleObject = GameObject.FindGameObjectWithTag("Console");
        console = consoleObject.GetComponent<Console>();
        cc = gameObject.GetComponent<CharacterController>();
        playerInfo = gameObject.GetComponent<PlayerInfo>();
        player = transform;
        camera = GameObject.FindGameObjectWithTag("MainCamera").transform;
        pi = gameObject.GetComponent<PlayerInfo>();
        gamemanager = GameObject.FindGameObjectWithTag("GameManager");
        dialogueManager = GameObject.FindGameObjectWithTag("DialogueManager");
        if (camera == null)
        {
            Debug.Log("Camera transform needed");
        }
        lockCursor();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Sets y velocity / Jumping
        

    }



    void Update()
    {   
        if(Input.GetKeyDown(KeyCode.E)){
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, 10f)){
                //Debug.Log(hit.transform.tag);
                if (hit.transform.tag == "NPC"){
                    //CHANGE THIS TO INITIATE CONVERSATION
                    hit.transform.gameObject.GetComponent<NPC>().Interact();
                }
            }
        }

        if(dodgeVelocity >=1){
            dodgeVelocity = Mathf.Lerp(dodgeVelocity,1,.5f);
        }
        collisionAngle = 0;
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && allowInput)
        {
            fallSpeed = Mathf.Lerp(moveDirection.y,2,1);
        }
        moveDirection = new Vector3(moveDirection.x, fallSpeed , moveDirection.z);

        if (collisionAngle < slopeAngle && allowInput)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal")*speed*externalSpeed, fallSpeed , Input.GetAxis("Vertical")*speed*externalSpeed);
            moveDirection = transform.TransformDirection(moveDirection);

        }
        else
        {
            moveDirection = new Vector3(0, fallSpeed , 0);
            moveDirection = transform.TransformDirection(moveDirection);
        }

        if(Input.GetKey(KeyCode.LeftShift)&& playerInfo.UseStamina(1)){
            speed = 1.3f;
            
        }else
        {
            speed = 1;
        }
        

        if(!isGrounded){
            fallSpeed = Mathf.Lerp(moveDirection.y,-1,.1f);
        }
        moveDirection.x *= dodgeVelocity;
        moveDirection.z *= dodgeVelocity;
        cc.Move(moveDirection*.15f);

        

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            pi.TakeDamage(10);
        }
        if(Input.GetKeyDown(KeyCode.LeftAlt)){
            unlockCursor();
        }
        if(Input.GetKeyDown(KeyCode.I)){
            if(inventoryCanvas.isActiveAndEnabled){
                inventoryCanvas.enabled = false;
                lockCursor();
            }else{
                if(allowInput){
                    inventoryCanvas.enabled = true;
                    unlockCursor();
                }   
            }
            if(inventoryCanvas.enabled){
                
            }else{
                
                inv.destroyOption();
            }
            setAllowInput();
        }
        if(Input.GetKeyDown(KeyCode.BackQuote)){
            consoleObject.SetActive(!consoleObject.activeSelf);
            if(consoleObject.activeSelf){
                unlockCursor();
            }else{
                lockCursor();
            }
            setAllowInput();
        }
        //Movement
        
        //20 = Recoil Speed change this value to change how fast the recoil acts
        //Smooth recoil and compensation 
        recoil = Mathf.Lerp(recoil, 0, Time.deltaTime * recoilRate);
        compensation = Mathf.Lerp(compensation, 0, Time.deltaTime * compensationRate);

        //Checks if the player is on the ground
        if (Physics.Raycast(transform.position, Vector3.down, 1.3f))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
        //Locks cursor in game when clicked on
        if (Input.GetMouseButtonDown(0))
        {
            

        }

        //Camera rotations
        rotationZ = Mathf.Lerp(rotationZ,tilt,Time.deltaTime*10);
        if (rotationY > maxAngle)
            rotationY = Mathf.SmoothDamp(rotationY,maxAngle,ref testVel,0.05f);
        if (rotationY < minAngle)
            rotationY  = Mathf.SmoothDamp(rotationY,minAngle,ref testVel, 0.05f);;
        if(allowInput){
            rotationX += Input.GetAxis("Mouse X") * sensitivity_x;
            rotationY += (Input.GetAxis("Mouse Y") * sensitivity_x) + recoil + compensation;
            player.rotation = Quaternion.Euler(0, rotationX, 0);
            camera.rotation = Quaternion.Euler(-rotationY, camera.eulerAngles.y, rotationZ);
        //camera.rotation = Quaternion.Euler(-rotationY, camera.eulerAngles.y, camera.eulerAngles.z);
        }
        
        if (Input.GetAxis("Horizontal") != 0)
        {
            tilt = -Input.GetAxis("Horizontal");
        }
    }

    public void lockCursor()
    {
        //Sets cursor in game invisible and locked in the middle.
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public void unlockCursor(){
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Recoil(float amt)
    {
        recoil = amt;
    }
    public void recovery(float amt)
    {
        compensation = amt;
    }

    public void setAllowInput(){
        if(inventoryCanvas.enabled || consoleObject.activeSelf || dialogueManager.GetComponent<DialogueManager>().currentDialogue!=null){
            allowInput = false;
        }else{
            allowInput = true;
        }
        //moveDirection = Vector3.zero;
    }
}
