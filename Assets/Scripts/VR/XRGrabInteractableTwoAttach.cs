using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRGrabInteractableTwoAttach : XRGrabInteractable // we overwrite its functions
{
    public Transform leftAttachTransform;
    public Transform rightAttachTransform;
    [SerializeField] Transform leftHand;
    [SerializeField] Transform rightHand;

    protected override void OnSelectEntered(SelectEnterEventArgs args){
        if(args.interactorObject.transform.CompareTag("Left Hand")){
            attachTransform = leftAttachTransform;
            transform.parent = leftHand;
        }
        else if(args.interactorObject.transform.CompareTag("Right Hand")){
            attachTransform = rightAttachTransform;
            transform.parent = rightHand;
        }

        base.OnSelectEntered(args);
    }
    
}
