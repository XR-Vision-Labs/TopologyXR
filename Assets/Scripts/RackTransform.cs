using Oculus.Interaction;
using Oculus.Interaction.HandGrab;
using UnityEngine;

public class RackTransform : MonoBehaviour
{
    public HandGrabInteractable handGrabInteractable;
    public GrabInteractable grabInteractable;

    private bool isEnabled = false;

    public GameObject plugParent;

 
    public void ToggleTransform()
    {
        if (isEnabled)
        {
            SampleController.Instance.Log("disabling rack transform");
            isEnabled = false;
            RackTransformfn(false);
        }
        else
        {
            SampleController.Instance.Log("enabling rack transform");
            isEnabled = true;
            RackTransformfn(true);
        }
    }

    public void EnableRackTransform()
    {
        RackTransformfn(true);
        Debug.Log("ENabling rack transform");

    }

    public void DisableRackTransform()
    {

        RackTransformfn(false);

        Debug.Log("DIsabling rack transform");
    }

    private void RackTransformfn(bool val)
    {
        if (handGrabInteractable != null)
        {
            SampleController.Instance.Log(typeof(RackTransform) + " Setting hand grab interaction to " + val);
            handGrabInteractable.enabled = val;
        }
        else
        {
            SampleController.Instance.Log("Hand grab interactable not found trying to find");
            GetComponentInChildren<HandGrabInteractable>().enabled = val;

            SampleController.Instance.Log("Hand grab found and setted");
        }

        if (grabInteractable != null)
        {
            SampleController.Instance.Log(typeof(RackTransform) + " Setting grab interaction to " + val);

            grabInteractable.enabled = val;
        }
        else
        {
            SampleController.Instance.Log("Grab interactable not found trying to find");
            GetComponentInChildren<GrabInteractable>().enabled = val;

            SampleController.Instance.Log("Grab found and setted");

        }


    }

    public void ApplyServerTransform()
    {
        DisableRackTransform();
        //EnablePlugs();
    }

    public void ApplyRackTransform()
    {
        EnableRackTransform();
        //DisablePlugs();
    }

    public void ApplyWireTransform()
    {
        DisableRackTransform();
        //DisablePlugs();
    }

    //private void EnablePlugs()
    //{
    //    SampleController.Instance.Log(typeof(ServerGrabManager) + " Enabling plug inteactables");
    //    plugParent.SetActive(true);
    //}

    //private void DisablePlugs()
    //{
    //    SampleController.Instance.Log(typeof(ServerGrabManager) + " Disabling plug inteactables");
    //    plugParent.SetActive(false);
    //}
}
