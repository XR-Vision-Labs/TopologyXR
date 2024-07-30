using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
using static TransformMenu;

public class WireController : MonoBehaviour
{
    //author: nicogarcia.s.dev / nicogarcia_s_dev
    //no license

    /// <summary>
    /// Project setup:
    /// Project Settings > Physics > Default Solver Iterations: Set between 10 to 20
    /// If you use URP update materials.
    /// 
    /// Create a new layer, example: "wire".
    /// Go to Proyect Settings > Physics > Layer Collision Matrix > disables collisions of the layer with itself.
    /// Set the layer "wire" in the prefabs EndAnchor, segment, segmentNoPhysics, StartAnchor, WireBuilder. Change childrens as well. 
    /// Do not change plug layer. 
    /// 
    /// If you use URP add layer "wire" to URP Renderer Data > Filtering.
    /// 
    /// Keep the gizmos active to be able to select position.
    /// </summary>

    /// <summary>
    /// How to use:
    /// Put the prefab WireBuilder in your scene.
    /// Choose the starting position by right-clicking with the WireBuilder object selected in the hierarchy.
    /// Press Set Start
    /// Choose again the position by right-clicking.
    /// Press Add Segment.
    /// You can select position again and add more segments if you want.
    /// Press Set End to finish the wire.
    /// Select position and press Set Plug to add the plug if needed.
    /// Press Clear if you want to delete the entire wire and start over from scratch.
    /// Press undo to undo the previous segment creation.
    /// Press Render Wire to update the mesh render of the wire in case you move segments individually from the editor.
    /// 
    /// Only if you are using the wire without physics and you don't want to modify the wire anymore
    /// press Finish no physics wire, this removes the segments as they are not needed because the positions are stored in TubeRender.cs
    /// it also removes references and some components. To improve performance.
    /// </summary>


    #region TIPS
    [TextArea]
    [Tooltip("Dont remove Notes variable.")]
    public string Notes = "With the WireBuilder object selected use right click to select position. Have active Gizmos.";
    [TextArea]
    [Tooltip("Dont remove Notes2 variable.")]
    public string Notes2 = "Wire render settings in TubeRender.cs on WireRender object.";
    #endregion

    [Header("SETTINGS")]
    [Tooltip("Disabling it removes the wire physics, for use as a prop (Only change after clearing).")]
    public bool usePhysics = true;
    [Tooltip("Distance between segments and position selected with the mouse. Lowering it allows more precision. Increase it when you want to set the end anchor point. Dont go below than 0.01")]
    public float maxDistanceWithSelectedPos = 0.2f;
    [Tooltip("Separation between segments, lower it instance less segments. Dont go below than 0.01")]
    public float segmentsSeparation = 0.2f;
    [Tooltip("Prevents infinite segments from being instantiated in case of an error in the code.")]
    public int limitMax = 200;
    private int limit = 0;
    [Tooltip("A higher value improves the stability of the physics.")]
    public float segmentsRadius = 1.5f;
    public float currentDistanceToStartAnchor;
    [Tooltip("Sets the maximum distance from the start anchor point to the end anchor point, based on the number of segments and the separation between them.")]
    public float maxDistanceToStarAnchor;

    //public Transform name;



    [Header("SPAWNED SEGMENTS")]
    public List<Transform> segments;
    [HideInInspector]
    [Tooltip("You can delete these references when you are no longer modifying the wire.")]
    public List<int> undoSegments;
    private int undoCount = 0;

    [Header("REFERENCES")]
    public TubeRenderer ropeMesh;
    public Transform starAnchorTemp;
    public Transform firstSegment;
    public Transform endAnchorTemp;
    public Transform plugTemp;

    [Header("PREFABS")]
    public Transform startAnchorPoint;
    public Transform segment;
    public Transform segmentFirst;
    public Transform segmentNoPhysics;
    public Transform endAnchorPoint;
    public Transform plugObjt;


    bool iscalled = false;

    private void OnValidate()
    {
        ChangeRadius();
    }

    public void GetSegmentsDistance(Vector3 segmentPos)
    {
        
        int lastSegment = segments.Count - 1;
        float distance = Vector3.Distance(segments[lastSegment].position, segmentPos);

        
        if (distance >= maxDistanceWithSelectedPos + segmentsSeparation && limit <= limitMax)
        {

            limit++;
            if (usePhysics)
            {
                


                Transform newSegment = Instantiate(segment, segments[lastSegment].position + (segments[lastSegment].forward * segmentsSeparation), segments[lastSegment].rotation, transform);
                
                newSegment.GetComponent<ConfigurableJoint>().connectedBody = segments[lastSegment].GetComponent<Rigidbody>();

                segments.Add(newSegment);
            }
            else
            {
                


                Transform newSegment = Instantiate(segmentNoPhysics, segments[lastSegment].position + (segments[lastSegment].forward * segmentsSeparation), segments[lastSegment].rotation, transform);
                

                segments.Add(newSegment);
            }
            #region Undo
            undoCount++;
            #endregion


            GetSegmentsDistance(segmentPos);
            return;
        }



      
        SetMaxDistance();
    }

    public void AddStar(Vector3 startPos, Transform prefab)
    {


        if (starAnchorTemp == null)
        {
            

            starAnchorTemp = Instantiate(prefab, startPos, Quaternion.identity, transform);

            starAnchorTemp.parent = this.transform;

            segments.Add(starAnchorTemp);
            

        }

        //If you do not use physics, the components are removed to the start anchor point, to improve performance.
        if (!usePhysics)
        {
            DestroyImmediate(starAnchorTemp.GetComponent<FixedJoint>());
            DestroyImmediate(starAnchorTemp.GetComponent<Collider>());
            DestroyImmediate(starAnchorTemp.GetComponent<Rigidbody>());
        }
    }

    public void AddSegment(Vector3 segmentPos)
    {
        #region undo
        undoCount = 0;
        #endregion


        if (firstSegment == null)
        {


            if (usePhysics)
            {
                firstSegment = Instantiate(segment, starAnchorTemp.position, new Quaternion(-90, 0, 0, 0), transform);
                

                firstSegment.GetComponent<ConfigurableJoint>().connectedBody = starAnchorTemp.GetComponent<Rigidbody>();
                starAnchorTemp.GetComponent<ConfigurableJoint>().connectedBody = firstSegment.GetComponent<Rigidbody>();

                
            }
            else
            {

                firstSegment = Instantiate(segmentNoPhysics, starAnchorTemp.position, starAnchorTemp.rotation, transform);

              
            }

            segments.Add(firstSegment);

            #region undo
            undoCount++;
            #endregion
        }


        //The last current segment is rotated in the direction of selected position.
        int lastSegment = segments.Count - 1;
        segments[lastSegment].LookAt(segmentPos);



        //Segment is added based on the distance to the selected position.
        GetSegmentsDistance(segmentPos);
        RenderWireMesh();
        //StreamSegmentData();

        #region undo
        undoSegments.Add(undoCount);
        #endregion
    }

    public void AddEnd(Transform prefab=null)
    {

        //Adds the final anchor point.
        int lastSegment = segments.Count - 1;
        GameObject lastSeg = segments[lastSegment].gameObject;
        //lastSeg.layer = 10;
        Duplicate(segments[lastSegment - 1].gameObject, lastSeg);

        
        if (endAnchorTemp == null) {
            endAnchorTemp = Instantiate(prefab, lastSeg.transform.position, new Quaternion(-180f, 0, 0, 0), transform);
            //endAnchorTemp = PhotonNetwork.Instantiate(prefab.name, lastSeg.transform.position, new Quaternion(-180f, 0, 0, 0)).transform;

            endAnchorTemp.parent = this.transform;
        }

        //lastSeg.AddComponent<FixedJoint>().connectedBody = endAnchorTemp.GetComponent<Rigidbody>();


        endAnchorTemp.GetComponent<ConfigurableJoint>().connectedBody = lastSeg.GetComponent<Rigidbody>();
        lastSeg.GetComponent<ConfigurableJoint>().connectedBody = endAnchorTemp.GetComponent<Rigidbody>();

        segments.Add(endAnchorTemp);

        SampleController.Instance.Log(typeof(WireController)+"called the apply function");
        ApplyTransform();

        
    }

    void Duplicate(GameObject anchor, GameObject lastSeg)
    {
        ConfigurableJoint joint = anchor.AddComponent<ConfigurableJoint>();
        ConfigurableJoint segJoint = segment.GetComponent<ConfigurableJoint>();

        joint.connectedBody = lastSeg.GetComponent<Rigidbody>();
        joint.angularXMotion = ConfigurableJointMotion.Limited;
        joint.angularYMotion = ConfigurableJointMotion.Limited;
        joint.angularZMotion = ConfigurableJointMotion.Limited;

        joint.xMotion = ConfigurableJointMotion.Locked;
        joint.yMotion = ConfigurableJointMotion.Locked;
        joint.zMotion = ConfigurableJointMotion.Locked;

        joint.axis = segJoint.axis;
        joint.autoConfigureConnectedAnchor = false;
        joint.anchor = segJoint.anchor;
        joint.projectionAngle = segJoint.projectionAngle;
        joint.connectedAnchor = segJoint.connectedAnchor;
        joint.secondaryAxis = segJoint.secondaryAxis;

        joint.angularXLimitSpring = segJoint.angularXLimitSpring;
        joint.lowAngularXLimit = segJoint.lowAngularXLimit;
        joint.highAngularXLimit = segJoint.highAngularXLimit;
        joint.angularYZLimitSpring = segJoint.angularYZLimitSpring;
        joint.angularYLimit = segJoint.angularYLimit;
        joint.angularZLimit = segJoint.angularZLimit;
        joint.xDrive = segJoint.xDrive;
        joint.yDrive = segJoint.yDrive;
        joint.zDrive = segJoint.zDrive;



    }

    public void AddPlug(Vector3 plugPos)
    {
        //Instances the plug in the selected position.
        plugTemp = Instantiate(plugObjt, plugPos, plugObjt.transform.rotation, transform);
        PlugController plugScritp = plugTemp.GetComponent<PlugController>();

        plugScritp.endAnchor = endAnchorTemp;
        plugScritp.endAnchorRB = endAnchorTemp.GetComponent<Rigidbody>();
        //plugScritp.wireController = this;

    }

    public void SetMaxDistance()
    {
        maxDistanceToStarAnchor = segments.Count * segmentsSeparation;
    }

    public void ChangeRadius()
    {
        ///<summary>
        ///Modifies the radius of the sphere colliders of all instantiated segments.
        ///Increasing the radius usually improves the stability of the physics but makes the collisions less accurate in relation to the mesh.
        /// </summary>
        if (usePhysics)
            foreach (Transform segment in segments)
            {
                segment.GetComponent<SphereCollider>().radius = segmentsRadius;
            }
    }

    #region Buttons
    public void Clear()
    {
        //Destroy the segments.
        for (int i = 1; i < segments.Count; i++)
        {
            DestroyImmediate(segments[i].gameObject);
        }

        //Destroy the start anchor point.
        if (firstSegment != null)
            DestroyImmediate(firstSegment.gameObject);

        //Destroy the start anchor point.
        if (starAnchorTemp != null)
            DestroyImmediate(starAnchorTemp.gameObject);

        //Destroy the end anchor point.
        if (endAnchorTemp != null)
            DestroyImmediate(endAnchorTemp.gameObject);

        //Destroy the plug.
        if (plugTemp != null)
            DestroyImmediate(plugTemp.gameObject);


        //Clears the lists.
        segments.Clear();
        #region undo
        //Clear undo list.
        undoSegments.Clear();
        undoCount = 0;
        #endregion

        //Render wire
        RenderWireMesh();
        //StreamSegmentData();
        ClearWireMesh();

        //Reset limit
        limit = 0;
    }

    public void Undo()
    {
        //Destroy the end anchor point.
        if (endAnchorTemp != null)
            DestroyImmediate(endAnchorTemp.gameObject);

        //Undo the last segment creation.
        for (int i = 1; i <= undoSegments[undoSegments.Count - 1]; i++)
        {
            DestroyImmediate(segments[segments.Count - 1].gameObject);
            segments.Remove(segments[segments.Count - 1]);
        }
        undoSegments.RemoveAt(undoSegments.Count - 1);

        //The wire rendering is cleaned.
        if (undoSegments.Count == 0)
            ClearWireMesh();
        //Wire rendering updated
        RenderWireMesh();
    }

    public void ClearWireMesh()
    {
        /// <summary>
        /// When the TubeRender.cs position array is cleared the mesh render is not updated properly.
        /// For it to work properly you have to add 2 momentary positions in the array, that's what this function is for. 
        /// </summary>

        Vector3[] temp = new Vector3[]
        {
            new Vector3 (0,0,0),
            new Vector3 (0,0,0)
        };
        ropeMesh.SetPositions(temp);
    }

    public void FinishNoPhysicsWire()
    {
        /// <summary>
        /// Only when it is wire without physics.
        /// Only when you no longer want to modify the position of the segments.
        /// Eliminates segments and segment references to improve performance.
        /// </summary>>
        if (!usePhysics)
        {
            foreach (Transform segment in segments)
            {
                DestroyImmediate(segment.gameObject);
            }
            segments.Clear();
            undoSegments.Clear();
        }
        else
        {
            Debug.LogWarning("only use in no-physics wires and when you don't want to modify them anymore.");
        }
    }
    #endregion


    private void ApplyTransform()
    {
        States currentState = TransformMenu.CurrentState;
        SampleController.Instance.Log(typeof(WireController) + ": Current transform state is " + currentState);
        WireTransform wireTransform = starAnchorTemp.GetComponent<WireTransform>();
        WireTransform wireTransform2 = endAnchorTemp.GetComponent<WireTransform>();

        if (wireTransform != null && wireTransform2!=null)
        {
            switch (currentState)
            {
                case States.Rack:
                    wireTransform.ApplyRackTransform();
                    wireTransform2.ApplyRackTransform();
                    break;

                case States.Server:
                    wireTransform.ApplyServerTransform();
                    wireTransform2.ApplyServerTransform();
                    break;

                case States.Wire:
                    wireTransform.ApplyWireTransform();
                    wireTransform2.ApplyWireTransform();
                    break;
            }
        }
    }


    private void Update()
    {


        if (usePhysics)
        {
            
            RenderWireMesh();
        }
    }

    public void DistanceBetweenStartAndEnd()
    {
        currentDistanceToStartAnchor = Vector3.Distance(endAnchorTemp.position, starAnchorTemp.position);

        if (currentDistanceToStartAnchor > maxDistanceToStarAnchor)
        {
            /// <summary>
            /// Call a function when the distance between the start anchor point and the End anchor point exceeds the maximum.
            /// Example: do not let the wire rope move any further.
            /// </summary>>
        }
    }

    public void RenderWireMesh()
    {
        /// <summary>
        /// For more wire render settings see TubeRender.cs.
        /// </summary>

        //Render the wire.
        List<Vector3> tempPos = new List<Vector3>();
        foreach (Transform pos in segments)
        {
            tempPos.Add(pos.localPosition);
        }
        ropeMesh.SetPositions(tempPos.ToArray());
    }




    }

