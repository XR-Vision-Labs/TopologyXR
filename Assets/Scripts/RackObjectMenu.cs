using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RackObjectMenu : MonoBehaviour
{
    public RackTransform rackTransform;
    [SerializeField]private GameObject[] measureObj;
    private bool isActivated=false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            Debug.Log("Toggle menu pressed");
            ToggleMove();
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            DeleteModel();
        }
    }

    public void ToggleMove()
    {
        SampleController.Instance.Log("Toggle menu button pressed");

        if (isActivated)
        {
            DisableMove();
            isActivated = false;

            CustomRayBehaviour.Instance.ActivateRay();
        }
        else
        {
            EnableMove();
            isActivated = true;

        }
    }

    public void EnableMove()
    {
        rackTransform.EnableRackTransform();
    }
    
    public void DisableMove()
    {
        rackTransform.DisableRackTransform();
    }

    public void DeleteModel()
    {
        CustomRayBehaviour.Instance.ActivateRay();
        Destroy(rackTransform.gameObject);
    }

    public void ToggleMeasure()
    {
        foreach(GameObject obj in measureObj)
        {
            obj.SetActive(!obj.activeSelf);
            
        }
    }
}
