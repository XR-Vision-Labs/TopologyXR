using Parabox.CSG;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[Serializable]
public class WallData
{
    public GameObject wall;

    public Queue<GameObject> obstacles;

    public void ReplaceWall(GameObject newWall)
    {
        wall = newWall;
        Debug.Log($"Old wall is replaced with {newWall.name}");
    }

    public void EnqueueObstacle(GameObject obstacle)
    {
        obstacles.Enqueue(obstacle);

        Debug.Log($"{obstacle.name} is added to queue");
    }

    public WallData()
    {
        wall = null;
        obstacles = new Queue<GameObject>();
    }
}
namespace Parabox.CSG.Demo
{
    public class WallManager : MonoBehaviour
    {
        public List<WallData> wallDataList;
        public int wallCount;
        public int obstacleCount;

        public int currentWalls;
        public int currentObstacles;

        public TextMeshProUGUI countText;
        public TextMeshProUGUI currentCounttext;

        private bool isProcessing = false;

        public static WallManager instance;
        private bool isCalled = false;

        private List<GameObject> walls;
        private void Start()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(this);
            }

            walls = new List<GameObject>();
            wallDataList = new List<WallData>();

            Debug.Log("Creating an empty gameobject");
            SampleController.Instance.Log("Creating an empty gameobject");

            //GameObject newObj = new GameObject(name: "ResultantMesh");
        }

        public void SetCounts(int wallCount, int obstacleCount)
        {
            this.wallCount = wallCount;
            this.obstacleCount = obstacleCount;

            Debug.Log($"Set wall count to {wallCount} and obstacle count to {obstacleCount}");
            SampleController.Instance.Log($"Set wall count to {wallCount} and obstacle count to {obstacleCount}");
        }

        private void Update()
        {
            if (!isCalled && CheckIfReadyForProcessing())
            {
                Debug.Log("Calling process function");
                SampleController.Instance.Log("Calling process function");
                isCalled = true;

                StartCoroutine(ProcessWallsCoroutine());
            }

            countText.text = $"T walls {wallCount}, T Obstacles {obstacleCount}";
            currentCounttext.text = $"Walls {currentWalls}, Obstacles {currentObstacles}";
        }

        public void AddWall(GameObject wall)
        {

            //if (currentWalls < wallCount && !walls.Contains(wall))
            //{
                WallData newWallData = new WallData { wall = wall };
                walls.Add(wall);
                wallDataList.Add(newWallData);

                Debug.Log($"Wall added to list");
                SampleController.Instance.Log($"Wall added to list");

                currentWalls++;

                Debug.Log("wall " + currentWalls);
                SampleController.Instance.Log("wall " + currentWalls);
            //}
            //else
            //{
            //    SampleController.Instance.Log("No more walls are adding");
            //}
           
            //if (CheckIfReadyForProcessing())
            //{
            //    Debug.Log($"data is ready to process calling function");
            //    SampleController.Instance.Log($"data is ready to process calling function");

            //    StartCoroutine(ProcessWallsCoroutine());
            //}

        }

        public void AddObstacleToWall(GameObject wall, GameObject obstacle)
        {

            //if(currentObstacles < obstacleCount)
            //{
                WallData wallData = wallDataList.Find(w => w.wall == wall);
                if (wallData != null)
                {
                    Debug.Log(wallData.wall.name);
                    Debug.Log(wallData.obstacles.Count);
                    Debug.Log(obstacle.gameObject.name);
                    wallData.obstacles.Enqueue(obstacle);
                }

                currentObstacles++;
                Debug.Log("obstacle " + currentObstacles);
                SampleController.Instance.Log("obstacle " + currentObstacles);
            //}
            //else
            //{
            //    SampleController.Instance.Log("No more obstacles are adding");
            //}
            

            //if (CheckIfReadyForProcessing())
            //{
            //    Debug.Log($"data is ready to process calling function");
            //    SampleController.Instance.Log($"data is ready to process calling function");
            //    StartCoroutine(ProcessWallsCoroutine());
            //}
        }

        bool CheckIfReadyForProcessing()
        {
            if (currentWalls == wallCount && currentObstacles == obstacleCount)
            {
                return true;
            }

            return false;
        }

        private IEnumerator ProcessWallsCoroutine()
        {
            Debug.Log("Inside process wall function");
            isProcessing = true;
            foreach (var wallData in wallDataList)
            {
                Debug.Log("processing wall with obstacle " + wallData.obstacles.Count.ToString());
                SampleController.Instance.Log("processing wall with obstacle " + wallData.obstacles.Count.ToString());

                
                List<GameObject> objectsToDestroy = new List<GameObject>();

                while (wallData.obstacles.Count > 0)
                {
                    //originalWall = wallData.wall;

                    GameObject obstacle = wallData.obstacles.Dequeue().gameObject;
                    Debug.Log($"deque obstacle with name {obstacle.name}");
                    SampleController.Instance.Log($"deque obstacle with name {obstacle.name}");

                    Debug.Log(wallData.wall.name);


                    Model result;

                    GameObject wall = wallData.wall.gameObject;

                    result = CSG.Subtract(wall, obstacle);

                    GameObject newWall = new GameObject(name: "ResultantMesh");
                    newWall.AddComponent<MeshFilter>().sharedMesh = result.mesh;
                    newWall.AddComponent<MeshRenderer>().sharedMaterials = result.materials.ToArray();

                    newWall.AddComponent<BoxCollider>().isTrigger = true;
                    newWall.AddComponent<Rigidbody>().isKinematic = true;
                    newWall.AddComponent<OVRSceneAnchor>();
                    wallData.ReplaceWall(newWall);


                    objectsToDestroy.Add(obstacle);
                    //yield return null;
                    Destroy(wall);
                }

                // Destroy the original wall and obstacles after processing is complete
                Debug.Log("Destroying old wall and obstacles");
                SampleController.Instance.Log("Destroying old wall and obstacles");
                
                foreach (var obstacle in objectsToDestroy)
                {
                    Destroy(obstacle);
                }

                yield return null;  // Wait for next frame before processing the next wall
            }
            isProcessing = false;
        }


    }

}
