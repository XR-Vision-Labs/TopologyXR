using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using Parabox.CSG.Demo;
using System;
using TMPro;

public class WorldGenerationController : MonoBehaviour
{
    [SerializeField]
    GameObject wallPrefab;

    [SerializeField]
    GameObject floorprefab;

    [SerializeField]
    GameObject ceilingprefab;

    [SerializeField]
    GameObject obstaclePrefab;

    private Scene scene;

    public float floorHeightSet = -0.5f;


    List<GameObject> sceneObjects = new List<GameObject>();
    Queue<GameObject> obstacleObject = new Queue<GameObject>();

    public static float floorHeight=0;


    bool sceneAlignmentApplied = false;

    public static bool valueChanged = false;

    public static bool IsWorldGenerated;

    public int obstacleCount = 3;

    //public TextMeshProUGUI wallCount;

    void Start()
    {
#if UNITY_EDITOR

        Debug.Log("scene creation function calling");

        Scene sampleScene = GenerateSampleScene();
        //Debug.Log(GenerateSampleScene());

        //yield return null;
       GenerateWorld(sampleScene);
        //Debug.Log("scene creation function called");

        //WallManager.instance.SetCounts(4, obstacleCount);

        valueChanged = true;
        floorHeight = floorHeightSet;
        Debug.Log("floor value set");
#endif

        
    }


    private Scene GenerateSampleScene()
    {
        Debug.Log("Inside generate function");

        Common.Scene scene = new Common.Scene();

        Debug.Log(scene);

        // Define the floor plane.
        scene.floor = new Common.Plane
        {
            position = new Vector3(0f, -0.5f, 0f),
            rotation = new Quaternion(90, 0, 0, 0),
            rect = new Rect(-5f, -5f, 10f, 10f) // Example floor dimensions
        };

        Debug.Log(scene.floor.position);

        // Define the ceiling plane.
        scene.ceiling = new Common.Plane
        {
            position = new Vector3(0f, 3f, 0f), // Adjust the height of the ceiling as needed
            rotation = new Quaternion(90, 0, 0, 0),
            rect = new Rect(-5f, -5f, 10f, 10f) // Example ceiling dimensions
        };

        Debug.Log(scene.ceiling.position);
        // Define the walls (assuming there are 4 walls).
        scene.walls = new Common.Plane[4];

        for (int i = 0; i < 4; i++)
        {
            float wallX = Mathf.Cos(Mathf.PI / 2 * i) * 5f; // Example wall position
            float wallZ = Mathf.Sin(Mathf.PI / 2 * i) * 5f; // Example wall position

            scene.walls[i] = new Common.Plane
            {
                position = new Vector3(wallX, 1.5f, wallZ), // Adjust the height of the walls as needed
                rotation = Quaternion.Euler(0f, 90f * i, 0f), // Example wall rotation
                rect = new Rect(0f, 0f, 4f, 3f) // Example wall dimensions
            };
        }

        Debug.Log(scene.walls.Length);

        // Define obstacles (door and window).
        scene.obstacles = new Common.Obstacle[4];

        scene.obstacles[0] = new Common.Obstacle
        {
            position = new Vector3(0f, 1.5f, 0f), // Example door position
            rotation = Quaternion.Euler(0f, 90f, 0f), // Example door rotation
            boundingBox = new Bounds(new Vector3(2f, 1.5f, 5f), new Vector3(1f, 2.5f, 0.1f)), // Example door dimensions
            type = Common.ObstacleType.Door
        };

        scene.obstacles[1] = new Common.Obstacle
        {
            position = new Vector3(-5f, 1.5f, 0f), // Example window position
            rotation = Quaternion.Euler(0f, 0f, 0f), // Example window rotation
            boundingBox = new Bounds(new Vector3(-5f, 1.5f, 0f), new Vector3(0.1f, 2.5f, 2f)), // Example window dimensions
            type = Common.ObstacleType.Window
        };
        
        scene.obstacles[2] = new Common.Obstacle
        {
            position = new Vector3(-4f, 1.5f, 0f), // Example window position
            rotation = Quaternion.Euler(0f, 0f, 0f), // Example window rotation
            boundingBox = new Bounds(new Vector3(-5f, 1.5f, 0f), new Vector3(0.1f, 2.5f, 2f)), // Example window dimensions
            type = Common.ObstacleType.Window
        };
        
        scene.obstacles[3] = new Common.Obstacle
        {
            position = new Vector3(-3f, 1.5f, 0f), // Example window position
            rotation = Quaternion.Euler(0f, 0f, 0f), // Example window rotation
            boundingBox = new Bounds(new Vector3(-5f, 1.5f, 0f), new Vector3(0.1f, 2.5f, 2f)), // Example window dimensions
            type = Common.ObstacleType.Window
        };

        Debug.Log(scene);

        return scene;
    }

    public void GenerateWorld(Scene scene)
    {

        this.scene = scene;
        //SampleController.Instance.Log("Updating Generated World...");
        //SampleController.Instance.Log($"Walls: {scene.walls.Length}");
        //SampleController.Instance.Log($"Floor: {scene.floor}");
        //SampleController.Instance.Log($"Obstacles: {scene.obstacles.Length}");

        SampleController.Instance.Log("obstacles are "+scene.obstacles.Length);

        //yield return null;
        //WallManager.instance.SetCounts(scene.walls.Length, scene.obstacles.Length);
        SampleController.Instance.Log($"Set wall manager counts with walls = {scene.walls.Length} and obstacles = {scene.obstacles.Length}");

        foreach (GameObject obj in sceneObjects)
            Destroy(obj);
        sceneObjects.Clear();

        //wallCount.text = scene.walls.Length.ToString();

        GameObject newFloor = GameObject.Instantiate(floorprefab, scene.floor.position, scene.floor.rotation);
        newFloor.transform.localScale = new Vector3(scene.floor.rect.width, scene.floor.rect.height, 0.05f);
        newFloor.transform.rotation = scene.floor.rotation * Quaternion.Euler(180, 0, 0);



        if(newFloor.activeSelf && newFloor != null)
        {

            valueChanged=true;
            floorHeight= newFloor.transform.position.y;
            SampleController.Instance.Log("Floor is " + floorHeight.ToString());
        }
        
        newFloor.SetActive(sceneAlignmentApplied);
        if (newFloor.GetComponent<BoxCollider>() == null)
        {
        newFloor.AddComponent<BoxCollider>();
            SampleController.Instance.Log("Collider not present added");
        }
        sceneObjects.Add(newFloor);

        GameObject newCeiling = GameObject.Instantiate(ceilingprefab, scene.ceiling.position, scene.ceiling.rotation);
        newCeiling.transform.localScale = new Vector3(scene.ceiling.rect.width, scene.ceiling.rect.height, 0.05f);
        newCeiling.transform.rotation = scene.ceiling.rotation * Quaternion.Euler(180, 0, 0);
        newCeiling.SetActive(sceneAlignmentApplied);
        if (newCeiling.GetComponent<BoxCollider>() == null)
        {
            newCeiling.AddComponent<BoxCollider>();
            SampleController.Instance.Log("Collider not present added");
        }

       
        sceneObjects.Add(newCeiling);
        //int index = 0;
        

        foreach (var wall in scene.walls)
        {
            GameObject newWall = GameObject.Instantiate(wallPrefab, wall.position, wall.rotation);
            newWall.transform.localScale = new Vector3(wall.rect.width, wall.rect.height, 0.05f);
            newWall.transform.rotation = wall.rotation * Quaternion.Euler(0, 180, 0);
            newWall.SetActive(sceneAlignmentApplied);

            sceneObjects.Add(newWall);
            
        }

        
    }

   

    public void ShowSceneObjects()
    {
        foreach (GameObject sceneObj in sceneObjects)
        {
            sceneObj.SetActive(true); 
        }

        sceneAlignmentApplied = true;
    }

    public void ToggleGameobject()
    {
        foreach(GameObject obj in sceneObjects)
        {
            MeshRenderer[] renderer = obj.GetComponentsInChildren<MeshRenderer>();
            foreach(MeshRenderer mr in renderer)
            {
                mr.enabled = !mr.enabled;
            }
        }
    }

}
