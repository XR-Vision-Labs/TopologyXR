using Parabox.CSG;
using Parabox.CSG.Demo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarveManager : MonoBehaviour
{
  
    private Queue<GameObject> obstacles;

    public List<GameObject> obstacleList;

    public  int index=0;
    [SerializeField]
    private bool isProcessInProgress = false;


    public static CarveManager Instance;

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

        obstacles = new Queue<GameObject>();
        //obstacleList = new List<GameObject>();
        //SetObstacles();

       

    }

    private void SetObstacles()
    {
        if (obstacleList == null)
            return;

        foreach(GameObject obstacle in obstacleList)
        {
            obstacles.Enqueue(obstacle);
            Debug.Log(obstacle);
        }

        
        Debug.Log(obstacles.Count);

        CarveNext();
    }

    public void SetObstacles(Queue<GameObject> obs)
    {
        obstacles = obs;

        CarveNext();
    }

    //public void IsProcessInProgress(bool val)
    //{
    //    isProcessInProgress = val;
    //}

    public void CarveNext()
    {
        if (obstacles != null  &&  obstacles.Count>0)
        {
            Debug.Log("Trigger enabling for index " + index++);
            Debug.Log("Total obstacles in queue " + obstacles.Count);
            //isProcessInProgress = true;

            GameObject obj = obstacles.Dequeue();
            if (obj != null)
            {
                Debug.Log(obj.name);

                //yield return null;
                //obj.GetComponent<BoxCollider>().isTrigger = true;

                //obj.AddComponent<Rigidbody>().isKinematic = true;
                obj.AddComponent<CarveOut>();
                obj.AddComponent<Rigidbody>().isKinematic = true;
                obj.GetComponent<BoxCollider>().isTrigger = true;
            }
            Debug.Log("Total obstacles in queue after dequeue" + obstacles.Count);


        }
    }

    //private void Update()
    //{
    //    whether the process is done
    //    obstacle ++trigger on



    //    if (obstacles != null && !isProcessInProgress && index < obstacles.Count)
    //    {
    //        Debug.Log("Trigger enabling for index " + index++);
    //        Debug.Log("Total obstacles in queue " + obstacles.Count);
    //        isProcessInProgress = true;

    //        GameObject obj = obstacles.Dequeue();
    //        if (obj != null)
    //        {
    //            Debug.Log(obj.name);

    //            yield return null;
    //            obj.GetComponent<BoxCollider>().isTrigger = true;

    //            obj.AddComponent<Rigidbody>().isKinematic = true;
    //            obj.AddComponent<CarveOut>();
    //        }
    //        Debug.Log("Total obstacles in queue after dequeue" + obstacles.Count);


    //    }

    //}
}
