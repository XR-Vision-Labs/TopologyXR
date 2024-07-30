using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerObjectMenu : MonoBehaviour
{
    public ServerGrabManager grabManager;
    private bool isActivated = false;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Move button pressed for server");

            ToggleMove();
        }
    }

    private void Start()
    {
        grabManager= GetComponentInParent<ServerGrabManager>();
        if(grabManager == null)
        {
            Debug.Log("Didnt get grabmanager");
        }
    }

    public void ToggleMove()
    {
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
        grabManager.EnableServerTransform();
    }

    public void DisableMove()
    {
        grabManager.DisableServerTransform();
    }

    public void DeleteModel()
    {
        Destroy(grabManager.gameObject);
    }
}
