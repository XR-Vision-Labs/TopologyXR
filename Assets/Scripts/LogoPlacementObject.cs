using Oculus.Interaction.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogoPlacementObject : MonoBehaviour
{
    public SampleController SampleController { get;  set; }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0)) 
        Save();

        //if(OVRInput.GetDown(OVRInput.Button.Two))
        //    Save();

    }

    public void Save()
    {
        if(SampleController != null)
        {
            SampleController.PlaceAnchorAtRoot(transform);
        }
    }


    public void Share()
    {
        if (SampleController != null)
        {
            SampleController.ShareAnchor();
        }
    }
    
    public void Align()
    {
        if (SampleController != null)
        {
            SampleController.AlignWithAnchor();
        }
    }


    
}
