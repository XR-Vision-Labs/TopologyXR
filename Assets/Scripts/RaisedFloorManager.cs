using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaisedFloorManager : MonoBehaviour
{
    public Transform floor;
    public Transform floorGO;
    public Transform player;

    // getting the 0th value of the floor
    private Vector3 point;
    private GameObject parent;

    public Vector3 Point
    {
        get { return point; }

        set { point = value; }
    }

    public Transform FloorGO
    {
        get { return floorGO; }
        set { floorGO = value; }
    }
    private float floorWidth;
    private float floorLength;


    private void Start()
    {
        floorWidth = floor.localScale.x;
        floorLength = floor.localScale.z;


        parent = new GameObject();

        PassthroughManager.togglePassthrough += TogglePassthrough;
    }

    private void TogglePassthrough()
    {
        parent.SetActive(!parent.activeSelf);
    }

    public void MakeFloor()
    {
        float X = floorGO.localScale.y +3;
        float Z= floorGO.localScale.x + 3;
        Debug.Log("FLoor y is " + floorGO.position.z);
        float y = floorGO.position.y - floor.localScale.y / 2;
        //float y = FloorGO.position.z - floor.localScale.y / 2;

        X++;
        Z++;

        float max, min;
        if (X > Z)
        {
            max = X;
            min = Z;
            Debug.Log("Totalx is big");
        }
        else
        {
            max = Z;
            min = X;
            Debug.Log("Totalz is big");

        }

        float x = point.x;

        Debug.Log("Max length is " + min);
        Debug.Log("Max width is " + max);
        Vector3 pos= new Vector3(x, y, 0);
        SampleController.Instance.Log("Position of floor is " + FloorGO.localScale);
        while (true)
        {
            if(min < 0)
            {
                Debug.Log("Max length get less than 0 " + min);
                break;
            }

            float z = point.z;
            Debug.Log("X position taked " + x);
            pos.z = z;
            pos.x = x;

            float temp = max;
            Debug.Log("Running the loop");
            while (true)
            {
                Debug.Log("X postion inside fn is " + pos.x);
                if (temp < 0)
                {
                    Debug.Log("Max width get less than 0 " + Z);
                    break;
                }


                InstantiateFloorTile(pos);

                z += floorLength;
                pos.z = z;
                temp -= floorLength;
                Debug.Log("reduce max width by " + floorLength);
            }

            x += floorWidth;
            min -= floorWidth;
            Debug.Log("reduce max length by " + floorWidth);
        }

        //player.transform.position += Vector3.up;
        SampleController.Instance.Log("Moving the position up new is "+player.transform.position);

        //floorGO.GetComponent<MeshRenderer>().enabled = false;
    }

    private void InstantiateFloorTile(Vector3 point)
    {
        

        Vector3 scale = floor.localScale;

        point.y += scale.y / 2;
        point.x += scale.x / 2;
        point.z += scale.z / 2;

        var obj=Instantiate(floor, point, Quaternion.identity, parent.transform);
       
    }
}
