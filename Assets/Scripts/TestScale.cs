using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScale : MonoBehaviour
{
    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha5))
        {
            Debug.Log("5 button is pressed");

            Vector3 localScale = transform.localScale;
            Debug.Log(localScale);

            Vector3 scale = new Vector3(localScale.x, localScale.y, localScale.z * 3);

            Debug.Log(scale);

            transform.localScale = scale;
        }
    }
}
