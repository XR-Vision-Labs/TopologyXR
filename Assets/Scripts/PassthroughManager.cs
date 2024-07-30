using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class PassthroughManager : MonoBehaviour
{
    public Camera centerEye;
    public GameObject shadowCaster;

    public static PassthroughManager instance;
    public OVRSceneManager sceneManager;

    private UniversalAdditionalCameraData cameraData;
    private OVRManager ovrManager;
    private static bool isPassthroughEnable=false;

    public static event Action togglePassthrough;

    //public WorldGenerationController worldGeneration;

    private void Start()
    {
        cameraData = centerEye.GetComponent<UniversalAdditionalCameraData>();

        if (cameraData == null)
        {
            Debug.LogError("UniversalAdditionalCameraData not found on the camera. Make sure you're using URP.");
            return;
        }

        ovrManager = GameObject.Find("Start Rig").GetComponent<OVRManager>();

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }

        sceneManager.SceneModelLoadedSuccessfully += DisablePassthrough;

        //worldGeneration = FindObjectOfType<WorldGenerationController>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            TogglePassthrough();
        }
    }

    public void TogglePassthrough()
    {
        if (isPassthroughEnable)
        {
            DisablePassthrough();
        }
        else
        {
            EnablePassthrough();
        }

        togglePassthrough.Invoke();
    }

    public void EnablePassthrough()
    {
        isPassthroughEnable = true;
        //SampleController.Instance.Log("Enable passthrough function inside");

        PerformPassthroughOp(true);
        //SampleController.Instance.Log("Perform Passthrough function done");

        PassthroughCamConf();

        //SampleController.Instance.Log("Camera settings for passthrough done");
    }

    public void DisablePassthrough()
    {
        isPassthroughEnable = false;
        //SampleController.Instance.Log("disable passthrough function inside");
        PerformPassthroughOp(false);
        //SampleController.Instance.Log("Perform Passthrough function done");

        NormalCamConf();
        //SampleController.Instance.Log("Camera settings for non passthrough done");
    }

    private void PerformPassthroughOp(bool val)
    {
        
        if (ovrManager != null)
        {
            //if (OVRManager.IsInsightPassthroughSupported())
            ovrManager.isInsightPassthroughEnabled = val;

            //SampleController.Instance.Clear();
            //SampleController.Instance.Log("Setting passthrough to "+val);
        }
    }

    private void PassthroughCamConf()
    {
        if (ovrManager != null && cameraData!=null && ovrManager.isInsightPassthroughEnabled)
        {
            //SampleController.Instance.Log("Configuring camera for passthrough");
            shadowCaster.SetActive(false);

            cameraData.renderPostProcessing = false;
            centerEye.clearFlags = CameraClearFlags.SolidColor;
        }
    }

    private void NormalCamConf()
    {
        if (ovrManager!=null && !ovrManager.isInsightPassthroughEnabled)
        {
            //SampleController.Instance.Log("Invoking passthrough disable funciton events");

            shadowCaster.SetActive(true);
            //onPassthroughDiable.Invoke();

            if (cameraData != null)
            {
                //SampleController.Instance.Log("configuring camera");
                cameraData.renderPostProcessing = true;
                centerEye.clearFlags = CameraClearFlags.Skybox;
            }
        }
    }
}
