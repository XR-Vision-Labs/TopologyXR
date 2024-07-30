using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DCSceneManager : MonoBehaviour
{
    public static DCSceneManager Instance;
    [SerializeField]
    private List<GameObject> spawnedObjects;

    private void Start()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }

        spawnedObjects = new List<GameObject>();
    }

    public void AddToList(GameObject obj)
    {
        SampleController.Instance.Log("Adding the gameobject " + obj.name + " to the list");

        spawnedObjects.Add(obj);
    }

    public void Reset()
    {
        foreach(GameObject obj in spawnedObjects)
        {
            Destroy(obj);
        }

        spawnedObjects.Clear();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Reset();
        }
    }

}
