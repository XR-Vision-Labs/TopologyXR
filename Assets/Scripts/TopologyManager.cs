using System;
using System.Collections;
using System.Collections.Generic;

using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class TopologyManager : MonoBehaviour
{
    // given: floor dimensions
    // find the edge coordinates
    // a function to find middle coordinates

    // a function to calculate position of the object
    // a static variable to track totalz and totalx
    // a function to actually spawn the stuffs

    public Transform floor;
    public Transform crac;
    public Transform container;
    public Transform bigWall;
    public List<Vector3> coordinates;
    public List<Vector3> mid_coordinates;
    public static TopologyManager Instance;
    public List<Transform> walls;
    public List<float> areas = new List<float>();
   
    public SetEnvironment env;

    public float space = 1f;

    [Header("Debug")]
    public Transform debugObj;

    private static float totalX;
    private static float totalZ;
    private SceneObj sceneObj;
    private Vector3 mainCoordinate;
    public int mainIndex = 0;
    private RaisedFloorManager floorManager;
    private ServerManager serverManager;
    private float raisedFloorHeight = 0.5f;

    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }

        walls = new List<Transform>();
        floorManager = GetComponent<RaisedFloorManager>();
        serverManager = GetComponent<ServerManager>();
#if UNITY_EDITOR
        //SpawnTopology();
#endif
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            SpawnTopology();
        }
    }

    public void SpawnTopology()
    {
        
        SpawnTopologyWorking();
    }
    public void SpawnTopologyWorking()
    {
        //    initialPos.position = player.position;
        //    initialPos.rotation = player.rotation;
       
        
        Debug.Log("Scene changed");

        if (floor == null)
        {
            SampleController.Instance.Log("Floor is null");
            sceneObj = new SceneObj();
            sceneObj.floor = transform;
            walls = new List<Transform>();
            SampleController.Instance.Log("Scene obj walls " + walls.Count);
            SampleController.Instance.Log("scene obj walls from env " + env.GetFloor().walls.Count);


            // giving floor to raisedfloormanager

            sceneObj = env.GetFloor();

            floor = sceneObj.floor;
            floor.GetComponent<BoxCollider>().isTrigger = false;

            Debug.Log("Passing floor to raised floormanager");
            floorManager.FloorGO = sceneObj.floor;
            SampleController.Instance.Log("Scale of the mesh renderer object " + floor.name + " " + floor.localScale);
            //SampleController.Instance.Log("Scale of the mesh renderer object " + floor.parent.name + " " + floor.parent.localScale);
            SampleController.Instance.Log("Position of the mesh renderer object " + floor.position);
        }


        SampleController.Instance.Log("Floor found working ahead");

        //coordinates = new List<Vector3>();
        mid_coordinates = new List<Vector3>();

        SortGameObjectsByPosition();
        totalX = floor.localScale.y;
        totalZ = floor.localScale.x;
        floorManager.Point = coordinates[0];

        ;

        //SampleController.Instance.Log("Calling make floor function");

        SampleController.Instance.Log("Calling sort gameobject function");

        mainCoordinate = GetBiggestWall();

        floorManager.MakeFloor();
        //floor.position += Vector3.up;

        DebugCoodinates(coordinates);

        CalculateMidCoordinates();
        DebugCoodinates(mid_coordinates);

        CracManager();

        SampleController.Instance.Log($"Mainindex is {mainIndex}");

        mainIndex = 3;

        if (mainIndex == 0)
        {
            ContainerManager();
        }

        else if (mainIndex == 1)
            ContainerManager1();

        else if (mainIndex == 2)
        {
            ContainerManager2();
        }

        else if (mainIndex == 3)
        {
            ContainerManager3();
        }

        // calling this function to tell that all the containers are added
        serverManager.ContainerAdded();

        

    }

    public void SortGameObjectsByPosition()
    {
        SampleController.Instance.Log("Inside sort gameobject by position method with " + coordinates.Count);
        SampleController.Instance.Log("Wall count is " + sceneObj.walls.Count);

        //walls.AddRange(sceneObj.walls);
        walls= new List<Transform>();

        float positionTolerance = 0.01f; // You can adjust this value based on your requirements

        for (int i = 0; i < coordinates.Count; i++)
        {
            SampleController.Instance.Log("Coordinate point is " + coordinates[i]);

            for (int j = 0; j < sceneObj.walls.Count; j++)
            {
                SampleController.Instance.Log("Wall point pos " + sceneObj.walls[j].GetChild(0).position);

                // Compare positions with tolerance
                if (Vector3.Distance(coordinates[i], sceneObj.walls[j].GetChild(0).position) < positionTolerance)
                {
                    SampleController.Instance.Log($"Found the wall for position {sceneObj.walls[j].GetChild(0).position} where point is {coordinates[i]}");
                    walls.Add(sceneObj.walls[j]);
                    break;
                }
            }
        }
        //floor.eulerAngles = new Vector3(90, 0, 0);
        SampleController.Instance.Log("Wall count is " + walls.Count);
        //FixRotation();
        //RefixPoints();

    }

    private void FixRotation()
    {
        SampleController.Instance.Log("Fix rotation function called");
        if (walls.Count < 0)
        {
            return;
        }

        Vector3 angle = Vector3.zero;

        // manually fixing the values of walls y rotation 
        angle.y = -90;
        walls[0].parent.localEulerAngles = angle;
        //walls[0].parent.localEulerAngles = Vector3.zero;
        SampleController.Instance.Log("Previouse angle of wall 0 is " + walls[0].localEulerAngles);
        SampleController.Instance.Log("Previouse angle of wall 0 parent is " + walls[0].parent.localEulerAngles);
        SampleController.Instance.Log("Angle made is " + angle);

        angle.y = 0;
        walls[1].parent.localEulerAngles = angle;
        //walls[1].parent.localEulerAngles = Vector3.zero;

        SampleController.Instance.Log("Previouse angle of wall 0 is " + walls[1].localEulerAngles);
        SampleController.Instance.Log("Previouse angle of wall 0 parent is " + walls[1].parent.localEulerAngles);
        SampleController.Instance.Log("Angle made is " + angle);

        angle.y = 90;
        walls[2].parent.localEulerAngles = angle;
        //walls[2].parent.localEulerAngles = Vector3.zero;

        SampleController.Instance.Log("Previouse angle of wall 0 is " + walls[2].localEulerAngles);
        SampleController.Instance.Log("Previouse angle of wall 0 parent is " + walls[2].parent.localEulerAngles);
        SampleController.Instance.Log("Angle made is " + angle);

        angle.y = 180;
        walls[3].parent.localEulerAngles = angle;
        //walls[3].parent.localEulerAngles = Vector3.zero;

        SampleController.Instance.Log("Previouse angle of wall 0 is " + walls[3].localEulerAngles);
        SampleController.Instance.Log("Previouse angle of wall 0 parent is " + walls[3].parent.localEulerAngles);
        SampleController.Instance.Log("Angle made is " + angle);

        //floor.localScale += new Vector3(2, 0, 2);
        //floor.eulerAngles = new Vector3(90, 0, 0);
    }

    private void RefixPoints()
    {
        for(int i=0; i<coordinates.Count; i++)
        {
            SampleController.Instance.Log("Previous point position is " + coordinates[i]);
            coordinates[i] = new Vector3(walls[i].GetChild(0).position.x, walls[i].GetChild(0).position.y + 1, walls[i].GetChild(0).position.z);

            SampleController.Instance.Log("New point position is " + coordinates[i]);
        }
    }

    private void DebugIndex()
    {
        for (int i = 0; i < walls.Count; i++)
        {
            walls[i].GetComponentInChildren<TextMeshPro>().text = i.ToString();
        }
    }

    private Vector3 GetBiggestWall()
    {
        SampleController.Instance.Log("Inside get biggest wall funciton");
        if (walls.Count < 0)
        {
            SampleController.Instance.Log("Scene object is null so returning");
            return Vector3.zero;
        }


        // calculating the areas of all the walls
         areas = new List<float>();

        //foreach(Transform t in walls)
        //{
        //    SampleController.Instance.Log("Adding calulated area to the list");
        //    areas.Add(CalculateArea(t.transform.localScale));
        //}

        for (int i = 0; i < walls.Count - 1; i++)
        {
            SampleController.Instance.Log("Adding the area to teh list");

            areas.Add(CalculateLength(walls[i].localScale));
        }

        SampleController.Instance.Log("Areas list is " + areas.Count);

        // finding the maximum area
        float maxArea = 0;

        for (int i = 0; i < areas.Count; i++)
        {
            if (areas[i] > maxArea)
            {
                mainIndex = i;
                maxArea = areas[i];

                SampleController.Instance.Log($"Current wall has more area {maxArea} with index {i}");
            }
            else
            {
                SampleController.Instance.Log("Current wall is less than other");
            }
        }

        // finding which was having the maximum area
        //walls[mainIndex].GetComponent<MeshRenderer>().material.color = Color.blue;

        Transform point = walls[mainIndex].transform.GetChild(0);

        //point.GetComponent<MeshRenderer>().material.color = Color.red;

        return point.localPosition;
    }

    private float CalculateArea(Vector3 scale)
    {
        return scale.x * scale.z;
    }

    private float CalculateLength(Vector3 a)
    {
        
        return a.x * a.z;
    }

    private void DebugCoodinates(List<Vector3> l)
    {
        foreach (Vector3 c in l)
        {
            SampleController.Instance.Log($"Coordinates of point is {c}");
            Instantiate(debugObj, c, Quaternion.identity);
        }
    }

    public void GetCoordinates(List<Vector3> l)
    {
        SampleController.Instance.Log("Get the coordinate function");
        coordinates = l;

        SampleController.Instance.Log("Set coordinates count is  " + coordinates.Count);

        
    }

    private void CalculateCoordinates()
    {
        SampleController.Instance.Log("Calculating coordinates");

        float x = floor.localScale.x / 2;
        float y = floor.localScale.y / 2;
        SampleController.Instance.Log($"X and Y calculated in calculatecoordinates is {x} and {y}");

        // find the coordinte

        x += floor.position.x;
        y += floor.position.y;

        coordinates.Add(new Vector3(-x, 0, -y));
        coordinates.Add(new Vector3(-x, 0, y));
        coordinates.Add(new Vector3(x, 0, y));
        coordinates.Add(new Vector3(x, 0, -y));
    }

    private void CalculateMidCoordinates()
    {
        SampleController.Instance.Log("Calculating middle coordinates with coordinates " + coordinates.Count);


        for (int i = 0; i < coordinates.Count; i++)
        {

            if (i == coordinates.Count - 1)
            {
                mid_coordinates.Add(CalculateMidPoint(coordinates[coordinates.Count - 1], coordinates[0]));
            }
            else
            {
                mid_coordinates.Add(CalculateMidPoint(coordinates[i], coordinates[i + 1]));
            }

        }
    }

    private Vector3 CalculateMidPoint(Vector3 a, Vector3 b)
    {
        SampleController.Instance.Log("Calculating mid point");


        Vector3 midPoint = new Vector3();

        midPoint.x = (a.x + b.x) / 2;
        midPoint.z = (a.z + b.z) / 2;
        midPoint.y = floor.position.y;

        SampleController.Instance.Log("midpoint set to floor postiion that is " + floor.position.y);
        return midPoint;
    }

    private Vector3 CalculateObjPosition(Transform obj, Vector3 point)
    {
        Debug.Log("MidPoint is " + point);
        SampleController.Instance.Log("MidPoint is " + point);

        Vector3 scale = obj.localScale;
        Vector3 temp = Vector3.zero;

        temp.x = scale.z / 2;
        temp.z = scale.x / 2;
        temp.y = scale.y / 2;

        
        point += scale;

        Debug.Log("Object scale is " + scale);
        Debug.Log("Object positon calculated is " + point);

        return point;
    }

    private void CracManager()
    {
        // crac 1

        Vector3 midPoint = CalculateMidPoint(coordinates[mainIndex], mid_coordinates[mainIndex]);
        Debug.Log("midpoint is " + mid_coordinates[mainIndex] + " point is " + coordinates[mainIndex]);
        SampleController.Instance.Log("midpoint is " + mid_coordinates[mainIndex] + " point is " + coordinates[mainIndex]);

        SpawnCRAC(midPoint);



        // crac 2
        if (mainIndex == 3)
        {
            Vector3 midPoint3 = CalculateMidPoint(coordinates[0], mid_coordinates[mainIndex]);
            Debug.Log("midpoint is " + mid_coordinates[mainIndex] + " point is " + coordinates[0]);
            SpawnCRAC(midPoint3);
            return;
        }

        Vector3 midPoint2 = CalculateMidPoint(coordinates[mainIndex + 1], mid_coordinates[mainIndex]);
        Debug.Log("midpoint is " + mid_coordinates[mainIndex] + " point is " + coordinates[mainIndex + 1]);
        SpawnCRAC(midPoint2);
    }

    private void SpawnCRAC(Vector3 pos)
    {
        

        float y = crac.localScale.y / 2 + raisedFloorHeight;
        float x = crac.localScale.x / 2;
        float z = crac.localScale.z / 2;

        float deg = 0;
        
        pos.y += y;
        switch (mainIndex)
        {
            // change z if floor is changed
            case 0:
                pos.x += x;
                //deg = 90;
                break;

            case 1:

                deg = 90;
                pos.z -= x;
                break;

            case 2:
                deg = 180;
                pos.x += x;
                break;

            case 3:
                deg = -90;
                pos.z -= x;
                //deg = 0;
                break;
        }


        Transform obj=Instantiate(crac, pos, Quaternion.identity);
        obj.Rotate(Vector3.up,angle: deg);
        DCSceneManager.Instance.AddToList(obj.gameObject);
        
        
    }

    private void ContainerManager1()
    {
        float cracX = crac.localScale.x; 
        float cracZ = crac.localScale.z; 
        float contX = container.localScale.x; 
        float contZ = container.localScale.z;
        float max, min;
        if (totalX > totalZ)
        {
            max = totalX;
            min = totalZ;
        }
        else
        {
            max = totalZ;
            min = totalX;
        }
        float z = coordinates[mainIndex].z - cracX;

        Debug.Log($"Total x is {totalZ} and total z is {totalX}");

        // change z if rotation of floor changed
        while (true)
        {
            min -= space;
            z -= space;

            float x = coordinates[mainIndex].x;
            Vector3 cont = new Vector3(x, floor.position.y, z);
            cont.z = z;

            if (min - space - contX < 0)
            {
                break;
            }

            float temp = max;

            while (true)
            {
                temp -= space;

                if (temp - space - contZ <= 0)
                {
                    break;
                }

                x += space;

                cont.x = x;

                Vector3 pos = cont;
                pos.y += container.localScale.y / 2 + raisedFloorHeight;
                pos.x += contX / 2;
                pos.z -= contZ / 2;

                var obj=Instantiate(container, pos, container.rotation);
                DCSceneManager.Instance.AddToList(obj.gameObject);
                serverManager.AddContainersToList(obj);
                //obj.Rotate(Vector3.up, 90);

                temp -= contX;

                x += contX;
            }

            //var obj=Instantiate(container, cont, container.rotation);

            min -= contZ;

            z -= contZ;
        }

    } 
    
    private void ContainerManager3()
    {
        float cracX = crac.localScale.x; 
        float cracZ = crac.localScale.z; 
        float contX = container.localScale.x; 
        float contZ = container.localScale.z;

        float max, min;
        if (totalX > totalZ)
        {
            max = totalX;
            min = totalZ;
            Debug.Log("Totalx is big");
        }
        else
        {
            max = totalZ;
            min = totalX;
            Debug.Log("Totalz is big");

        }

        float z = coordinates[mainIndex].z + cracX;

        Debug.Log($"Total x is {totalX} and total z is {totalZ}");

        while (true)
        {
            min -= space;
            z += space;

            float x = coordinates[mainIndex].x;
            Vector3 cont = new Vector3(x, floor.position.y, z);
            cont.z = z;

            if (min - space - contX < 0)
            {
                break;
            }

            float temp = max;

            while (true)
            {
                temp -= space;

                if (temp - space - contZ <= 0)
                {
                    break;
                }

                x -= space;

                cont.x = x;

                Vector3 pos = cont;
                pos.y += container.localScale.y / 2 + raisedFloorHeight;
                pos.x -= contX / 2;
                pos.z += contZ / 2;

                var obj=Instantiate(container, pos, container.rotation);
                serverManager.AddContainersToList(obj);
                DCSceneManager.Instance.AddToList(obj.gameObject);
                //obj.Rotate(Vector3.up, 90);

                temp -= contX;

                x -= contX;
            }

            //var obj=Instantiate(container, cont, container.rotation);

            min -= contZ;

            z += contZ;
        }

    }


    private void ContainerManager()
    {
        float cracX = crac.localScale.x;
        float cracZ = crac.localScale.z;
        float contX = container.localScale.x;
        float contZ = container.localScale.z;

        float max, min;
        if (totalX > totalZ)
        {
            max = totalX;
            min = totalZ;
            Debug.Log("Totalx is big");
        }
        else
        {
            max = totalZ;
            min = totalX;
            Debug.Log("Totalz is big");

        }

        float x = coordinates[mainIndex].x + cracX;
        Debug.Log("X coordinate got is " + coordinates[mainIndex].x);
        SampleController.Instance.Log("X value got is " + x);

        Debug.Log("total x is " + totalZ);

        min  -= cracX;
        Debug.Log("total x after first is " + totalZ);

        while (true)
        {
            min -= space;
            x += space;
            float z = coordinates[mainIndex].z;
            Vector3 cont = new Vector3(x, floor.position.y, z);
            cont.x = x;
            Debug.Log("x is " + totalX);

            if (min - space - contX <= 0)
            {
                break;
            }

            Debug.Log("Further working with totalx " + totalX);
            float temp = max;
            Debug.Log("Temp value is " + temp);

            while (true)
            {

                temp -= space;
                Debug.Log(temp);
                Debug.Log("Crac z is " + cracZ);
                // condition is checking if after removing one space for the back and container if it is not 0 we will spawn a container
                if (temp - space - contX <= 0)
                {
                    Debug.Log("Value get below 0 " + (temp - space - contX));
                    SampleController.Instance.Log("Value get below 0 " + (temp - space - contX));

                    break;
                }

                z += space;

                cont.z = z;
                Vector3 pos = cont;
                pos.y += container.localScale.y / 2 + raisedFloorHeight;
                pos.z += contX/2;
                pos.x += contZ / 2;

                var obj = Instantiate(container, pos, container.rotation);
                serverManager.AddContainersToList(obj);
                DCSceneManager.Instance.AddToList(obj.gameObject);
                obj.Rotate(Vector3.up, 90);

                temp -= contX;
                z += contX;
            }
            min -= contZ;
            x += contZ;
        }
    }
    
    private void ContainerManager2()
    {
        float cracX = crac.localScale.x;
        float cracZ = crac.localScale.z;
        float contX = container.localScale.x;
        float contZ = container.localScale.z;

        float max, min;
        if (totalX > totalZ)
        {
            max = totalX;
            min = totalZ;
            Debug.Log("Totalx is big");
        }
        else
        {
            max = totalZ;
            min = totalX;
            Debug.Log("Totalz is big");

        }

        float x = coordinates[mainIndex].x - cracX;
        Debug.Log("X coordinate got is " + coordinates[mainIndex].x);
        SampleController.Instance.Log("X value got is " + x);

        Debug.Log("total x is " + totalX);

        min  -= cracX;
        Debug.Log("total x after first is " + totalX);

        while (true)
        {
            min -= space;
            x -= space;
            float z = coordinates[mainIndex].z;
            Vector3 cont = new Vector3(x, floor.position.y, z);
            cont.x = x;
            Debug.Log("x is " + totalX);

            if (min - space - contX <= 0)
            {
                break;
            }

            Debug.Log("Further working with totalx " + totalX);
            float temp = max;
            Debug.Log("Temp value is " + temp);

            while (true)
            {

                temp -= space;
                Debug.Log(temp);
                Debug.Log("Crac z is " + cracZ);
                // condition is checking if after removing one space for the back and container if it is not 0 we will spawn a container
                if (temp - space - contX <= 0)
                {
                    Debug.Log("Value get below 0 " + (temp - space - contX));
                    SampleController.Instance.Log("Value get below 0 " + (temp - space - contX));

                    break;
                }

                z -= space;

                cont.z = z;
                Vector3 pos = cont;
                pos.y += container.localScale.y / 2 + raisedFloorHeight;
                pos.z -= contX / 2;
                pos.x -= contZ / 2;

                var obj = Instantiate(container, pos, container.rotation);
                serverManager.AddContainersToList(obj);
                DCSceneManager.Instance.AddToList(obj.gameObject);
                obj.Rotate(Vector3.up, 90);

                temp -= contX;
                z -= contX;
            }
            min -= contZ;
            x -= contZ;
        }
    }
        //    //totalZ = totalZ - space;
        //    //float z = coordinates[0].z + space;

        //    //Vector3 cont1= new Vector3(x, 0, z);
        //    //Vector3 pos =CalculateObjPosition(container, cont1);
        //    //Instantiate(container, pos, Quaternion.identity);

        //    //totalZ= totalZ-space-container.localScale.z;
        //    //Debug.Log("Total z is " + totalZ);

        //    //z= z + container.localScale.z + space;
        //    //cont1.z = z;
        //    //pos= CalculateObjPosition(container, cont1);
        //    //Instantiate(container, pos, Quaternion.identity);
        //}
    }

