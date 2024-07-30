using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireCubeBehaviour : MonoBehaviour
{
    public bool isGrabbed = false;
    public string cubeTag = "wirecube";

    public WireController wireController;
    private GameObject neighbourSeg;

    private Collider triggeredPoint;

    public ConfigurableJoint[] joints = new ConfigurableJoint[2];
    public Rigidbody[] backup = new Rigidbody[2];

    private void OnTriggerEnter(Collider other)
    {
        SampleController.Instance.Log("Trigger enter with " + other.tag);

        if (other.CompareTag(cubeTag) && isGrabbed)
        {
            triggeredPoint = other;

            wireController = other.GetComponentInParent<WireController>();
            if (wireController != null)
            {
                SampleController.Instance.Log("Got the wirecontroller");
            }

            SampleController.Instance.Log("Trigger enter with wire cube");

            //neighbourSeg = wireController.GetSegmentJoint(other.gameObject);

            if (neighbourSeg == null)
            {
                SampleController.Instance.Log(typeof(WireCubeBehaviour) + " : No neighbour segment");
                return;
            }
            else
            {

                joints[0] = GetCJ(other.gameObject);
                joints[1] = GetCJ(neighbourSeg);

                backup[0] = other.GetComponent<ConfigurableJoint>().connectedBody;
                backup[1] = neighbourSeg.GetComponent<ConfigurableJoint>().connectedBody;
            }
            
            if (joints.Length > 0)
            {
                Rigidbody rb = GetComponent<Rigidbody>();
                joints[0].connectedBody = rb;
                joints[1].connectedBody = rb;

                //wireController.ChangeKinematic(false);

                SampleController.Instance.Log("Cube rigidbody connected to both the segments");
            }
            
        }
    }



    private ConfigurableJoint GetCJ(GameObject go)
    {
        ConfigurableJoint[] cj = go.GetComponents<ConfigurableJoint>();
        if (cj.Length <= 0)
        {
            SampleController.Instance.Log("No configuraable joint found");
            return null;
        }

        else
        {
            return cj[1];
        }
    }

    //private void OnTriggerStay(Collider other)
    //{
    //    SampleController.Instance.Log("Trigger stay with " + other.tag);

    //    if (other.CompareTag(cubeTag))
    //    {
    //        SampleController.Instance.Log("Trigger stay with wire cube");

    //        // get the above configurable object or below

    //        // change connected body of both segments to this rigidbody
    //    }
    //}

    private void ReleaseWire()
    {
        //SampleController.Instance.Log("Trigger exit with " + other.tag);

        if (triggeredPoint)
        {
            SampleController.Instance.Log("Trigger exit with wire cube");

            // swap the rigidbodies

            if (joints.Length > 0)
            {
                Destroy(joints[0]);

                Destroy(joints[1]);

                //wireController.ChangeKinematic(true);
                SampleController.Instance.Log("Trigger exit so swap the rigidbody");
            }
        }
    }

    //private void OnTriggerExit(Collider other)
    //{
    //    SampleController.Instance.Log("Trigger exit with " + other.tag);

    //    if (other.CompareTag(cubeTag) && !isGrabbed)
    //    {
    //        SampleController.Instance.Log("Trigger exit with wire cube");

    //        // swap the rigidbodies

    //        if (joints.Length > 0)
    //        {
    //            joints[0].connectedBody = backup[0];
    //            joints[1].connectedBody = backup[1];



    //            wireController.ChangeKinematic(true);
    //            SampleController.Instance.Log("Trigger exit so swap the rigidbody");
    //        }
    //    }
    //}

    private void Update()
    {
        if(OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger) || OVRInput.GetDown(OVRInput.Button.SecondaryHandTrigger))
        {
            isGrabbed = true;
        }

        if(OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger) || OVRInput.GetUp(OVRInput.Button.SecondaryHandTrigger))
        {
            isGrabbed = false;

            ReleaseWire();
        }

#if UNITY_EDITOR
        if (!isGrabbed)
        {
            ReleaseWire();
        }

#endif
    }
}
