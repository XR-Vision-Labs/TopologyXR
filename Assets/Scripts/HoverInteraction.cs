using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverInteraction : MonoBehaviour
{
    public SnapInteractable interactable;
    public SnapInteractor interactor;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            Debug.Log("Hover interactable added");
            interactable.AddInteractor(interactor);
        }
    }
}
