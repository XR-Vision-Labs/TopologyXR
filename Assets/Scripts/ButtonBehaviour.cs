using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ButtonBehaviour : MonoBehaviour
{
    public RoundedBoxProperties _property;
    public ButtonColor btncolor;
    public TextMeshPro _label;
    public bool isLabel = true;

    private static bool isPressed = false;

    void Start()
    {
        _property = GetComponentInChildren<RoundedBoxProperties>();
        if (_property == null)
        {
            Debug.Log("Did not Got the rounded property");
        }
    }

    public void SetColor(bool isSelected)
    {
        if (_property == null)
        {
            Debug.Log("Didnt get rounded property so returning");
            return;
        }

        if (isSelected)
        {
            Debug.Log("Changing color to selected one");
            var selectColor = new MaterialPropertyColor();
            selectColor.name = "_Color";
            selectColor.value = btncolor.selectColor;

            List<MaterialPropertyColor> color = new List<MaterialPropertyColor>();
            color.Add(selectColor);

            _property.GetComponent<MaterialPropertyBlockEditor>().ColorProperties = color;

            if(isLabel)
            _label.color = Color.black;
        }
        else
        {
            Debug.Log("Changing color to unselected one");
            var selectColor = new MaterialPropertyColor();
            selectColor.name = "_Color";
            selectColor.value = btncolor.normalColor;

            List<MaterialPropertyColor> color = new List<MaterialPropertyColor>();
            color.Add(selectColor);

            _property.GetComponent<MaterialPropertyBlockEditor>().ColorProperties = color;
            if (isLabel)
                _label.color = Color.white;
        }
    }


    public void ToggleButton()
    {
        isPressed = !isPressed;
        SetColor(isPressed);
    }

}
