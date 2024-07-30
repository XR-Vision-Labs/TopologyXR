using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelRay : MonoBehaviour
{
    public Transform cursor;

    private GameObject prevHitPoint;
    private GameObject currentHitPoint;

    private Transform prevHitParent;
    private Transform currentHitParent;

    public Color hoverColor;
    public Color selectColor;
    private Renderer render;
    private int _shaderInnerColor = Shader.PropertyToID("_Color");

    private void Start()
    {
        render= cursor.GetComponent<Renderer>();
        if(render != null)
        {
            Debug.Log("Renderer not found");
        }
    }

    private void Update()
    {
        RaycastHit hit;

        //Debug.Log("Casting the ray");

        // Cast a ray forward from the GameObject's position
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            //Debug.Log("Ray hit");

            currentHitPoint = hit.collider.gameObject;
            cursor.position = hit.point;
            cursor.rotation= Quaternion.LookRotation(hit.normal, Vector3.up);
            render.material.SetColor(_shaderInnerColor, hoverColor);
        }

        if(OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
        {
            SampleController.Instance.Log("Trigger pressed");
            OnTriggerPress();
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log("Trigger pressed");
            OnTriggerPress();
        }
    }

    private void OnTriggerPress()
    {
        Debug.Log("Trigger pressed doing work");
        SampleController.Instance.Log("Trigger pressed doing work");

        Debug.Log("Current object is "+currentHitPoint.name);
        SampleController.Instance.Log("Current object is " + currentHitPoint.name);

        render.material.SetColor(_shaderInnerColor, selectColor);
        HidePrevMenu();


        //Debug.Log("Previous object is "+prevHitPoint.name);

        if (!currentHitPoint.CompareTag("select") )
        {
            SampleController.Instance.Log("Hit is not any object of our choice but " + currentHitPoint.tag);
            Debug.Log("Hit is not any object of our choice but " + currentHitPoint.tag);
            return;
        }

        currentHitParent = currentHitPoint.transform.parent.parent;
        SampleController.Instance.Log("Current hit parent is "+currentHitParent.name);
        if (prevHitPoint)
        {
            SampleController.Instance.Log("previoous hit point is "+ prevHitPoint.name);
            prevHitParent = prevHitPoint.transform.parent.parent;
            SampleController.Instance.Log("previoous hit point is "+ prevHitParent.name);
        }
        else
        {
            SampleController.Instance.Log("Previous hit is null");
        }

        if (prevHitParent != currentHitParent)
        {
            Debug.Log("Hit different gameobject");

            CustomRayBehaviour.Instance.DeactivateRay();
            SampleController.Instance.Log("Ray deactivated and reactivated other ray");
            AfterHitProcessing();
        }
        else
        {
            SampleController.Instance.Log("hit same object " + currentHitParent + " " + prevHitParent);
        }

        prevHitPoint = currentHitPoint;
    }

    private void AfterHitProcessing()
    {
        SampleController.Instance.Log("Current parent is "+currentHitParent.name);
        Debug.Log("Current parent is "+ currentHitParent.name);

        currentHitParent.GetComponent<ToggleObjectMenu>().ActivateMenu();
        Debug.Log("Activated the object menu");
        SampleController.Instance.Log("Activated the object menu");

        if (prevHitPoint == null)
        {
            return;
        }
        
        Debug.Log("Previous hit is " + prevHitParent.name);
        SampleController.Instance.Log("Previous hit is " + prevHitParent.name);


        prevHitParent.GetComponent<ToggleObjectMenu>().DeactivateMenu();
        Debug.Log("Deactivated the object menu for the previous hit point");
        SampleController.Instance.Log("Deactivated the object menu for the previous hit point");
    }


    public void HidePrevMenu()
    {
        if (prevHitPoint == null)
        {
            SampleController.Instance.Log("Previous hit point is null so returning");
            return;
        }
        else if(prevHitParent == null)
        {
            prevHitParent= prevHitPoint.transform.parent.parent;
            SampleController.Instance.Log("Parent was null so assigned "+prevHitParent.name);
        }

        
            Debug.Log("Previous hit is " + prevHitParent.name);
            SampleController.Instance.Log("Previous hit is " + prevHitParent.name);
            prevHitParent.GetComponent<ToggleObjectMenu>().DeactivateMenu();

            Debug.Log("Deactivated the object menu for the previous hit point");
            SampleController.Instance.Log("Deactivated the object menu for the previous hit point");
        
    }
    
}
