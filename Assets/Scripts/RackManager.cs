using UnityEngine;


public class RackManager : MonoBehaviour
{
    public GameObject grandParent;
    public GameObject parent;
    public GameObject child;
    public GameObject snapInteractables;
    public GameObject snapInteractors;


    private void Update()
    {

        
        if (Input.GetKeyDown(KeyCode.E))
        {
            Select();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Deselect();
        }
       

    }

    public void ApplyRackTransform()
    {
        EnableRack();
    }

    public void ApplyWireTransform()
    {
        DisableRack();
    }

    public void ApplyServerTransform()
    {
        DisableRack();
    }


    public void Select()
    {
        Debug.Log("selecting");
        SampleController.Instance.Log("Gravity off and kinematic on");
        parent.GetComponent<Rigidbody>().useGravity = false;
        parent.GetComponent<Rigidbody>().isKinematic =true;
    }

    public void Deselect()
    {
        Debug.Log("deselecting");
        SampleController.Instance.Log("Gravity off and kinematic on");
        parent.GetComponent<Rigidbody>().useGravity = true;
        parent.GetComponent<Rigidbody>().isKinematic = false;
    }

    private void OnEnable()
    {

        TransformMenu.RackTransform += EnableRack;

        TransformMenu.WireTransform += DisableRack;

        TransformMenu.ServerTransform += DisableRack;

    }

    private void OnDisable()
    {

        TransformMenu.RackTransform -= EnableRack;

        TransformMenu.WireTransform -= DisableRack;

        TransformMenu.ServerTransform -= DisableRack;
    }

    private void EnableRack()
    {
        parent.transform.parent = null;
        grandParent.transform.parent = parent.transform;
        grandParent.GetComponent<BoxCollider>().enabled = false;

        parent.SetActive(true);
        child.transform.parent = parent.transform;

        SampleController.Instance.Log($"{typeof(RackManager)}: Enabling Rack");
    }

    private void DisableRack()
    {
        grandParent.transform.parent = null;
        parent.transform.parent = grandParent.transform;
        grandParent.GetComponent<BoxCollider>().enabled = true;

        child.transform.parent = parent.transform.parent;
        parent.SetActive(false);
        

        SampleController.Instance.Log($"{typeof(RackManager)}: Disabling Rack");
        //HandleGrab(true, true);
    }

 
}
