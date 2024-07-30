using Dummiesman;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using Oculus.Interaction;

public class OBJ : MonoBehaviour
{
    public string objURL = "https://xrvisionlabs.com/wp-content/realtime/310.obj";
    private Vector3 spawnPosition = new Vector3(-4.3f, 0.2f, 9);
    private Vector3 scale = new Vector3(0.05f, 0.05f, 0.05f);
    private int numberOfInstances = 10;
    private GameObject modelPrefab;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B) || OVRInput.GetDown(OVRInput.Button.One))
        {
            LoadModelFromURL();
        }
    }

    public void LoadModelFromURL()
    {
        SampleController.Instance.Log("Button clicked");
        SampleController.Instance.Log("" + objURL);
        StartCoroutine(DownloadandLoadModel());
    }

    private IEnumerator DownloadandLoadModel()
    {
        string assetPath = Path.Combine(Application.persistentDataPath, Path.GetFileName(objURL));
        if (!File.Exists(assetPath))
        {
            using (UnityWebRequest www = UnityWebRequest.Get(objURL))
            {
                yield return www.SendWebRequest();

                if (www.result != UnityWebRequest.Result.Success)
                {
                    Debug.LogError("Failed to download model: " + www.error);
                    yield break;
                }
                SampleController.Instance.Log("Model downloaded successfully at: " + assetPath);
                File.WriteAllBytes(assetPath, www.downloadHandler.data);
                SampleController.Instance.Log("Model downloaded successfully at: " + assetPath);
            }
            //byte[] downloadedData = System.IO.File.ReadAllBytes(assetPath);
            //string dataString = System.Text.Encoding.UTF8.GetString(downloadedData);
            //SampleController.Instance.Log("Data at path " + assetPath + ": " + dataString);
        }
        else
        {
            Debug.Log("Model already exists offline at: " + assetPath);
            SampleController.Instance.Log("Model already exists at: " + assetPath);
        }

        if (!File.Exists(assetPath))
        {
            Debug.LogError("Model file does not exist at: " + assetPath);
            SampleController.Instance.Log("Model does not exist at: " + assetPath);
            StopCoroutine(DownloadandLoadModel());
        }

        if (modelPrefab != null)
        {
            for (int i = 0; i < numberOfInstances + 1; i++)
            {
                GameObject modelInstance = Instantiate(modelPrefab, spawnPosition, Quaternion.identity);
                SampleController.Instance.Log("Model Load Successfull");
            }
        }

        else
        {
            modelPrefab = new OBJLoader().Load(assetPath);
            modelPrefab.transform.localScale = scale;
            modelPrefab.transform.position = spawnPosition;
            AddComponents(modelPrefab);

            for (int i = 0; i < numberOfInstances; i++)
            {
                GameObject modelInstance = Instantiate(modelPrefab, spawnPosition, Quaternion.identity);
                SampleController.Instance.Log("Model Load Successfull");
            }

        }
    }

    private void AddComponents(GameObject obj)
    {
        if (obj == null)
        {
            Debug.LogError("GameObject is null. Cannot add components.");
            return;
        }

        // Add Rigidbody component
        Rigidbody rb = obj.AddComponent<Rigidbody>();
        rb.mass = 1f;

        // Add BoxCollider component
        BoxCollider boxCollider = obj.AddComponent<BoxCollider>();

        // Check if Renderer component is present
        Renderer renderer = obj.GetComponent<Renderer>();
        if (renderer != null)
        {
            // Add BoxCollider size based on Renderer bounds
            boxCollider.size = renderer.bounds.size;
        }
        else
        {
            Debug.LogWarning("No Renderer component found on the loaded object.");
            // Set default BoxCollider size if Renderer component is missing
            boxCollider.size = new Vector3(11.2f, 2.9f, 18.4f);
        }

        // Add OVRGrabbable component
        Grabbable grabbable = obj.AddComponent<Grabbable>();

        // Set OVRGrabbable component as active
        if (grabbable != null)
        {
            grabbable.enabled = true; // Ensure the component is enabled
        }
        else
        {
            Debug.LogWarning("Failed to add OVRGrabbable component.");
        }
        Debug.Log("Components added");
        SampleController.Instance.Log("Components added");
    }
}
