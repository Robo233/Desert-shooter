using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ActivateGrabRay : MonoBehaviour
{
    public GameObject leftGrabRay;
    public GameObject rightGrabRay;
    public GameObject leftteleportationRay;
    public GameObject rightteleportationRay;

    public XRDirectInteractor leftDirectGrab;
    public XRDirectInteractor rightDirectGrab;

    void Update()
    {
        leftGrabRay.SetActive(leftDirectGrab.interactablesSelected.Count == 0 && !leftteleportationRay.activeSelf); // the grab ray is activated only if the playe doesn't hold anything
       // rightGrabRay.SetActive(rightDirectGrab.interactablesSelected.Count == 0 && !rightteleportationRay.activeSelf);
    }
}
