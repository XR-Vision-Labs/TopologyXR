using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandMenuManager : MonoBehaviour
{
    public GameObject rackMenu;
    public GameObject serverMenu;
    public GameObject wireMenu;
    public GameObject otherMenu;
    public GameObject debug;

    public ButtonBehaviour rackBtn;
    public ButtonBehaviour serverBtn;
    public ButtonBehaviour wireBtn;
    public ButtonBehaviour otherBtn;
    public ButtonBehaviour debugBtn;

    public Transform leftHand;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("Rack button pressed");
            OnRackButtonPressed();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("Server button pressed");
            OnServerButtonPressed();
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("Wire button pressed");
            OnWireButtonPressed();
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            Debug.Log("Other button pressed");
            OnOtherButtonPressed();
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            Debug.Log("Other button pressed");
            OnOtherButtonPressed();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log("Other button pressed");
            OnDebugButtonPressed();
        }



    }

    
    public void OnRackButtonPressed()
    {
        rackMenu.SetActive(true);
        serverMenu.SetActive(false);
        wireMenu.SetActive(false);
        otherMenu.SetActive(false);
        debug.SetActive(false);

        rackBtn.SetColor(true);
        serverBtn.SetColor(false);
        wireBtn.SetColor(false);
        otherBtn.SetColor(false);
        debugBtn.SetColor(false);
    }

    public void OnTopologyButtonPressed()
    {
        rackMenu.SetActive(false);
        serverMenu.SetActive(false);
        wireMenu.SetActive(false);
        otherMenu.SetActive(false);
        debug.SetActive(false);
    }

    public void OnServerButtonPressed()
    {
        rackMenu.SetActive(false);
        serverMenu.SetActive(true);
        wireMenu.SetActive(false);
        otherMenu.SetActive(false);
        debug.SetActive(false);

        rackBtn.SetColor(false);
        serverBtn.SetColor(true);
        wireBtn.SetColor(false);
        otherBtn.SetColor(false);
        debugBtn.SetColor(false);
    }

    public void OnWireButtonPressed()
    {
        rackMenu.SetActive(false);
        serverMenu.SetActive(false);
        wireMenu.SetActive(true);
        otherMenu.SetActive(false);
        debug.SetActive(false);

        rackBtn.SetColor(false);
        serverBtn.SetColor(false);
        wireBtn.SetColor(true);
        otherBtn.SetColor(false);
        debugBtn.SetColor(false);
    }

    public void OnOtherButtonPressed()
    {
        rackMenu.SetActive(false);
        serverMenu.SetActive(false);
        wireMenu.SetActive(false);
        otherMenu.SetActive(true);
        debug.SetActive(false);

        rackBtn.SetColor(false);
        serverBtn.SetColor(false);
        wireBtn.SetColor(false);
        otherBtn.SetColor(true);
        debugBtn.SetColor(false);
    }

    public void OnDebugButtonPressed()
    {
        rackMenu.SetActive(false);
        serverMenu.SetActive(false);
        wireMenu.SetActive(false);
        otherMenu.SetActive(false);
        debug.SetActive(true);

        rackBtn.SetColor(false);
        serverBtn.SetColor(false);
        wireBtn.SetColor(false);
        otherBtn.SetColor(false);
        debugBtn.SetColor(true);
    }

}
