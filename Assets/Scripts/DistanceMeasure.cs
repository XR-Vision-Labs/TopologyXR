using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DistanceMeasure : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public TextMeshPro text;

    private Vector3 prevPos;

    private void OnEnable()
    {
        prevPos = transform.position;
        Debug.Log("position is " + prevPos);
        FindDistance();
    }

    void Update()
    {
        if(transform.position != prevPos)
        {
            Debug.Log("Position is not equal so finding distance");
            FindDistance();
            prevPos=transform.position; 
        }
    }

    void FindDistance()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            if (hit.collider.CompareTag("wall"))
            {
                float distance = Vector3.Distance(transform.position, hit.point);
                lineRenderer.SetPosition(0, transform.position);
                lineRenderer.SetPosition(1, hit.point);
                Debug.Log("Distance to wall: " + distance);
                text.text = distance + " m";
            }
        }
    }
}
