using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RFloorSpawner : MonoBehaviour
{
    public Transform floorPrefab;
    public Transform floor;
    public Transform ceiling;

    private Vector3 startPos;
    private Vector3 raisedPos;

    private void Start()
    {
        CalculateSLocation();
        Spawner();
    }

    private Vector3 CalculateDimensions(Transform obj)
    {
        float length = obj.localScale.x / 2;
        float width = obj.localScale.z / 2;
        float height = obj.localScale.y / 2;

        Vector3 d= new Vector3 (length, height, width);
        return d;
    }

    private void CalculateSLocation()
    {
        startPos = CalculateDimensions(floor);
    }

    private void Spawner()
    {
        raisedPos = CalculateDimensions(floorPrefab);


    }
}
