
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class  SceneObj
{
    public Transform floor;

    public List<Transform> walls;
}

public class SetEnvironment : MonoBehaviour
{
    public OVRSceneManager manager;

    public List<MeshRenderer> render= new List<MeshRenderer>();

    public GameObject testCube;

    public Transform rightHand;

    private bool gotRenderers = false;

    private SceneObj sceneObj;

    private void Start()
    {
        manager.InitialAnchorParent = transform;
        manager.SceneModelLoadedSuccessfully += GetRenderers;

        //ToggleGO();
        //StartCoroutine(GetRenderers());
    }

    private void Update()
    {
        //if (OVRInput.Get(OVRInput.Button.One))
        //{
        //    ToggleGO();
        //}

        if (Input.GetKeyDown(KeyCode.V))
        {
            ToggleGO();
        }

        //if (OVRInput.GetDown(OVRInput.Button.Two))
        //{
        //    Instantiate(testCube, rightHand.position, Quaternion.identity);
        //}

        
    }

    public SceneObj GetFloor()
    {
        SampleController.Instance.Log("Inside get floor function");
        if(sceneObj == null)
        {
            return null;
        }
        return sceneObj;
    }

    //private void CallGetRenderer()
    //{
    //    SampleController.Instance.Log("Calling get renderer function");

    //    StartCoroutine(GetRenderers());

    //    SampleController.Instance.Log("Called the renderer function");
    //}

    private void DebugRotations()
    {
        foreach(var r in render)
        {
            SampleController.Instance.Log("Rotations are");
            SampleController.Instance.Log("Rotation of go is " + r.transform.localEulerAngles);
            SampleController.Instance.Log("Rotation of go parent is " + r.transform.parent.localEulerAngles);
        }
    }

    private void GetRenderers()
    {
        //SampleController.Instance.Log("Inside get renderer function");
        sceneObj = new SceneObj();
        sceneObj.walls = new List<Transform>();

        //yield return null;
        //SampleController.Instance.Log("Child count is " + transform.childCount);

        if (transform.childCount > 0)
        {
            Debug.Log("Found childs now getting meshrenderers");

            MeshRenderer[] renderers = transform.GetChild(0).GetComponentsInChildren<MeshRenderer>();

            foreach(var r in renderers)
            {
                
                if(r.CompareTag("floor"))
                {
                    sceneObj.floor = r.transform;
                    render.Add(r);

                    //Vector3 angles = new Vector3(90, 0, 0);
                    //r.transform.eulerAngles = angles;
                    // setting rotation to nearest 90 or 180 
                    
                }

                else if (r.CompareTag("wall"))
                {
                    sceneObj.walls.Add(r.transform);
                    render.Add(r);
                    
                }

                else if (r.CompareTag("ceiling"))
                {
                    render.Add(r);
                    //Vector3 angles = new Vector3(90, 180, 0);
                    //r.transform.eulerAngles = angles;
                }
            }
            

            //render = renderers.ToList();
            Debug.Log("Got the mesh rendereers " + renderers.Length);

            if(sceneObj.floor!=null && sceneObj.walls.Count > 0)
            {
                //SampleController.Instance.Log("Floor is not null so calling getwallcount function");

                sceneObj.floor.GetComponent<FloorCoordinatesManager>().GetWallCount(sceneObj.walls.Count);
            }


            DebugRotations();
            gotRenderers = true;
        }

        foreach(var r in render)
        {
            if(r.GetComponent<Collider>() == null)
            {
                r.AddComponent<BoxCollider>();
                //r.transform.eulerAngles = RoundToNearest90(transform.eulerAngles);
                //if (r.CompareTag("floor"))
                //{
                //    r.GetComponent<BoxCollider>().isTrigger = true;

                //    SampleController.Instance.Log("Floor trigger enabled");
                //}
            }

           

            //SampleController.Instance.Log("Added all the colliders");
        }       

        
    }

    

    public void ToggleGO()
    {

        if (!gotRenderers)
        {
           GetRenderers();
           
        }

#if !UNITY_EDITOR
        foreach (var renderer in render)
        {
            renderer.enabled = !renderer.enabled;
        }
    
#endif
    }
}
