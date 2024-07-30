using Oculus.Interaction;
using Oculus.Interaction.HandGrab;
using UnityEngine;

public class ServerGrabManager : MonoBehaviour
{
    public HandGrabInteractable handGrabInteractable;
    public GrabInteractable grabInteractable;

    public GameObject plugParent;
    private bool isEnabled = false;

    private void OnEnable()
    {
        //TransformMenu.ServerTransform += EnableServerTransform;
        ////TransformMenu.ServerTransform += DisablePlugs;

        //TransformMenu.WireTransform += DisableServerTransform;
        ////TransformMenu.WireTransform += EnablePlugs;

        //TransformMenu.RackTransform += DisableServerTransform;
        ////TransformMenu.RackTransform += DisablePlugs;


        //handGrabInteractable = GetComponentInChildren<HandGrabInteractable>();
        //grabInteractable = GetComponentInChildren<GrabInteractable>();
    }

    //private void OnDisable()
    //{
    //    TransformMenu.ServerTransform -= EnableServerTransform;
    //    //TransformMenu.ServerTransform -= DisablePlugs;

    //    TransformMenu.WireTransform -= DisableServerTransform;
    //    //TransformMenu.WireTransform -= EnablePlugs;

    //    TransformMenu.RackTransform -= DisableServerTransform;
    //    //TransformMenu.RackTransform -= DisablePlugs;
    //}

    public void ToggleTransform()
    {
        if(isEnabled)
        {
            isEnabled = false;
            DisableServerTransform();
        }
        else
        {
            isEnabled = true;
            EnableServerTransform();
        }
    }

    public void EnableServerTransform()
    {
        ServerTransform(true);

        
    }

    public void DisableServerTransform()
    {

        ServerTransform(false);

        
    }

    private void ServerTransform(bool val)
    {
        if (handGrabInteractable != null)
        {
            SampleController.Instance.Log(typeof(ServerGrabManager) + " Setting hand grab interaction to " + val);
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
            SampleController.Instance.Log(typeof(ServerGrabManager) + " Setting grab interaction to " + val);

            grabInteractable.enabled = val;
        }
        else
        {
            SampleController.Instance.Log("Grab interactable not found trying to find");
            GetComponentInChildren<GrabInteractable>().enabled = val;

            SampleController.Instance.Log("Grab found and setted");

        }


    }

    public void ApplyRackTransform()
    {
        //DisablePlugs();
        DisableServerTransform();
    }

    public void ApplyServerTransform()
    {
        EnableServerTransform();
        //DisablePlugs();
    }

    public void ApplyWireTransform()
    {
        DisableServerTransform();
        //EnablePlugs();
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
