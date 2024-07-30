//using Oculus.Interaction;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class ServerInteractableAddon : MonoBehaviour
//{
//    private SnapInteractable interactable;
//    private Transform parent;

//    private ServerSnapBehaviour snapBehaviour;

//    private void Start()
//    {
//        interactable= GetComponent<SnapInteractable>();
//        if (interactable == null)
//        {
//            Debug.Log("Interactable not found so returning");
//            return;
//        }

//        Debug.Log("Assigning functions to different events");

//        interactable.WhenInteractorViewAdded += OnInteractorViewAdded;
//        interactable.WhenInteractorViewRemoved += OnInteractorViewRemoved;
//        interactable.WhenSelectingInteractorViewAdded += OnSelectingInteractorAdded;
//        interactable.WhenSelectingInteractorViewRemoved += OnSelectingInteractorRemoved;
//    }

//    private void OnDestroy()
//    {
//        Debug.Log("UnAssigning functions to different events");

//        interactable.WhenInteractorViewAdded -= OnInteractorViewAdded;
//        interactable.WhenInteractorViewRemoved -= OnInteractorViewRemoved;
//        interactable.WhenSelectingInteractorViewAdded -= OnSelectingInteractorAdded;
//        interactable.WhenSelectingInteractorViewRemoved -= OnSelectingInteractorRemoved;
//    }

    

//    public void OnInteractorViewAdded(IInteractorView view)
//    {
//        Debug.Log("Interactor view added");

//        parent = transform.parent.parent.parent;

//        SnapInteractor interactor

//        if (parent != null)
//        {
//            Debug.Log("parent name is " + parent.name);
//            snapBehaviour = parent.GetComponent<ServerSnapBehaviour>();
//            int size = CheckSize(interactor.transform);

//            int result = snapBehaviour.CheckAvailability(this.transform, size);
//            Debug.Log("Result is " + result);

//            if (result == -1)
//            {
//                SampleController.Instance.Log("Snap is not available");
//                return;
//            }

//            int index = snapBehaviour.GetIndex(transform.parent);
//            Debug.Log("Index is " + index);

//            if (size == 1)
//            {
//                Debug.Log("It is 1u so no further work");
//                // but set unavailable

//                snapBehaviour.AddToList(3, index);

//                return;
//            }

//            Debug.Log("Index is " + index);
//            snapBehaviour.AddToList(result, index, size);
//            Debug.Log("Adding the snap to list");

//            if (result == 1)
//            {
//                // add transfrom to interactor
//                SampleController.Instance.Log("Injecting down transfrom");
//                Transform t = interactor.transform.GetChild(1);

//                interactor.InjectOptionalSnapPoseTransform(t);
//            }

//            else if (result == 2)
//            {
//                SampleController.Instance.Log("Injecting up transfrom");
//                Transform t = interactor.transform.GetChild(0);

//                interactor.InjectOptionalSnapPoseTransform(t);
//            }
//        }

//        Debug.Log("Interactable state in snap is " + State);

//        // get the size of interactor
//        // get the index of interactable
//        // check for availability
//        // give some green signal that we can snap

//    }
//    public void OnInteractorViewRemoved(IInteractorView view)
//    {
//        Debug.Log("Interactor view Removed");
       
//    }

//    public void OnSelectingInteractorAdded(IInteractorView view)
//    {
//        Debug.Log("Selecting Interactor view Added");

//        // snap only if we got the signal that it is available
//        // mark the snap place unavailable and also mark on other places with this index
//    }

//    public void OnSelectingInteractorRemoved(IInteractorView view)
//    {
//        Debug.Log("Selecting Interactor view Removed");

//        // remove the object from list
//    }

//    private int CheckSize(Transform interactor)
//    {
//        var tag = interactor.parent.tag;
//        SampleController.Instance.Log("Interactor tag is " + tag);

//        switch (tag)
//        {
//            case "1U":
//                return 1;

//            case "2U":
//                return 2;
//            case "4U":
//                return 4;
//        }

//        return -1;
//    }
//}
