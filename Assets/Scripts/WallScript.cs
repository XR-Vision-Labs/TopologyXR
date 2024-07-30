using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Parabox.CSG.Demo;

public class WallScript : MonoBehaviour
{
    //private bool isFunctionCalled=false;
    public List<GameObject> obstacle;
    private void Start()
    {
    //    //yield return null;
            obstacle = new List<GameObject>();

        WallManager.instance.AddWall(this.gameObject);
        Debug.Log("Wall added to list");
        SampleController.Instance.Log("Wall added to list");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("obstacle") && !obstacle.Contains(other.gameObject))
        {
            Debug.Log($"Trigger with {other.tag}");
            SampleController.Instance.Log($"Trigger with {other.tag}");
            //isFunctionCalled=true;

            obstacle.Add(other.gameObject);
            SampleController.Instance.Log($"Added {other.gameObject} in the list");
            Debug.Log($"Added {other.gameObject} in the list");
            
            //yield return null;
            WallManager.instance.AddObstacleToWall(this.gameObject, other.gameObject);

            Debug.Log("Added obstacle with wall");
            SampleController.Instance.Log("Added obstacle with wall");
        }
    }
}
