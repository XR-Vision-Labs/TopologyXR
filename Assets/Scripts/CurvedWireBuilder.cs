using IndieMarc.CurvedLine;
using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class CurvedWireBuilder : MonoBehaviour
{

     
    public GameObject curvedLine;

    public Transform segment;
    public Transform spawnPoint;

    public float distance=1f;
    public float offset = .25f;

    public Transform eStartPoint;
    public Transform eEndPoint;
    public Transform sStartPoint;
    public Transform sEndPoint;
    public Transform pStartPoint;
    public Transform pEndPoint;

    public Material ethernetColor;
    public Material sftColor;
    public Material powerColor;

    public Transform startAnchor;

    private CurvedLine3D controller;

    private SegmentBehaviour segmentBehaviour;

    private void Start()
    {
        segmentBehaviour = GetComponent<SegmentBehaviour>();
    }

    public void BuildEthernet()
    {
        BuildWire(eStartPoint, eStartPoint, ethernetColor);
    }

    public void BuildSfp()
    {
        BuildWire(sStartPoint, sEndPoint, sftColor);
    }

    public void BuildPower()
    {
        BuildWire(pStartPoint, pEndPoint, powerColor);
    }

    private void BuildWire(Transform startPoint, Transform endPoint, Material color)
    {
        List<Transform> points= new List<Transform>();
        SampleController.Instance.Log("Wire building function called");

        GameObject wireBuilder = Instantiate(curvedLine, spawnPoint.position, Quaternion.identity);
        //GameObject wireBuilder = PhotonNetwork.Instantiate(wireBuilderPrefab.name, Vector3.zero, Quaternion.identity);
        //DCSceneManager.Instance.AddToList(wireBuilder);

        controller = wireBuilder.GetComponent<CurvedLine3D>();

        if (controller == null)
        {
            Debug.Log("CustomWireController not found");
            return;
        }

        Debug.Log("Instantiate wire builder");
        startAnchor = Instantiate(startPoint, spawnPoint.position, Quaternion.identity, controller.transform);

        points.Add(startAnchor);

        Debug.Log("Added start anchor");

        float distanceCovered = 0;
        Vector3 lastPos = spawnPoint.position;
        Vector3 segmentPos;
        int segmentCount = 1;

        while (distanceCovered < distance)
        {
            segmentPos = lastPos;
            
            var add = offset * segmentCount++;
            segmentPos.y -= add;

            Debug.Log("Making segment at " + offset * segmentCount);
            //segmentPos.x -= offset * ++segmentCount;
            distanceCovered += offset;
            Debug.Log($"Distance covered {distanceCovered}");

            Debug.Log($"Adding segment at {segmentPos}");
            

            Debug.Log($"Added wire segment at {segmentPos}");
            Debug.Log(distance - offset);

            if (distanceCovered < distance - offset)
            {
                points.Add(Instantiate(segment, segmentPos, Quaternion.identity, controller.transform));
                continue;
            }
            else if (distanceCovered == distance)
            {
                Debug.Log("Added end");
                //segmentPos.y -= offset * segmentCount;
                points.Add(Instantiate(endPoint, segmentPos, Quaternion.identity, controller.transform));
            }
        }


        //Debug.Log("Connecting configurable joints");




        points[1].GetComponent<ConfigurableJoint>().connectedBody = points[0].GetComponent<Rigidbody>();

        points[2].GetComponent<ConfigurableJoint>().connectedBody= points[points.Count-1].GetComponent<Rigidbody>();
        points[0].GetComponent<ConfigurableJoint>().connectedBody = points[1].GetComponent<Rigidbody>();

        points[points.Count - 1].GetComponent<ConfigurableJoint>().connectedBody = points[points.Count - 2].GetComponent<Rigidbody>();

        //Debug.Log($"Total segements {segmentCount}");
        controller.material = color;


        controller.paths = points.ToArray();
        controller.Build();
        Debug.Log("Sended the list to curvedline3d");

        controller.NewMesh();


    }

    //private void ListSend()
    //{
        

    //    controller.paths = points.ToArray();
    //    controller.Build();
    //    Debug.Log("Sended the list to curvedline3d");

    //    controller.NewMesh();
    //}
}
