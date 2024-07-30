using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SegmentManager : MonoBehaviour
{
    public static SegmentManager instance;

    private SegmentBehaviour prevBuilder=null;

    private void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void PlugGrabbed(SegmentBehaviour currentBuilder)
    {
        SampleController.Instance.Log("Inside plug grab function");
        if (currentBuilder == prevBuilder)
        {

            SampleController.Instance.Log("current is equal to prev");
            return;
        }
        else
        {
            SampleController.Instance.Log("current is not equal to prev");
            if(prevBuilder!=null)
            {
                SampleController.Instance.Log("prev builder is not null so calling disable function");
                prevBuilder.DisableGrab();
            }
            else
            {
                SampleController.Instance.Log("prev builder is null");
            }

            if (currentBuilder != null)
            {
                currentBuilder.GetComponent<SegmentBehaviour>().EnableGrab();
                SampleController.Instance.Log("Current builder enablegrab is called");
            }

            SampleController.Instance.Log("giving to prev builder");
            prevBuilder = currentBuilder;
            SampleController.Instance.Log("Enable grab and ungrab");
        }
    }
}
