using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    #region editor exposed variables
    [Header("Referances")]
    [SerializeField] private CharacterController controller;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform mainCamera;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private LayerMask VisibilityLayersWithVisualizer;
    [SerializeField] private LayerMask VisibilityLayersWithoutVisualizer;
  

    [Header("Settings")]
    [Range(1f, 30f)]
    [SerializeField] private float speed = 12f;

    [Tooltip("Fall speed")]
    [Range(-50f, 0f)]
    [SerializeField] private float gravity = -15f;

    [Range(1f,10f)]
    [SerializeField] private float jumpHeight = 3f;

    // Size of ground check sphere
    // Increase if isGrounded is never true 
    // Decrease if isGrounded is always true 
    [Tooltip("Size of ground check sphere. Increase if isGrounded is never true. Decrease if isGrounded is always true ")]
    [Range(0.1f,2f)]
    [SerializeField] private float groundDistance = 0.5f;

    [Range(100f,1000f)]
    [SerializeField] private float mouseSensitivity = 250f;

    [Header("Keybinds")]
    [SerializeField] private KeyCode pauseButton = KeyCode.P;

    [SerializeField] private KeyCode visualizerButton = KeyCode.V;

    #endregion //End of editor exposed variables


    private bool canSeeAP = true;
    private float pauseStartTime = -999f;
    private const float pauseCooldown = 0.3f;
    private Vector3 velocity;
    private float xRotation = 0f;
    private bool isGrounded;
    private Ray checkGround;
    private float footStepDelay = 0.8f;
    private float nextFootStep = 0;
    private bool changeStep;
    private bool jumpControlBool;

    private GameObject canvas = null;
    private GameObject pauseMenu;
    
    private GameManager gameManager = null;
    


    void Start(){

        // check for audio manager
        if (AudioManager.instance == null)
        {
            // create it if it doesn't exist
            GameObject audioManager = new GameObject("AudioManager");
            audioManager.AddComponent<AudioManager>();
        }


        Cursor.lockState = CursorLockMode.Locked;

        if(mainCamera == null) mainCamera = Camera.main.gameObject.transform;

        gameManager = GameManager.Find();

    }

    void Update() {
        UIControl();

        if(!GameManager.GamePaused){
            Movement();
            CameraControl();
            AudioControl();
        }
    }


    private void UIControl(){

        if(Input.GetKeyDown(visualizerButton)){ //Toggle enable viewing AP coverage

            Camera.main.cullingMask = (canSeeAP) ? VisibilityLayersWithVisualizer : VisibilityLayersWithoutVisualizer;
            canSeeAP = !canSeeAP;
        }

        //Update UI control settings
        if(GameManager.GamePaused && Cursor.lockState != CursorLockMode.Confined){  //Game is paused ... needs player UI update
            gameManager.UI.PauseGame(true);

            //Debug.Log("Game PAUSED, updating UI " + GameManager.GamePaused);

        }else if(!GameManager.GamePaused && Cursor.lockState == CursorLockMode.Confined){ //Game is unpaused ... needs player UI update
            gameManager.UI.PauseGame(false);

            //Debug.Log("Game unpaused, updating UI " + GameManager.GamePaused);

        }else if(Input.GetKeyDown(pauseButton) && Time.time - pauseStartTime > pauseCooldown){ //Check if player is trying to pause game
            
            pauseStartTime = Time.time;
            gameManager.UI.PauseGame(true);
            
            //Debug.Log("Setting pause state to " + GameManager.GamePaused);
            
        }

        //Debug.Log("Can pause: " + (Time.deltaTime - pauseStartTime > pauseCooldown));
    }

    private void Movement(){
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        
        // Reset velocity if grounded
        if (isGrounded && velocity.y < 0) {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Only move in the X and Z directions if doing so wouldn't make you fall off
        Vector3 move = (transform.right * x + transform.forward * z) * speed * Time.deltaTime;
        Vector3 moveX = Vector3.Scale(move, Vector3.right);
        Vector3 moveZ = Vector3.Scale(move, Vector3.forward);


        if (IsAboveGround(transform.position + moveX)) {
            controller.Move(moveX);
        }
        
        if (IsAboveGround(transform.position + moveZ))         {
            controller.Move(moveZ);
        }

        if (Input.GetButtonDown("Jump") && isGrounded) {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }


        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private void CameraControl(){
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        mainCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    private void AudioControl()
    {
        if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A)) 
            && isGrounded)
        {
            nextFootStep -= Time.deltaTime;
            if (nextFootStep <= 0)
            {
                if (changeStep)
                {
                    changeStep = false;
                    AudioManager.instance.PlayOneShot("PlayerStepL");
                    // attempt at faster footstep sounds at faster speeds
                    nextFootStep += (footStepDelay - (speed / 75));
                }
                else
                {
                    changeStep = true;
                    AudioManager.instance.PlayOneShot("PlayerStepR");
                    // attempt at faster footstep sounds at faster speeds
                    nextFootStep += (footStepDelay - (speed / 75));
                }
            }
        }

        // player is in the air
        if (!isGrounded)
        {
            jumpControlBool = true;
        }

        // play the sound when the player lands
        if (jumpControlBool && isGrounded)
        {
            AudioManager.instance.PlayOneShot("PlayerJumpLanding");
            jumpControlBool = false;
        }

        
        
        

        
    }

    bool IsAboveGround(Vector3 position) {
        return Physics.Raycast(position, transform.TransformDirection(Vector3.down), out _, Mathf.Infinity, groundMask);
    }

}
