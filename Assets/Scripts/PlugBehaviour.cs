using IndieMarc.CurvedLine;
using Oculus.Interaction;
using UnityEngine;

public class PlugBehaviour : MonoBehaviour
{
    public short nConnections=0;
    public CurvedLine3D controller;
    public GameObject startWire;

    public SnapInteractor interactor;
    public SnapInteractable interactable;
    private bool isConnected = false;

    private void Start()
    {
        controller = GetComponent<CurvedLine3D>();

        GetWirePlugs();
    }

    public void GetWirePlugs()
    {
        startWire = controller.GetStart();
        if (startWire != null)
        {
            interactor = startWire.GetComponentInChildren<SnapInteractor>();
            SampleController.Instance.Log("Got the interactor");
        }
    }

    public void PlugConnected()
    {
        SampleController.Instance.Log("Plug connected");
        nConnections+=1;

        SampleController.Instance.Log("Total plugs connected " + nConnections);

        if (checkConnection())
        {
            OnConnected();
        }
    }

    public void PlugDisconnected()
    {
        SampleController.Instance.Log("Plug disconnected");
        nConnections--;
        SampleController.Instance.Log("Total plugs connected " + nConnections);

        if (isConnected)
        {
            SampleController.Instance.Log("Plug was connected so calling disconnect");
            OnDisconnected();
        }
    }

    private void OnConnected()
    {
        SampleController.Instance.Log("Connection established");
        isConnected = true;

        if (interactor != null)
        {
            interactable = interactor.Interactable;
            SampleController.Instance.Log("Got the snap interactable " + interactable.name);
        }

        var parent = interactable.transform.parent.parent;
        if (parent != null)
        {
            SampleController.Instance.Log("Parent is not null so calling connection function " + parent.name);
            parent.GetComponent<ConnectionManager>().OnConnected();
        }
    }

    private void OnDisconnected()
    {
        isConnected = false;
        SampleController.Instance.Log("Connection removed");
        var parent = interactable.transform.parent.parent;

        if (interactable != null && parent != null)
        {
            SampleController.Instance.Log("Inside disconnection fn, saved interactable not null");

            SampleController.Instance.Log("Parent is not null so calling connection function " + parent.gameObject.name);
            parent.GetComponent<ConnectionManager>().OnDisconnected();
        }   
        
    }

    private bool checkConnection()
    {
        if (nConnections == 2)
        {
            return true;
        }

        return false;
    }
}
//using Oculus.Interaction;
//using UnityEngine;

//public class PlugBehaviour : MonoBehaviour
//{
//    public short nConnections=0;
//    public WireController controller;
//    public GameObject startWire;

//    public SnapInteractor interactor;
//    public SnapInteractable interactable;
//    private bool isConnected = false;

//    private void Start()
//    {
//        controller = GetComponent<WireController>();

//        GetWirePlugs();
//    }

//    public void GetWirePlugs()
//    {
//        startWire = controller.starAnchorTemp.gameObject;
//        if (startWire != null)
//        {
//            interactor = startWire.GetComponentInChildren<SnapInteractor>();
//            SampleController.Instance.Log("Got the interactor");
//        }
//    }

//    public void PlugConnected()
//    {
//        SampleController.Instance.Log("Plug connected");
//        nConnections+=1;

//        SampleController.Instance.Log("Total plugs connected " + nConnections);

//        if (checkConnection())
//        {
//            OnConnected();
//        }
//    }

//    public void PlugDisconnected()
//    {
//        SampleController.Instance.Log("Plug disconnected");
//        nConnections--;
//        SampleController.Instance.Log("Total plugs connected " + nConnections);

//        if (isConnected)
//        {
//            SampleController.Instance.Log("Plug was connected so calling disconnect");
//            OnDisconnected();
//        }
//    }

//    private void OnConnected()
//    {
//        SampleController.Instance.Log("Connection established");
//        isConnected = true;

//        if (interactor != null)
//        {
//            interactable = interactor.Interactable;
//            SampleController.Instance.Log("Got the snap interactable " + interactable.name);
//        }

//        var parent = interactable.transform.parent.parent;
//        if (parent != null)
//        {
//            SampleController.Instance.Log("Parent is not null so calling connection function " + parent.name);
//            parent.GetComponent<ConnectionManager>().OnConnected();
//        }
//    }

//    private void OnDisconnected()
//    {
//        isConnected = false;
//        SampleController.Instance.Log("Connection removed");
//        var parent = interactable.transform.parent.parent;

//        if (interactable != null && parent != null)
//        {
//            SampleController.Instance.Log("Inside disconnection fn, saved interactable not null");

//            SampleController.Instance.Log("Parent is not null so calling connection function " + parent.gameObject.name);
//            parent.GetComponent<ConnectionManager>().OnDisconnected();
//        }   
        
//    }

//    private bool checkConnection()
//    {
//        if (nConnections == 2)
//        {
//            return true;
//        }

//        return false;
//    }
//}
