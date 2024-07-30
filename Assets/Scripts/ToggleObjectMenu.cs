using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ToggleObjectMenu : MonoBehaviour
{
    public GameObject objectMenu;

    public UnityEvent disableMove;

    public void ActivateMenu()
    {
        objectMenu.SetActive(true);

        
    }

    public void DeactivateMenu()
    {
        objectMenu.SetActive(false);

        disableMove.Invoke();
    }

}
