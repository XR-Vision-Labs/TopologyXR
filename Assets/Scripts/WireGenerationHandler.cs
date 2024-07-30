using UnityEngine;
using System;
using UnityEngine.UI;

public class WireGenerationHandler : MonoBehaviour
{
    [SerializeField]
    public Transform ethernetStartPrefab;
    public Transform ethernetEndPrefab;
    public Transform powerStartPrefab;
    public Transform powerEndPrefab;

    [SerializeField]
    public Transform sfpStartPrefab;
    public Transform sfpEndPrefab;


    public GameObject wireBuilderPrefab;

    public float offset = 0.5f;
    public Transform rightHandAnchor;

    private Button ethernetButton;

    public void BuildSFPWire(float distance)
    {
        BuildWire(sfpStartPrefab,sfpEndPrefab, distance, Color.black);
    }


    public void BuildEthernetWire(float distance)
    {
        BuildWire(ethernetStartPrefab,ethernetEndPrefab, distance, Color.blue);
    }

    public void BuildPowerWire(float distance)
    {
        BuildWire(powerStartPrefab, powerEndPrefab, distance, Color.black);
    }

    //public void BuildSnapWire(float distance)
    //{
    //    BuildWire(compoundStartWire, compoundEndWire, distance, Color.blue);
    //}

    private void BuildWire(Transform sPrefab, Transform ePrefab, float distance, Color wireColor)
    {

        SampleController.Instance.Log("Wire building function called");

        GameObject wireBuilder = Instantiate(wireBuilderPrefab);
        //GameObject wireBuilder = PhotonNetwork.Instantiate(wireBuilderPrefab.name, Vector3.zero, Quaternion.identity);
        DCSceneManager.Instance.AddToList(wireBuilder);

        TubeRenderer renderer = wireBuilder.GetComponentInChildren<TubeRenderer>();

        if(renderer != null)
        renderer.material.color= wireColor;

        WireController controller = wireBuilder.GetComponent<WireController>();

        if(controller == null)
        {
            Debug.Log("CustomWireController not found");
            return;
        }

        Debug.Log("Instantiate wire builder");

        controller.AddStar(rightHandAnchor.position, sPrefab);
        Debug.Log("Added start anchor");

        float distanceCovered = 0;
        Vector3 lastPos = rightHandAnchor.position;

        int segmentCount = 0;

        while (distanceCovered < distance)
        {
            var segmentPos = lastPos;
            segmentPos.y -= offset * ++segmentCount;
            distanceCovered += offset;
            Debug.Log($"Distance covered {distanceCovered}");

            Debug.Log($"Adding segment at {segmentPos}");
            controller.AddSegment(segmentPos);

            Debug.Log($"Added wire segment at {segmentPos}");
            Debug.Log(distance - offset);

            if (distanceCovered < distance - offset)
            {

                continue;
            }
            else if(distanceCovered == distance)
            {
                Debug.Log("Added end");
                controller.AddEnd(ePrefab);
            }
        }

        SampleController.Instance.Log("Stream segment function called");

        Debug.Log($"Total segements {segmentCount}");
    }
}
