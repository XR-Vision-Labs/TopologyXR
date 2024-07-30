using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Parabox.CSG.Demo
{


    public class CarveOut : MonoBehaviour
    {

        private GameObject right, left;

        private List<GameObject> obstacles;

        bool isFunctioncalled = false;

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("Trigger enter");


            //SampleController.Instance.Clear();

            SampleController.Instance.Log($"Trigger enter with {other.tag}");


            if (other.gameObject.CompareTag("wall") && !isFunctioncalled)
            {

                left = this.gameObject;
                SampleController.Instance.Log(left.gameObject.name);
                Debug.Log(left.gameObject.name);


                right = other.gameObject;
                SampleController.Instance.Log(right.gameObject.name);
                Debug.Log(right.gameObject.name);

                isFunctioncalled = true;
                //yield return new WaitForEndOfFrame();
                //StartCoroutine(DoBooleanOperation());
                DoBooleanOperation();

            }
        }

        public void DoBooleanOperation()
        {
            SampleController.Instance.Log("Carving in progress");


            //yield return new WaitForEndOfFrame();

            Model result;

            Debug.Log("Calling out subtract method");

            SampleController.Instance.Log("Calling out subtract method");

            result = CSG.Subtract(right, left);

            Debug.Log($"subtraction done result is {result}");

            Debug.Log("Destroy the gameobject");
            Destroy(right.gameObject);
            Destroy(left.gameObject);

            //yield return null;A
            configurMesh(result);
            //StartCoroutine(configurMesh(result));

            //CarveManager.Instance.EndCarveOperation(this.gameObject);
        }

        void configurMesh(Model result)
        {
            //SampleController.Instance.Log($"subtraction done result is {result}");
            GameObject composite = new GameObject(name: "ResultantMesh");

            composite.AddComponent<MeshFilter>().sharedMesh = result.mesh;
            composite.AddComponent<MeshRenderer>().sharedMaterials = result.materials.ToArray();

            composite.AddComponent<BoxCollider>();
            //composite.AddComponent<Rigidbody>().isKinematic = true;
            //composite.AddComponent<CarveOut>();
            composite.AddComponent<OVRSceneAnchor>();
            composite.gameObject.tag = "wall";


            //yield return null;
            //CarveManager.Instance.IsProcessInProgress(false);

            CarveManager.Instance.CarveNext();
            SampleController.Instance.Log("Carving done set progress to false");


            //yield return null;
            SampleController.Instance.Log("Carving done");
            //canCallBoolean = true;

        }

        //private void OnTriggerEnter(Collider other)
        //{
        //    SampleController.Instance.Log("Trigger enter with " + other.tag);

        //    if(other.gameObject.CompareTag("obstacle") && !obstacles.Contains(other.gameObject))
        //    {
        //        obstacles.Add(other.gameObject);
        //        SampleController.Instance.Log("")
        //    }
        //}


    }

}


