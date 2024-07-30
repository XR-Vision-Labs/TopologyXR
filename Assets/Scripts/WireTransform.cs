using System.Collections;
using System.Collections.Generic;
using Oculus.Interaction;
using Oculus.Interaction.HandGrab;
using UnityEngine;

public class WireTransform : MonoBehaviour
{
        private HandGrabInteractable handGrabInteractable;
        private GrabInteractable grabInteratable;

        //private void OnEnable()
        //{
        //    TransformMenu.WireTransform += EnableWire;
        //    TransformMenu.ServerTransform += DisableWire;
        //    TransformMenu.RackTransform += DisableWire;

        //    handGrabInteractable = GetComponentInChildren<HandGrabInteractable>();
        //    grabInteratable = GetComponentInChildren<GrabInteractable>();
        //}

        //private void OnDisable()
        //{
        //    TransformMenu.WireTransform -= EnableWire;
        //    TransformMenu.ServerTransform -= DisableWire;
        //    TransformMenu.RackTransform -= DisableWire;
        //}

    public void ApplyRackTransform()
    {

        DisableWire();
    }
    public void ApplyServerTransform()
    {
        DisableWire();
    }
    public void ApplyWireTransform()
    {
        EnableWire();
    }

    private void EnableWire()
    {
        WireTransformF(true);
    }

    private void DisableWire()
    {
        WireTransformF(false);

    }

    private void WireTransformF(bool val)
    {
        //if (handGrabInteractable != null)
        //{
        //    SampleController.Instance.Log(typeof(WireTransform) + "Setting hand grab interaction to " + val);
        //    handGrabInteractable.enabled = val;
        //}
        //else
        //{
        //    SampleController.Instance.Log("Hand grab interactable not found");

        //    GetComponentInChildren<HandGrabInteractable>().enabled = val;

        //    SampleController.Instance.Log("Grab found and setted");
        //}

        //if (grabInteratable != null)
        //{
        //    SampleController.Instance.Log(typeof(WireTransform) + "Setting grab interaction to " + val);

        //    grabInteratable.enabled = val;
        //}
        //else
        //{
        //    SampleController.Instance.Log("Grab interactable not found");

        //    GetComponentInChildren<GrabInteractable>().enabled = val;

        //    SampleController.Instance.Log("Grab found and setted");
        //}
    }
}
