using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CustomSnapInteractable : SnapInteractable
{
    public bool isWire = false;
    public bool isServer = false;
    private PlugBehaviour plugBehaviour;
    private int size;
    private int index;
    private Transform parent;

    private ServerSnapBehaviour snapBehaviour;
    private static bool canSnap = false;
    private int result = -1;
    public UnityEngine.Color normalColor;
    private MaterialPropertyBlockEditor editor;
    private BoxCollider collider;
    private List<MaterialPropertyColor> redColor;
    private List<MaterialPropertyColor> noColor;

    private SnapInteractor interactor2;

    protected override void Start()
    {
        base.Start();

        if (isServer)
        {

        var properties = new MaterialPropertyColor
        {
            name = "_BorderColor",
            value = normalColor
        };

        noColor = new List<MaterialPropertyColor>() { properties };

        properties.value = UnityEngine.Color.red;

        redColor = new List<MaterialPropertyColor>() { properties };

        editor = transform.parent.GetComponent<MaterialPropertyBlockEditor>();
        collider = transform.parent.GetComponent<BoxCollider>();
        }
    }

    private int CheckSize(Transform interactor)
    {
        var tag = interactor.parent.tag;
        SampleController.Instance.Log("Interactor tag is " + tag);

        switch (tag)
        {
            case "1U":
                return 1;

            case "2U":
                return 2;
            case "4U":
                return 4;
        }

        return -1;
    }


    protected override void InteractorAdded(SnapInteractor interactor)
    {
        base.InteractorAdded(interactor);

        Debug.Log("Interator added function");
        SampleController.Instance.Log("Interator added function");

        if (isServer)
        {
            parent = transform.parent.parent.parent;

            if (parent != null)
            {
                Debug.Log("parent name is " + parent.name);
                SampleController.Instance.Log("parent name is " + parent.name);

                snapBehaviour = parent.GetComponent<ServerSnapBehaviour>();
                size = CheckSize(interactor.transform);

                result = snapBehaviour.CheckAvailability(this.transform, size);

                
                Debug.Log("Result is " + result);
                SampleController.Instance.Log("Result is " + result);

                if (result == -1)
                {
                    SampleController.Instance.Log("Snap is not available");

                    UnavailableBehaviour();

                    return;
                }

                Debug.Log("The place is available so setting canSnap to true");
                SampleController.Instance.Log("The place is available so setting canSnap to true");
                canSnap = true;
            }
        }

    }

    private void UnavailableBehaviour()
    {
        SampleController.Instance.Log("Got the result -1 so performing unavailable behaviour");

        // disable snap until unhover
        // mark the color red



        if (editor != null)
        {
            SampleController.Instance.Log("Got the editor now changing color");
            transform.parent.GetComponent<MaterialPropertyBlockEditor>().ColorProperties = redColor;
        }

        if (collider)
        {
            SampleController.Instance.Log("Got the collider not setting it to off");
            collider.enabled = false;
        }

    }

    private void RemoveUnavailable()
    {
        SampleController.Instance.Log("Got the result -1 so performing removing unavailable behaviour");


        if (editor != null)
        {
            SampleController.Instance.Log("Got the editor now changing color");
            transform.parent.GetComponent<MaterialPropertyBlockEditor>().ColorProperties = noColor;
        }

        if (collider)
        {
            SampleController.Instance.Log("Got the collider now setting it to on");
            collider.enabled = true;
        }
    }


    protected override void InteractorRemoved(SnapInteractor interactor)
    {

        base.InteractorRemoved(interactor);

        Debug.Log("Interactor removed function");
        SampleController.Instance.Log("Interactor removed function");

        if (result == -1 && isServer)
        {
            SampleController.Instance.Log("No space so calling removeunavailable function");
            RemoveUnavailable();
        }

    }


    protected override void SelectingInteractorAdded(SnapInteractor interactor)
    {
        SampleController.Instance.Log("Selecting interactor called");
        if (result == -1 && isServer)
        {
            
            if (HasInteractor(interactor))
            {
                
                RemoveInteractor(interactor);

            }
            return;
        }

        base.SelectingInteractorAdded(interactor);

        if (isWire)
        {
#if UNITY_EDITOR

            transform.parent.GetComponent<SegmentPlugBehaviour>().PlugGrab();
            Debug.Log("Calling plug grab function");

#endif

            interactor2 = interactor;

            SampleController.Instance.Log("Wire connect coroutine called");
            StartCoroutine(WireConnect());
        }

        Debug.Log("Can snap is " + canSnap);
        SampleController.Instance.Log("Can snap is " + canSnap);


        if (isServer && canSnap)
        {
            canSnap = false;
            index = snapBehaviour.GetIndex(transform.parent);
            Debug.Log("Index is " + index);
            SampleController.Instance.Log("Index is " + index);


            if (size == 1)
            {
                Debug.Log("It is 1u so no further work");
                SampleController.Instance.Log("It is 1u so no further work");

                // but set unavailable

                snapBehaviour.AddToList(3, index);

                return;
            }

            Debug.Log("Index is " + index);
            SampleController.Instance.Log("Index is " + index);

            snapBehaviour.AddToList(result, index, size);
            Debug.Log("Adding the snap to list");
            SampleController.Instance.Log("Adding the snap to list");


            if (result == 1)
            {
                // add transfrom to interactor
                SampleController.Instance.Log("Injecting down transfrom");

                Transform t = interactor.transform.GetChild(1);

                interactor.InjectOptionalSnapPoseTransform(t);
            }

            else if (result == 2)
            {
                SampleController.Instance.Log("Injecting up transfrom");
                Transform t = interactor.transform.GetChild(0);

                interactor.InjectOptionalSnapPoseTransform(t);
            }
        }

        Debug.Log("Interactable state in snap is " + State);
        SampleController.Instance.Log("Interactable state in snap is " + State);



        //SendSnapData(interactor);
    }

    private IEnumerator WireConnect()
    {
        yield return new WaitForEndOfFrame();

        SampleController.Instance.Log("Wire connect routine working");

        plugBehaviour = interactor2.transform.parent.parent.GetComponent<PlugBehaviour>();
        SampleController.Instance.Log("Inside select interactor function with wire");
        if (plugBehaviour != null)
        {
            SampleController.Instance.Log("Got the plug behaviour and calling connect function");
            plugBehaviour.PlugConnected();
        }
    }

    protected override void SelectingInteractorRemoved(SnapInteractor interactor)
    {
        base.SelectingInteractorRemoved(interactor);


        if (isWire)
        {
            if (plugBehaviour == null)
            {
                plugBehaviour = interactor.transform.parent.parent.GetComponent<PlugBehaviour>();
            }
            SampleController.Instance.Log("Inside unselect interactor function with wire");

            plugBehaviour.PlugDisconnected();
        }

        if (isServer && parent != null)
        {
            Debug.Log("Interactable state is " + State);
            SampleController.Instance.Log("Interactable state is " + State);


            Debug.Log("Performing the server interaction remove function");
            SampleController.Instance.Log("Performing the server interaction remove function");


            // first check size
            int size = CheckSize(interactor.transform);
            Debug.Log("Size is " + size);

            // get index
            int index = snapBehaviour.GetIndex(transform.parent);
            Debug.Log("Index is " + index);

            // if 1u remove the space from list
            if (size == 1)
            {
                snapBehaviour.RemoveFromList(3, index, 1);
            }

            // check if snapped or not



            // check if used up or down

            int result = snapBehaviour.CheckOccupy(index, size);

            // accordingly release all the up and down snap places
            switch (result)
            {
                case 1:
                    Debug.Log("Got the result 1 so removing up places");
                    snapBehaviour.RemoveFromList(result, index, size);
                    break;

                case 2:
                    Debug.Log("Get the result 2 so removing down places");
                    snapBehaviour.RemoveFromList(result, index, size);
                    break;

                case -1:
                    Debug.Log("Got the result -1 so just returning");
                    break;
            }
        }

        //SendSnapData(interactor, false);
    }



    //private void SendSnapData(SnapInteractor interactor, bool forSelect = true)
    //{
    //    int interactorID = interactor.gameObject.GetPhotonView().ViewID;
    //    int interactableID = view.ViewID;

    //    // Call the PerformSnap RPC on all clients to synchronize the snap.
    //    view.RPC("PerformSnap", RpcTarget.Others, interactorID, forSelect);
    //}

    //[PunRPC]
    //public void PerformSnap(int interactorID, bool forSelect)
    //{

    //    SnapInteractor interactor = FindInteractorByID(interactorID);
    //    if (interactor != null && !forSelect)
    //    {
    //        Debug.Log("Got data for unselect so calling unselect function");
    //        SampleController.Instance.Log("Got data for unselect so calling unselect function");

    //        interactor.UnsetSelectInteractable();

    //    }
    //    else
    //    {
    //        Debug.Log("Got request for select so getting interactable with id and calling select function");
    //        SampleController.Instance.Log("Got request for select so getting interactable with id and calling select function");




    //        interactor.SetSelectInteratable(this);

    //        Debug.Log("Done with select");
    //        SampleController.Instance.Log("Done with select");

    //    }


    //    Debug.Log("Done rpc");
    //    SampleController.Instance.Log("Done rpc");
    //}

    //SnapInteractor FindInteractorByID(int interactorID)
    //{
    //    // Find the SnapInteractor with the specified PhotonView.ViewID.
    //    PhotonView[] photonViews = FindObjectsOfType<PhotonView>();
    //    foreach (PhotonView view in photonViews)
    //    {
    //        if (view.ViewID == interactorID)
    //        {
    //            return view.GetComponent<SnapInteractor>();
    //        }
    //    }
    //    return null;
    //}
}
