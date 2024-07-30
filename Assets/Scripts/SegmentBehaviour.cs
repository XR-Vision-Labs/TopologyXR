using Oculus.Interaction;
using Oculus.Interaction.HandGrab;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SegmentBehaviour : MonoBehaviour
{
    public List<GameObject> segments;
    private const string str = "segment";



    private void Start()
    {
        segments = new List<GameObject>();

        GetSegments();
    }

    public void LocalPlugGrab()
    {
        if(SegmentManager.instance != null)
        {
            SampleController.Instance.Log("Segmentmanager found now calling grab");

            SegmentManager.instance.PlugGrabbed(this);
        }
    }

    private void GetSegments()
    {
        for(int i = 0; i < transform.childCount; i++) 
        { 
            GameObject child= transform.GetChild(i).gameObject;

            if(child.tag == str)
            {
                Debug.Log("Tag equal so adding to list");

                segments.Add(child);
            }   
        }
    }

    public void EnableGrab()
    {
        PerformGrabOP(true);
    }
    
    public void DisableGrab()
    {
        PerformGrabOP(false);
    }

    private void PerformGrabOP(bool val)
    {
        foreach(GameObject child in segments)
        {
            GrabInteractable grabInteractable = child.GetComponent<GrabInteractable>();
            HandGrabInteractable handGrabInteractable=child.GetComponent<HandGrabInteractable>();

            grabInteractable.enabled = val;
            handGrabInteractable.enabled = val;

            child.GetComponent<MeshRenderer>().enabled = val;
        }
    }
}
