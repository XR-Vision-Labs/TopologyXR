using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MountSpawner : MonoBehaviour
{
    public GameObject rack1;
    public GameObject rack2;
    public GameObject pole;

    public Transform pos;

    public void SpawnRack1()
    {
        PerformSpawn(rack1);
    }

    public void SpawnRack2()
    {
        PerformSpawn(rack2);
    }

    public void SpawnPole()
    {
        PerformSpawn(pole);
    }

    private void PerformSpawn(GameObject obj)
    {
        var spawned = Instantiate(obj, pos.position, Quaternion.identity);
        spawned.transform.eulerAngles = new Vector3(0, pos.rotation.eulerAngles.y, 0);
        DCSceneManager.Instance.AddToList(spawned);
    }

}
