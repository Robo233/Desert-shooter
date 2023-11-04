using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.UI;

public class PistolVR : MonoBehaviour
{
   [SerializeField] GameObject impactEffect;
   [SerializeField] GameObject pistolBarrel;

   [SerializeField] float damage;
   [SerializeField] float range;
   [SerializeField] float impactForce;
   [SerializeField] float fireRate;

   float nextTimeToFire = 0f;

   [SerializeField] ParticleSystem muzzleFlash;

   [SerializeField] Image mark;

   [SerializeField] new Camera camera;

    void Start()
    {
        XRGrabInteractable grabbable = GetComponent<XRGrabInteractable>();
        grabbable.activated.AddListener(FireBullet);
        
    }

    void Update(){
        RaycastHit target;
        Physics.Raycast(transform.position, transform.forward, out target);
    
    }

    public void FireBullet(ActivateEventArgs args = null){
        if(Time.time >= nextTimeToFire){
            nextTimeToFire = Time.time + 1f/fireRate;
            muzzleFlash.Play();
            RaycastHit hit;
            
            if( Physics.Raycast(pistolBarrel.transform.position, transform.forward, out hit, range)){
                Target target = hit.transform.GetComponent<Target>();
                GameObject impactGameObject = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                impactGameObject.transform.parent = hit.transform;
                if(target){
                    target.TakeDamage(damage);
                }
            }
            if(hit.rigidbody){
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }

 
            }
        }
}