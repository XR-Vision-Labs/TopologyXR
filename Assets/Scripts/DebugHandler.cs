using TMPro;
using UnityEngine;

public class DebugHandler : MonoBehaviour
{
   
    [SerializeField]
    private Transform rightHandAnchor;

    [SerializeField]
    public TextMeshProUGUI logText;

    [SerializeField]
    public TextMeshProUGUI pageText;

    public static DebugHandler Instance;
   
   
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
   
    public void Log(string message)
    {
        // In VR Logging

        if (logText == null && logText.isActiveAndEnabled)
        {
            Debug.Log("Log text is empty");
            return;
        }
        else
        {
            Debug.Log("Log text is not empty");
        }

        logText.text = logText.text + "\n" + message;
       
        logText.pageToDisplay = logText.textInfo.pageCount;

        // Console logging (goes to logcat on device)

        const string anchorTag = "SpatialAnchorsUnity: ";
        
        Debug.Log(anchorTag + message);

        pageText.text = logText.pageToDisplay + "/" + logText.textInfo.pageCount;
    }

    public void LogNext()
    {
        if (logText.pageToDisplay >= logText.textInfo.pageCount)
        {
            return;
        }

        logText.pageToDisplay++;
        pageText.text = logText.pageToDisplay + "/" + logText.textInfo.pageCount;
    }

    public void LogPrev()
    {
        if (logText.pageToDisplay <= 1)
        {
            return;
        }
        Debug.Log("Log prev pressed");
        logText.pageToDisplay--;
        pageText.text = logText.pageToDisplay + "/" + logText.textInfo.pageCount;
    }

}
