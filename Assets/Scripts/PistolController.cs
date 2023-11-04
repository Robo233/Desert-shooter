using System.Collections;
using UnityEngine;

public class PistolController : MonoBehaviour
{
    [SerializeField] GameObject CameraParent;
    [SerializeField] GameObject crosshair;
    [SerializeField] GameObject impactEffect;
    [SerializeField] GameObject RightShoulder;
    [SerializeField] GameObject RightArm;

    [SerializeField] new Camera camera;

    [SerializeField] Vector3 aimCameraPosition;
    [SerializeField] Vector3 initialArmPosition;
    [SerializeField] Vector3 initialArmRotation;
    [SerializeField] Vector3 initialCameraPosition;

    [SerializeField] Quaternion ShoulderRotation;
    
    [SerializeField] float aimCameraTransitionTime;

    [SerializeField] ParticleSystem muzzleFlash;

    [SerializeField] LayerMask aimColliderMask = new LayerMask();

    bool correctCameraPositionWasSet = true;
    bool rightArmCorrectPositionWasSet = false;
     public bool isAiming;

    void LateUpdate()
    {
         if(Input.GetMouseButton(1)){
            isAiming = true;
            transform.eulerAngles = new Vector3(transform.localEulerAngles.x,CameraParent.transform.eulerAngles.y,transform.localEulerAngles.z);
            CameraParent.transform.localEulerAngles = new Vector3(CameraParent.transform.localEulerAngles.x,0,0);
            crosshair.SetActive(true);
            ShoulderRotation.x = camera.transform.localEulerAngles.x;


            RightShoulder.transform.localRotation = Quaternion.Euler(-(90-CameraParent.transform.localEulerAngles.x),RightShoulder.transform.localRotation.y,RightShoulder.transform.localRotation.z);
            if(!rightArmCorrectPositionWasSet){
            RightShoulder.transform.localRotation = Quaternion.Euler(ShoulderRotation.x-90, RightShoulder.transform.localEulerAngles.y, 0);
            rightArmCorrectPositionWasSet = true;
            }
            StartCoroutine(LerpFunctionPositionGameobjectToPosition(camera.gameObject,aimCameraPosition,aimCameraTransitionTime));  
               
            Vector2 screenCenterPoint = new Vector2(Screen.width/2f,Screen.height/2f);
            Ray ray = camera.ScreenPointToRay(screenCenterPoint);
            
            if(Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimColliderMask)){
                
                if (Input.GetMouseButtonDown(0)){ 
                    GameObject impactGameObject = Instantiate(impactEffect, raycastHit.point, Quaternion.LookRotation(raycastHit.normal));
                    impactGameObject.transform.parent = raycastHit.transform;
                    muzzleFlash.Play();
                }
            }
            correctCameraPositionWasSet = false;
        }else{
            isAiming = false;
            rightArmCorrectPositionWasSet = false;
            if(!correctCameraPositionWasSet){
                StartCoroutine(LerpFunctionPositionGameobjectToPosition(camera.gameObject,initialCameraPosition,aimCameraTransitionTime));
                crosshair.SetActive(false);
                RightArm.transform.localPosition = initialArmPosition;
                RightArm.transform.localEulerAngles = initialArmRotation;
                correctCameraPositionWasSet = true;
            }
            
        }
        
    }
    
     IEnumerator LerpFunctionPositionGameobjectToPosition(GameObject object1, Vector3 position, float duration)
    {
        float time = 0;
        Vector3 startValue = object1.transform.localPosition;
        while (time < duration)
        {
            object1.transform.localPosition = Vector3.Lerp(startValue, position, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        object1.transform.localPosition = position;

    }
}
