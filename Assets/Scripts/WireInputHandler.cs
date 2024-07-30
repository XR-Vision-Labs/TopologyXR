using System;
using TMPro;
using UnityEngine;

public class WireInputHandler : MonoBehaviour
{
    //public TMP_InputField inputField;
    public WireGenerationHandler wireGenerationHandler;
    //public TextMeshProUGUI logText;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            
            GetInput();
        }
    }

    public void GetInput()
    {
        //var value = inputField.text;
        float distance;

        try
        {
            //distance = float.Parse(value);
            distance = float.Parse("1");
            Debug.Log($"Called BuildWire function with value {distance}");
            //wireGenerationHandler.BuildWire(distance);
        }
        catch (Exception e)
        {
            //logText.text = e.Message;
            Debug.Log(e.Message);
        }
    }
}
