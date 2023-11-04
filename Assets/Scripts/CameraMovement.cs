using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] GameObject CameraParent;

    [SerializeField] float mouseSensivity;
    [SerializeField] float CameraParentMinRotationX;
    [SerializeField] float CameraParentMaxRotationX;

    bool isCorrectRotationOfPlayerSet = false;

    [SerializeField] new Camera camera;

    [SerializeField] Vector3 cameraPosition;

    [SerializeField] PlayerMovement playerMovement;

    void Start(){
        cameraPosition = camera.transform.position;
    }
  
    void FixedUpdate()
    {
        CameraParent.transform.Rotate(Input.GetAxis("Mouse Y") * mouseSensivity * Time.deltaTime,0 ,0,Space.Self);
        CameraParent.transform.localEulerAngles = new Vector3(ClampAngle(CameraParent.transform.localEulerAngles.x, CameraParentMinRotationX, CameraParentMaxRotationX),CameraParent.transform.localEulerAngles.y,CameraParent.transform.localEulerAngles.z);

        if(!playerMovement.playerIsMoving){
            CameraParent.transform.Rotate(0, Input.GetAxis("Mouse X") * mouseSensivity * Time.deltaTime,0,Space.World);
            isCorrectRotationOfPlayerSet = false;
        }else{
            if(!isCorrectRotationOfPlayerSet){
                transform.eulerAngles = new Vector3(transform.localEulerAngles.x,CameraParent.transform.eulerAngles.y,transform.localEulerAngles.z);
                CameraParent.transform.localEulerAngles = new Vector3(CameraParent.transform.localEulerAngles.x,0,0);
                isCorrectRotationOfPlayerSet = true;

            }
            transform.Rotate(0, Input.GetAxis("Mouse X") * mouseSensivity * Time.deltaTime,0);
            camera.transform.Rotate(0, Input.GetAxis("Mouse X") * mouseSensivity * Time.deltaTime,0);
            camera.transform.localEulerAngles = new Vector3(camera.transform.localEulerAngles.x,0,0);
        }
    }
    
    static float ClampAngle(float angle, float min, float max){
         if (angle<90 || angle>270){       // if angle in the critic region...
         if (angle>180) angle -= 360;  // convert all angles to -180..+180
         if (max>180) max -= 360;
         if (min>180) min -= 360;
     }    
     angle = Mathf.Clamp(angle, min, max);
     if (angle<0) angle += 360;  // if angle negative, convert to 0..360
     return angle;
    }
}