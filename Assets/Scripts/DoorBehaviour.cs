using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehaviour : MonoBehaviour
{
    public DoorAnimation doorAnimation;
    public DoorAnimation doorAnimation2;
    public int doorCloseTime = 3;

    public GameObject cubeRef;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger with " + other.name);
        if (other.CompareTag("player"))
        {
            cubeRef.SetActive(false);

            Debug.Log("Trigger happen with player");
            StartCoroutine(doorAnimation.ToggleOpen());
            StartCoroutine(doorAnimation2.ToggleOpen());

            StartCoroutine(AutomaticOFF());
        }
    }

    private IEnumerator AutomaticOFF()
    {
        yield return new WaitForSecondsRealtime(doorCloseTime);

        cubeRef.SetActive(true);
        StartCoroutine(doorAnimation.ToggleOpen());
        StartCoroutine(doorAnimation2.ToggleOpen());
    }
}
