using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiModelHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        OVRInput.EnableSimultaneousHandsAndControllers();
        SampleController.Instance.Log("Enable multi hand from script");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
