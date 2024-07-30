
using UnityEngine;

public class AutoWiring : MonoBehaviour
{
    private WireController wireBuilder;

    public Transform startAnchor;
    public Transform endAnchor;
    public Color color;

    public float offset = 0.5f;
    private float distance = 1;

    private void Start()
    {
        wireBuilder = GetComponent<WireController>();
        if(wireBuilder == null )
        Debug.Log("Didnt Get the wirebuilder");

        BuildWire();
    }



    private void BuildWire()
    {

        SampleController.Instance.Log("Wire building function called");

        

        TubeRenderer renderer = wireBuilder.GetComponentInChildren<TubeRenderer>();

        if (renderer != null)
            renderer.material.color = color;

        WireController controller = wireBuilder.GetComponent<WireController>();

        if (controller == null)
        {
            Debug.Log("CustomWireController not found");
            return;
        }

        Debug.Log("Instantiate wire builder");

        float distanceCovered = 0;
        Vector3 lastPos = startAnchor.position;

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
            else if (distanceCovered == distance)
            {
                Debug.Log("Added end");
                controller.AddEnd(endAnchor);
            }
        }

        SampleController.Instance.Log("Stream segment function called");

        Debug.Log($"Total segements {segmentCount}");
    }
}
