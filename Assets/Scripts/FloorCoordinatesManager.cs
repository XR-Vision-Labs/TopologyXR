using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FloorCoordinatesManager : MonoBehaviour
{
    public List<Vector3> coordinates;
    public List<Vector3> temp;
    public List<Vector3> final;

    private int wallCount;
    private bool isCalled = false;

    private Dictionary<int, Vector3> kv= new Dictionary<int, Vector3>();

    private void Update()
    {
        if(wallCount>0 && wallCount == coordinates.Count && !isCalled)
        {
            isCalled = true;
            SampleController.Instance.Log("Got all the points");

            OnCoordinatesFound();

            
        }
    }
    public void GetWallCount(int count)
    {
        wallCount = count;
    }

    private void Start()
    {
        coordinates = new List<Vector3> ();
        temp = new List<Vector3> ();
        final = new List<Vector3> ();

#if UNITY_EDITOR
        //OnCoordinatesFound();
#endif
    }

    public void AddToList(Vector3 coordinate)
    {
        SampleController.Instance.Log("Adding coordinate to the list");

        coordinates.Add(coordinate);
    }
    
   

    //private void OnCoordinatesFound()
    //{
    //    final.Add(coordinates[0]);

    //    // now we are gonna search the point from coordinate list in second coordinate list and add the point

    //    for(int i = 0; i < coordinates.Count-1; i++)
    //    {
    //        Vector3 last = final[final.Count-1];
    //        // find last in the list 
    //        // get the index and second point of that 
    //        // add that to the list

    //        for(int j=0; j<coordinates.Count; j++)
    //        {
    //            if (coordinates[j].x == last.x)
    //            {
    //                // find the second point
    //                final.Add(second_coordinates[j]);
    //                break;
    //            }
    //        }
    //    }

    //    //final list is complete lets do the other work

    //    if (TopologyManager.Instance != null && final.Count == wallCount)
    //    {
    //        SampleController.Instance.Log("Topology manager found calling coordinates function");
    //        TopologyManager.Instance.GetCoordinates(final);
    //    }
    //}


    private void OnCoordinatesFound()
    {
        // we will shift all the points based on the origin
        // Original coordinates: (-1.25, -0.25), ref point = (0.52, 1.25)
        // New coordinates: (-1.25 - 0.52, -0.25 - 1.25) = (-1.77, -1.50)

        float x = transform.position.x;
        float y = transform.position.y;

        // finding new coordinates based on teh formula

        if (coordinates.Count > 0)
        {
            foreach (Vector3 coordinate in coordinates)
            {
                Vector3 newPoint = new Vector3();

                newPoint.y = coordinate.y;
                newPoint.x = coordinate.x - x;
                newPoint.z = coordinate.z - y;

                temp.Add(newPoint);
            }
        }

        // now we will rearrange the coordinates based on the new point found

        for (int i = 0; i < temp.Count; i++)
        {
            if (temp[i].x < 0 && temp[i].z < 0)
            {


                kv.Add(0, coordinates[i]);
                SampleController.Instance.Log("Got the -ve and -ve vla value");
                SampleController.Instance.Log("Dictionary count is " + kv.Count);
            }

            else if (temp[i].x < 0 && temp[i].z > 0)
            {

                kv.Add(1, coordinates[i]);
                SampleController.Instance.Log("Got the -ve and +ve vli value");
                SampleController.Instance.Log("Dictionary count is " + kv.Count);
            }

            else if (temp[i].x > 0 && temp[i].z > 0)
            {

                kv.Add(2, coordinates[i]);
                SampleController.Instance.Log("Got the +ve and +ve vli value");
                SampleController.Instance.Log("Dictionary count is " + kv.Count);
            }

            else if (temp[i].x > 0 && temp[i].z < 0)
            {
                SampleController.Instance.Log("+ve and -ve value");
                //final.Add(coordinates[i]);
                kv.Add(3, coordinates[i]);
                SampleController.Instance.Log("Got the +ve and -ve vli value");
                SampleController.Instance.Log("Dictionary count is " + kv.Count);
            }
        }

        SampleController.Instance.Log("Dictionary count is " + kv.Count);

        var sortedkeys = kv.Keys.OrderBy(key => key);
        foreach (var key in sortedkeys)
        {
            SampleController.Instance.Log("Value added " + kv[key]);

            final.Add(kv[key]);
        }

        // final list is complete lets do the other work

        if (TopologyManager.Instance != null && final.Count == wallCount)
        {
            SampleController.Instance.Log("Topology manager found calling coordinates function");
            TopologyManager.Instance.GetCoordinates(final);
        }
    }

    
}
