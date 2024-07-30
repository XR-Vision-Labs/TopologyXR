using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerHandler : MonoBehaviour
{
    private IEnumerator OnTriggerEnter(Collider other)
    {
        Debug.Log("Collide with " + other.name + " tag is " + other.tag);

        if (other.CompareTag("floor"))
        {
            SampleController.Instance.Log("trigger with floor");
            yield return new WaitForEndOfFrame();
            Rigidbody rb = gameObject.GetComponent<Rigidbody>();
            rb.constraints = RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX;
            //Debug.Log(RigidbodyConstraints.FreezeRotationZ);
            //rb.constraints = RigidbodyConstraints.FreezeRotationX;

            //rb.constraints = RigidbodyConstraints.FreezePositionY;

            GetComponent<BoxCollider>().isTrigger = false;
        }
    }
}
