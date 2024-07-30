using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SegmentPlugBehaviour : MonoBehaviour
{
    private Transform parent;

    void Start()
    {
        parent = transform.parent;
    }


    public void PlugGrab()
    {
        if (parent != null)
        {

            SampleController.Instance.Log("Calling plug grab on parent");
            parent.GetComponent<SegmentBehaviour>().LocalPlugGrab();
        }
    }
}
