using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class PlugController : MonoBehaviour
{
    public bool isConnected = false;
    public UnityEvent OnWirePlugged;
    public Transform plugPosition;

    //public float rotationOffset = 180;

    [HideInInspector]
    public Transform endAnchor;
    [HideInInspector]
    public Rigidbody endAnchorRB;
    [HideInInspector]
    //public WireController wireController;
    public void OnPlugged()
    {
        Debug.Log("plug connected");
        OnWirePlugged.Invoke();
    }


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        Debug.Log("Trigger enter");
        if (other.CompareTag("End"))
        {
            endAnchor = other.transform;
            endAnchorRB = other.GetComponent<Rigidbody>();

            isConnected = true;
            endAnchorRB.isKinematic = true;
            endAnchor.transform.position = plugPosition.position;
            //Vector3 eulerRotation = new Vector3(0, -transform.eulerAngles.y, 0);
            //endAnchor.transform.rotation = Quaternion.Euler(eulerRotation);
            //endAnchor.transform.rotation = Quaternion.identity;

            endAnchor.transform.rotation = transform.rotation;


            OnPlugged();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Trigger exit");
        if(other.CompareTag("End"))
        isConnected = false;
    }

    private void Update()
    {

        if (isConnected)
        {
            endAnchorRB.isKinematic = true;
            endAnchor.transform.position = plugPosition.position;
            Vector3 eulerRotation = new Vector3(transform.rotation.x+90, transform.rotation.y, transform.rotation.z);
            //endAnchor.transform.rotation = Quaternion.Euler(eulerRotation);
            endAnchor.transform.rotation = Quaternion.Euler(eulerRotation);

        }
    }
    
    //private void OnTriggerEnter(Collider other)
    //{
    //    Debug.Log(other.name);
    //    if (other.gameObject == endAnchor.gameObject)
    //    {
    //        isConected = true;
    //        endAnchorRB.isKinematic = true;
    //        endAnchor.transform.position = plugPosition.position;
    //        endAnchor.transform.rotation = transform.rotation;


    //        OnPlugged();
    //    }
    //}

    //private void Update()
    //{

    //    if (isConected)
    //    {
    //        endAnchorRB.isKinematic = true;
    //        endAnchor.transform.position = plugPosition.position;
    //        Vector3 eulerRotation = new Vector3(this.transform.eulerAngles.x + 90, this.transform.eulerAngles.y, this.transform.eulerAngles.z);
    //        endAnchor.transform.rotation = Quaternion.Euler(eulerRotation);
    //    }
    //}
}
