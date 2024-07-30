using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomRayBehaviour : MonoBehaviour
{
    public GameObject modalRay;
    public GameObject rayInteractor;
    private bool isActivate = false;

    public static CustomRayBehaviour Instance;

    private void Start()
    {
        if(Instance == null)
        {
            Instance = this;

        }
        else
        {
            Destroy(Instance);
        }
    }

    private void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.Three))
        {
            SampleController.Instance.Log("Activating ray");
            ToggleRay();
        }

        //if (Input.GetKeyDown(KeyCode.T))
        //{
        //    SampleController.Instance.Log("Activating ray");
        //    ToggleRay();
        //}
    }

    public void ToggleRay()
    {
        if(!isActivate)
        {
            isActivate = true;
            ActivateRay();
        }
        else
        {
            DeactivateRay();
            isActivate =false;
        }
    }

    public void ActivateRay()
    {
        modalRay.SetActive(true);
        rayInteractor.SetActive(false);
    }

    public void DeactivateRay()
    {
        modalRay.SetActive(false);
        rayInteractor.SetActive(true);
    }


}
