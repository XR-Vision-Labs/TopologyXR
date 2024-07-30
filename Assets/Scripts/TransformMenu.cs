using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TransformMenu : MonoBehaviour
{
    public static event Action RackTransform;
    public static event Action ServerTransform;
    public static event Action WireTransform;

    public ButtonBehaviour rackTransformBn;
    public ButtonBehaviour serverTransformBn;
    public ButtonBehaviour wireTransformBn;

    public enum States { Rack, Server, Wire};

    private static States currentState=States.Rack;

    public static States CurrentState
    {
        get
        {
            return currentState;
        }
        set
        {
            currentState = value;
        }
    }

    private void Start()
    {

        //rackTransformBn.onClick.AddListener(OnRackTransformButton);
        //serverTransformBn.onClick.AddListener(OnServerTransformButton);
        //wireTransformBn.onClick.AddListener(OnWireTransformButton);

        //OnRackTransformButton();
    }

    //private void OnDestroy()
    //{
    //    rackTransformBn.onClick.RemoveListener(OnRackTransformButton);
    //    serverTransformBn.onClick.RemoveListener(OnServerTransformButton);
    //    wireTransformBn.onClick.RemoveListener(OnWireTransformButton);
    //}

    //private void Update()
    //{
        
    //}

    public void OnRackTransformButton()
    {
        SampleController.Instance.Log("Rack transform button pressed");

        RackTransform.Invoke();
        CurrentState = States.Rack;
        OnStateChange();
    }

    public void OnServerTransformButton()
    {
        SampleController.Instance.Log("Server transform button pressed");

        ServerTransform.Invoke();
        CurrentState = States.Server;
        OnStateChange();
    }

    public void OnWireTransformButton()
    {
        SampleController.Instance.Log("Wire transform button pressed");

        WireTransform.Invoke();
        CurrentState = States.Wire;
        OnStateChange();
    }

    public void OnStateChange()
    {
        SampleController.Instance.Log("State change");

        switch (CurrentState)
        {
            case States.Rack:
                //serverTransformBn.interactable = true;
                //rackTransformBn.interactable = false;
                //wireTransformBn.interactable = true;
                rackTransformBn.SetColor(true);
                serverTransformBn.SetColor(false);
                wireTransformBn.SetColor(false);
                


                break;

            case States.Server:
                //serverTransformBn.interactable = false;
                //rackTransformBn.interactable = true;
                //wireTransformBn.interactable = true;

                rackTransformBn.SetColor(false);
                serverTransformBn.SetColor(true);
                wireTransformBn.SetColor(false);

                break;

            case States.Wire:
                //wireTransformBn.interactable = false;
                //rackTransformBn.interactable = true;
                //serverTransformBn.interactable = true;

                rackTransformBn.SetColor(false);
                serverTransformBn.SetColor(false);
                wireTransformBn.SetColor(true);

                break;
        }
    }
}
