using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorInputHandler : MonoBehaviour
{
    public FlickerLight flickerLight;
    public WireSnapStay wireSnapStay;
    public ServerAudioManager serverAudioManager;
    public ConnectionManager connectionManager;
    public TransformMenu transformMenu;
    public Instantiator serverInstantiator;
    public CurvedWireBuilder wireGenerationHandler;

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.A))
        {
            flickerLight.StopFlickering();
            Debug.Log("Lights stop flickering");
        }


        if (Input.GetKeyDown(KeyCode.B))
        {
            Debug.Log("Server transform button pressed");
            transformMenu.OnServerTransformButton();
        }


        // handling lights flickering
        if (Input.GetKeyDown(KeyCode.F))
        {
            flickerLight.StartFlickering();
        }

        // handling wiresnap input
        if (Input.GetKeyDown(KeyCode.Z))
        {
            wireSnapStay.PerformSnap();
            Debug.Log("Performed the snap");
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            wireSnapStay.PerformUnSnap();
            Debug.Log("Performed the unsnap");
        }

        // handling server audio input
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            serverAudioManager.PlayStart();
        }

        //if (Input.GetKeyDown(KeyCode.R))
        //{
        //    Debug.Log("Rack transform button pressed");
        //    transformMenu.OnRackTransformButton();
        //}



        if (Input.GetKeyDown(KeyCode.X))
        {
            Debug.Log("Wire transform button pressed");
            transformMenu.OnWireTransformButton();
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            serverInstantiator.InstantiateSwitch();
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            serverInstantiator.InstantiateTwoU();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            serverInstantiator.InstantiatePdu();
        }

        // handing wire generation input

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            Debug.Log("Builded ethernet wire");
            wireGenerationHandler.BuildEthernet();
        }

        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            Debug.Log("Builded sfp wire");
            wireGenerationHandler.BuildSfp();
        }

        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            Debug.Log("Building power wire");
            wireGenerationHandler.BuildPower();
        }
    }
}
