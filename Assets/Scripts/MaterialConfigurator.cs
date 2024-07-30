using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialConfigurator : MonoBehaviour
{
    public Shader shader;

    private void Start()
    {
        Debug.Log("Material configure fn called");
        ConfigureMat();
        Debug.Log("Material configured");
    }

    private void ConfigureMat()
    {
        Material mat = GetComponent<MeshRenderer>().material;
        mat.shader = shader;
        
    }
}
