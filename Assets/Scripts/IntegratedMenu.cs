using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntegratedMenu : MonoBehaviour
{
    public float spawnDistance = 1;
    public Transform cameraTransform;

    public GameObject RackMenu;
    public GameObject ServerMenu;
    public GameObject WireMenu;
    public GameObject DebugMenu;

    public Color selectColor;
    public Color unselectColor;

    public Image rackCircle;
    public Image serverCircle;
    public Image wireCircle;
    public Image debugCircle;


    private bool HighlightRackCircle
    {
        set
        {
            rackCircle.color = value ? selectColor : unselectColor;
            
        }
    }

    private bool HighlightServerCircle
    {
        set
        {
            serverCircle.color = value ? selectColor : unselectColor;
        }
    }
    private bool HighlightWireCircle
    {
        set
        {
            wireCircle.color = value ? selectColor : unselectColor;
        }
    }

    private bool HighlightDebugCircle
    {
        set
        {
            debugCircle.color = value ? selectColor : unselectColor;
        }
    }

    #region MainMenu

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            ToggleMenu();
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            EnableRackMenu();
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            EnableServerMenu();
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            EnableWireMenu();
        }

        if (Input.GetKeyDown(KeyCode.Comma))
        {
            EnableDebugMenu();
        }

        //if (OVRInput.GetDown(OVRInput.Button.Start))
        //{
        //    ToggleMenu();
        //}
    }

    public void ToggleMenu()
    {
        gameObject.SetActive(!gameObject.activeSelf);

        RecenterMenu();
    }
  
    public void RecenterMenu()
    {
        var recenterPos = cameraTransform.position + cameraTransform.forward * spawnDistance;

        transform.position= recenterPos;
    }

    #endregion


    #region RackMenu

    public void EnableRackMenu()
    {
        RackMenu.SetActive(true);
        ServerMenu.SetActive(false);
        WireMenu.SetActive(false);
        DebugMenu.SetActive(false);

        HighlightRackCircle = true;
        HighlightDebugCircle = false;
        HighlightServerCircle = false;
        HighlightWireCircle = false;
    }

    #endregion

    #region ServerMenu
    public void EnableServerMenu()
    {
        RackMenu.SetActive(false);
        ServerMenu.SetActive(true);
        WireMenu.SetActive(false);
        DebugMenu.SetActive(false);

        HighlightRackCircle = false;
        HighlightDebugCircle = false;
        HighlightServerCircle = true;
        HighlightWireCircle = false;
    }

    #endregion

    #region WireMenu

    public void EnableWireMenu()
    {
        RackMenu.SetActive(false);
        ServerMenu.SetActive(false);
        WireMenu.SetActive(true);
        DebugMenu.SetActive(false);

        HighlightRackCircle = false;
        HighlightDebugCircle = false;
        HighlightServerCircle = false;
        HighlightWireCircle = true;
    }
    #endregion


    #region DebugMenu
    public void EnableDebugMenu()
    {
        RackMenu.SetActive(false);
        ServerMenu.SetActive(false);
        WireMenu.SetActive(false);
        DebugMenu.SetActive(true);

        HighlightRackCircle = false;
        HighlightDebugCircle = true;
        HighlightServerCircle = false;
        HighlightWireCircle = false;
    }

    #endregion
}
