using System.Collections;
using System.Collections.Generic;
using Oculus.Interaction;
using UnityEngine;

public class WireSnapStay : MonoBehaviour
{
    public SnapInteractor interactor;
    public SnapInteractable interatable;

    public bool isWire = false;

    //private void Start()
    //{
    //    if(interactor!=null && interatable != null)
    //    {
    //        PerformSnap();
    //    }
    //}

    public void PerformSnap()
    {
        Debug.Log("Setting select interactable");

        if(isWire)
        {
            SegmentPlugBehaviour plugBehaviour = interactor.transform.parent.GetComponent<SegmentPlugBehaviour>();
            if(plugBehaviour != null)
            {
                Debug.Log("plug behaviour is not null");

                plugBehaviour.PlugGrab();
            }
        }

        interactor.SetSelectInteratable(interatable);

        Debug.Log("Setted the interactable");
    }

    public void PerformUnSnap()
    {
        Debug.Log("UnSetting select interactable");

        interactor.UnselectInteractable();

        Debug.Log("Unsetted the interactable");
    }
}
