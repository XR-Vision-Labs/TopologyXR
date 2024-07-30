using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAnimation : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public IEnumerator ToggleOpen()
    {
        yield return null;
            bool val = animator.GetBool("isOpen");
            animator.SetBool("isOpen", !val);
        
    }

    
}
