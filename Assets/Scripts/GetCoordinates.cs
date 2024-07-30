using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetCoordinates : MonoBehaviour
{
    private bool isAdded = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("floor") && !isAdded)
            StartCoroutine(OnTrigger(other));
    }

    private IEnumerator OnTrigger(Collider other)
    {
        yield return null;

        SampleController.Instance.Log("Trigger with floor at point " + transform.position);
        isAdded = true;

        if (other != null)
        {
            SampleController.Instance.Log("Calling add to list function");
            other.GetComponent<FloorCoordinatesManager>().AddToList(transform.position);
        }
    }
}
