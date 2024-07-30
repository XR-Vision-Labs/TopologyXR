using System.IO;
using System.Text;
using UnityEngine;
using Dummiesman;
using System.Collections;

public class ObjFromWeb : MonoBehaviour
{
    private string objURL = "https://xrvisionlabs.com/wp-content/realtime/310.obj"; // Variable to hold the URL input

    private void Start(){
        Debug.Log("Load obj fn called");
        LoadOBJFromURL(objURL);

        Debug.Log("Loaded model");
    }

    void OnGUI()
    {
        // Create a text field for entering the URL
        objURL = GUI.TextField(new Rect(10, 10, 200, 20), objURL);

        // Create a button to trigger loading the OBJ file
        if (GUI.Button(new Rect(10, 40, 100, 20), "Load OBJ"))
        {
            LoadOBJFromURL(objURL);
        }
    }

    void LoadOBJFromURL(string url)
    {
        StartCoroutine(LoadOBJCoroutine(url));
    }

    IEnumerator LoadOBJCoroutine(string url)
    {
        // Make www
        var www = new WWW(url);
        yield return www;

        // Check for errors
        if (!string.IsNullOrEmpty(www.error))
        {
            Debug.LogError("Failed to load OBJ file: " + www.error);
            yield break;
        }

        // Create stream and load
        var textStream = new MemoryStream(Encoding.UTF8.GetBytes(www.text));
        var loadedObj = new Dummiesman.OBJLoader().Load(textStream);

        // Do whatever you need to do with the loadedObj
    }
}
