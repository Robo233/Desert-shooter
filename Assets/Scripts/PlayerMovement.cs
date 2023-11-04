using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
  //  [Space]
   
    [SerializeField] GameObject characterModel;

    [SerializeField] CharacterController Controller;
    [SerializeField] CharacterController ControllerCamera;

    [SerializeField] new Camera camera;

    [SerializeField] float runSpeed;
    [SerializeField] float sprintSpeed;
    [SerializeField] float jumpforce;
    [SerializeField] float gravity;
    [SerializeField] float cameraPositionY;

    float movementSpeed;
    float horizontalMovement = 0;
    float verticalMovement = 0;
    float rotationXCamera;
    float mouseInputY;
    float currentXRotation;

    Vector2 PlayerMouseInput;
    Vector2 PistolMouseInputY;
    Vector2 Velocity;

    Vector3 PlayerMovementInput;

    [SerializeField] Quaternion CamRotation;

    public bool playerIsMoving;
    bool fastRunPressed;

    int isRunningHash = Animator.StringToHash("isRunning");
    int isRunningSidewaysLeftHash = Animator.StringToHash("isRunningSidewaysLeft");
    int isRunningSidewaysRightHash = Animator.StringToHash("isRunningSidewaysRight");
    int isRunningDiagonallyLeft = Animator.StringToHash("isRunningDiagonallyLeft");
    int isRunningDiagonallyRight = Animator.StringToHash("isRunningDiagonallyRight");

    Animator animator;

    PistolController pistolController;

    [SerializeField] ParticleSystem muzzleFlash;

    bool isGoingForwardPressed;
    bool isGoingBackPressed;
    bool isGoingLeftPressed;
    bool isGoingRightPressed;

    void Start(){
        Cursor.lockState = CursorLockMode.Locked;
        CamRotation = camera.transform.localRotation;
        Physics.IgnoreLayerCollision(0,8,true);
        animator = characterModel.GetComponent<Animator>();
        pistolController = this.GetComponent<PistolController>();

    }

    void FixedUpdate(){
        isGoingForwardPressed = Input.GetKey("w");
        isGoingBackPressed = Input.GetKey("s");
        isGoingLeftPressed = Input.GetKey("a");
        isGoingRightPressed = Input.GetKey("d");

        bool isRunning = animator.GetBool(isRunningHash);

        MovePlayer();
        Jump();
        Sprint();

     

        if( ((isGoingForwardPressed || isGoingBackPressed) && !isGoingLeftPressed && !isGoingRightPressed) || (isGoingLeftPressed || isGoingRightPressed)){
            playerIsMoving = true;
        }else{
            playerIsMoving = false;
        }

    }

    void MovePlayer(){


        if( (isGoingForwardPressed  && isGoingLeftPressed && !isGoingRightPressed)  || (isGoingBackPressed  && isGoingRightPressed && !isGoingLeftPressed) &&  pistolController.isAiming  && !( (isGoingForwardPressed  && isGoingRightPressed)  || (isGoingBackPressed  && isGoingLeftPressed) && pistolController.isAiming) ){
            animator.SetBool(isRunningDiagonallyLeft,true);
            animator.SetBool(isRunningDiagonallyRight,false);
            animator.SetBool(isRunningSidewaysLeftHash,false);
            animator.SetBool(isRunningSidewaysRightHash,false);
            animator.SetBool(isRunningHash,false);
        }
        else if( (isGoingForwardPressed  && isGoingRightPressed  && !isGoingLeftPressed)  || (isGoingBackPressed  && isGoingLeftPressed && !isGoingRightPressed) && pistolController.isAiming){
            animator.SetBool(isRunningDiagonallyRight,true);
            animator.SetBool(isRunningDiagonallyLeft,false);
            animator.SetBool(isRunningSidewaysLeftHash,false);
            animator.SetBool(isRunningSidewaysRightHash,false);
            animator.SetBool(isRunningHash,false);
        }

        if( ((isGoingForwardPressed || isGoingBackPressed || isGoingRightPressed || isGoingLeftPressed) && !pistolController.isAiming) || (pistolController.isAiming && (isGoingForwardPressed || isGoingBackPressed) && !isGoingLeftPressed && !isGoingRightPressed ) ){
            animator.SetBool(isRunningHash,true);
            animator.SetBool(isRunningDiagonallyLeft,false);
            animator.SetBool(isRunningDiagonallyRight,false);
        }

        if(isGoingForwardPressed && !isGoingBackPressed  ){ // w
            verticalMovement = 1;
            if(!isGoingLeftPressed && !isGoingRightPressed  ){
                characterModel.transform.localEulerAngles = new Vector3(0,0,0);
                // StartCoroutine(LerpFunctionRotation(characterModel,new Vector3(0,0,0),0.5f));
            }
        }

        if(isGoingBackPressed && !isGoingForwardPressed ){ // s
            verticalMovement = -1;
            if( !isGoingLeftPressed && !isGoingRightPressed && !pistolController.isAiming ){
                characterModel.transform.localEulerAngles = new Vector3(0,180,0);
                //  StartCoroutine(LerpFunctionRotation(characterModel,new Vector3(0,180,0),0.5f));
            }
        }

        if(!isGoingForwardPressed && !isGoingBackPressed){
            verticalMovement = 0;

        }


        if(isGoingLeftPressed && !isGoingRightPressed ){ // a
            horizontalMovement = -1;
            if( !isGoingForwardPressed && !isGoingBackPressed && !pistolController.isAiming ){
                characterModel.transform.localEulerAngles = new Vector3(0,-90,0);
                // StartCoroutine(LerpFunctionRotation(characterModel,new Vector3(0,-90,0),0.5f));
            }
        }

        if(isGoingRightPressed && !isGoingLeftPressed ){ //d
            horizontalMovement = 1;
            if(isGoingRightPressed && !isGoingForwardPressed && !isGoingBackPressed && !pistolController.isAiming ){
                characterModel.transform.localEulerAngles = new Vector3(0,90,0);
                // StartCoroutine(LerpFunctionRotation(characterModel,new Vector3(0,90,0),0.5f));
            }
        }

        if(!isGoingRightPressed && !isGoingLeftPressed){
            horizontalMovement = 0;
        }


    if(!pistolController.isAiming){
        if(isGoingForwardPressed && isGoingLeftPressed){
            verticalMovement = 1;
            horizontalMovement = -1;
            characterModel.transform.localEulerAngles = new Vector3(0,-45,0);
            //  StartCoroutine(LerpFunctionRotation(characterModel,new Vector3(0,-45,0),0.5f));
            
        }

        if(isGoingForwardPressed && isGoingRightPressed){
            verticalMovement = 1;
            horizontalMovement = 1;
            characterModel.transform.localEulerAngles = new Vector3(0,45,0);
            //  StartCoroutine(LerpFunctionRotation(characterModel,new Vector3(0,45,0),0.5f));
            
        }
        if(isGoingBackPressed && isGoingLeftPressed){
            verticalMovement = -1;
            horizontalMovement = -1;
            characterModel.transform.localEulerAngles = new Vector3(0,225,0);
            //  StartCoroutine(LerpFunctionRotation(characterModel,new Vector3(0,45,0),0.5f));
            
        }
        if(isGoingBackPressed && isGoingRightPressed){
            verticalMovement = -1;
            horizontalMovement = 1;
            characterModel.transform.localEulerAngles = new Vector3(0,-225,0);
            // StartCoroutine(LerpFunctionRotation(characterModel,new Vector3(0,-225,0),0.5f));
            
        }

    }


      

        if(isGoingLeftPressed && !isGoingForwardPressed && !isGoingBackPressed && pistolController.isAiming){
            animator.SetBool(isRunningSidewaysLeftHash,true);
            animator.SetBool(isRunningSidewaysRightHash,false);
            animator.SetBool(isRunningDiagonallyLeft,false);
            animator.SetBool(isRunningDiagonallyRight,false);
        }
        if(isGoingRightPressed &&  !isGoingForwardPressed && !isGoingBackPressed && pistolController.isAiming){
            animator.SetBool(isRunningSidewaysRightHash,true);
            animator.SetBool(isRunningSidewaysLeftHash,false);
            animator.SetBool(isRunningDiagonallyLeft,false);
            animator.SetBool(isRunningDiagonallyRight,false);
        }
      

        if(!isGoingForwardPressed && !isGoingBackPressed && !isGoingLeftPressed && !isGoingRightPressed){
            animator.SetBool(isRunningHash,false);
            animator.SetBool(isRunningSidewaysLeftHash,false);
            animator.SetBool(isRunningSidewaysRightHash,false);
            animator.SetBool(isRunningDiagonallyLeft,false);
            animator.SetBool(isRunningDiagonallyRight,false);
        }

        PlayerMovementInput = new Vector3(horizontalMovement, 0f, verticalMovement);
        Vector3 MoveVector = transform.TransformDirection(PlayerMovementInput);
        Controller.Move(MoveVector * movementSpeed * Time.deltaTime);
        Controller.Move(Velocity * Time.deltaTime);

    }

    void Jump(){
        if(Controller.isGrounded){

            Velocity.y = -1f;
             if(Input.GetKeyDown(KeyCode.Space)){
                Velocity.y = jumpforce;
            }
               
        }else{
            Velocity.y -= gravity * -2f * Time.deltaTime;
        }
    }

    void Sprint(){
        fastRunPressed = Input.GetKey("left shift");  
        if(fastRunPressed){
        movementSpeed = sprintSpeed;
        }else{
        movementSpeed = runSpeed;
        }
    }

    IEnumerator LerpFunctionRotation(GameObject object1, Vector3 position, float duration){
        float time = 0;
        float startValue = object1.transform.localEulerAngles.y;
        while (time < duration)
        {
            float angle = Mathf.LerpAngle(startValue, position.y, time / duration);
            object1.transform.localEulerAngles = new Vector3(0, angle, 0);
            time += Time.deltaTime;
            yield return null;
        }
        object1.transform.localEulerAngles = position;
        
    }
}