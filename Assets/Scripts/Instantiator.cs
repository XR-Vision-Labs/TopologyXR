//using PhotonPun = Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//using PhotonRealtime = Photon.Realtime;
using UnityEngine.UI;
using GLTFast;
using static TransformMenu;

public class Instantiator : MonoBehaviour
{

    
    public GameObject twoUprefab;
    public GameObject switchPrefab;
    public GameObject pduPrefab;
    public GameObject radioPrefab;
    public GameObject qualsarPrefab;
    public GameObject n310Prefab;
    public GameObject x410Prefab;
    public GameObject fibrolanPrefab;

    public Transform spawnPoint;

       

    public void InstantiateTwoU()
    {
        InstantiateSpecified(twoUprefab);

    }

    public void InstantiateRadio()
    {
        InstantiateSpecified(radioPrefab);

    }

    public void InstantiateFibrolan()
    {
        InstantiateSpecified(fibrolanPrefab);

    }

    public void InstantiateQualsar()
    {
        InstantiateSpecified(qualsarPrefab);

    }

    public void Instantiate310()
    {
        InstantiateSpecified(n310Prefab);

    }

    public void Instantiate410()
    {
        InstantiateSpecified(x410Prefab);

    }

    public void InstantiateSwitch()
    {
        InstantiateSpecified(switchPrefab);
    }

    public void InstantiatePdu()
    {
        InstantiateSpecified(pduPrefab);
    }

    private void InstantiateSpecified(GameObject prefab)
    {
        var networkServer = Instantiate(prefab, spawnPoint.position, Quaternion.identity);
        networkServer.transform.eulerAngles = new Vector3(0, spawnPoint.rotation.eulerAngles.y, 0);
        
        DCSceneManager.Instance.AddToList(networkServer);
        //ApplyTransform(networkServer);
    }

    private void ApplyTransform(GameObject obj)
    {
        States currentState = TransformMenu.CurrentState;
        SampleController.Instance.Log(typeof(Instantiator) + ": Current transform state is " + currentState);
        ServerGrabManager serverTransform = obj.GetComponent<ServerGrabManager>();


        if (serverTransform != null )
        {
            switch (currentState)
            {
                case States.Rack:
                    serverTransform.ApplyRackTransform();
                    
                    break;

                case States.Server:
                    serverTransform.ApplyServerTransform();

                    break;

                case States.Wire:
                    serverTransform.ApplyWireTransform();

                    break;
            }
        }
    }
    



}
