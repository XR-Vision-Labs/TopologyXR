using UnityEngine;
using Oculus.Interaction;

public class CustomGrab : OVRGrabbable
{
    //private OVRGrabbable grabbable;
    //private Transform grabbedByTransform; // Store the hand's transform when grabbed

    //private void Start()
    //{
    //    grabbable = transform.GetComponentInParent<OVRGrabbable>();
    //    grabbable.OnGrabBegin += OnGrabBegin;
    //    grabbable.OnGrabEnd += OnGrabEnd;
    //}

    //private void Update()
    //{
    //    // If the cube is currently grabbed, update its position and rotation to match the hand
    //    if (grabbable.isGrabbed)
    //    {
    //        // Make sure grabbedByTransform is not null (in case of unexpected situations)
    //        if (grabbedByTransform != null)
    //        {
    //            // Set the cube's position and rotation to match the hand
    //            transform.position = grabbedByTransform.position;
    //            transform.rotation = grabbedByTransform.rotation;
    //        }
    //    }
    //}

    //private void OnGrabBegin()
    //{
    //    // Cube is grabbed, store the hand's transform
    //    if (grabbable.grabbedBy != null)
    //    {
    //        grabbedByTransform = grabbable.grabbedBy.transform;
    //    }
    //}

    //private void OnGrabEnd()
    //{
    //    // Cube is released, reset the stored hand's transform
    //    grabbedByTransform = null;
    //}




}
