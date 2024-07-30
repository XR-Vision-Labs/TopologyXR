using UnityEngine;

using TMPro;


public class SharedAnchorControlPanel : MonoBehaviour
{
    


    [SerializeField]
    private TextMeshProUGUI pageText;


    public void LogNext()
    {
        if (SampleController.Instance.logText.pageToDisplay >= SampleController.Instance.logText.textInfo.pageCount)
        {
            return;
        }

        SampleController.Instance.logText.pageToDisplay++;
        pageText.text = SampleController.Instance.logText.pageToDisplay + "/" + SampleController.Instance.logText.textInfo.pageCount;
    }

    public void LogPrev()
    {
        if (SampleController.Instance.logText.pageToDisplay <= 1)
        {
            return;
        }

        SampleController.Instance.logText.pageToDisplay--;
        pageText.text = SampleController.Instance.logText.pageToDisplay + "/" + SampleController.Instance.logText.textInfo.pageCount;
    }



    
    
}
