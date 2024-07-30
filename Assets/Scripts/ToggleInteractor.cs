using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ToggleInteractor : MonoBehaviour
{
    public UnityEvent onPressOne;
    public UnityEvent onPressTwo;
    

    private void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.One))
        {
            onPressOne.Invoke();

        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            onPressOne.Invoke();
        }
        
        if (OVRInput.GetDown(OVRInput.Button.Two))
        {
            onPressTwo.Invoke();

        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            onPressTwo.Invoke();
        }
    }

    
}
