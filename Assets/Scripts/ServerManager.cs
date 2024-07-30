using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerManager : MonoBehaviour
{
    public List<Transform> serversPrefab;

    public List<Transform> containers;
    public List<Transform> row1 = new List<Transform>();
    public List<Transform> row2 = new List<Transform>();
    public List<Transform> slots1;
    public List<Transform> slots2;

    public Transform initialPos;
    public Transform tempPos;
    public Transform player;

    public float timeGap = 1f;

    private bool canPopulate = false;
    public void ContainerAdded()
    {
        canPopulate = true;
    }

    // i want to get all the servers from racka and then differentiate to 
    // two lists one for each row racks
    // then we will get the input from the user to populate the racks based on those inputs
    // so we will populate rack by rack out of the row list

    private void Start()
    {
        containers = new List<Transform>();
        row1 = new List<Transform>();
        row2 = new List<Transform>();
        slots1 = new List<Transform>();
        slots2 = new List<Transform>();
    }

    private void Update()
    {
        if(canPopulate && containers.Count > 0)
        {
            Debug.Log("Getting all the racks");
            DifferentiateRows();

        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            PopulateRack();
        }

        }


    public void DifferentiateRows()
    {
        canPopulate = false;
        foreach(Transform t in containers)
        {
            row1.AddRange( t.GetComponent<ContainerBehaviour>().row_1);
            row2.AddRange(t.GetComponent<ContainerBehaviour>().row_2);
        }

        SampleController.Instance.Log("Differnetate row done");
        SampleController.Instance.Log($"Current raw1 is {row1.Count} and row2 is {row2.Count}");

        GetSnaps();
    }

    private void GetSnaps()
    {
        foreach(Transform t in row1)
        {
            slots1.AddRange(t.GetComponent<ServerSnapBehaviour>().snaps);
        }
        //SampleController.Instance.Log("Row1 snap extracted current count is " + slots1.Count);
        
        foreach(Transform t in row2)
        {
            slots2.AddRange(t.GetComponent<ServerSnapBehaviour>().snaps);
        }
        //SampleController.Instance.Log("Row1 snap extracted current count is " + slots2.Count);

    }

    public void PopulateRack()
    {
        //initialPos.position = player.position;
        StartCoroutine(PopulateRacks());
    }

    private IEnumerator PopulateRacks()
    {
        //yield return null;

        //SampleController.Instance.Log("Moving player to temp posito");
        //player.position = tempPos.position; player.rotation = tempPos.rotation;

        //yield return null;
        for (int i=1; i<slots1.Count; i++)
        {
            var obj = Instantiate(serversPrefab[0]);
            DCSceneManager.Instance.AddToList(obj.gameObject);
            SnapInteractable interactable = slots1[i].GetChild(0).GetComponent<SnapInteractable>();
            //Debug.Log("Interactable is " + interactable.name);
            SnapInteractor interactor = obj.GetComponentInChildren<SnapInteractor>();
            //Debug.Log("Interactor is " + interactor.name);
            interactor.SetSelectInteratable(interactable);
            i+=3;
            yield return null;
        }

        yield return null;
        for (int i = 1; i < slots2.Count; i++)
        {
            var obj = Instantiate(serversPrefab[0]);
            DCSceneManager.Instance.AddToList(obj.gameObject);
            SnapInteractable interactable = slots2[i].GetChild(0).GetComponent<SnapInteractable>();
            //Debug.Log("Interactable is " + interactable.name);
            SnapInteractor interactor = obj.GetComponentInChildren<SnapInteractor>();
            //Debug.Log("Interactor is " + interactor.name);
            interactor.SetSelectInteratable(interactable);
            i+=3;
            yield return null;
        }

        // again getting the the previous position
        //yield return null;
        //player.position = initialPos.position;
        //player.rotation = initialPos.rotation;
    }

    public void AddContainersToList(Transform container)
    {
        Debug.Log("Adding hte container to the list");
        containers.Add(container);
    }
   

}
