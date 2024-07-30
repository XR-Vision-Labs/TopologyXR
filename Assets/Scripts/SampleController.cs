
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using Oculus.Interaction;
using System.Collections.Generic;
using UnityEngine.UI;

/// <summary>
/// Main manager for sample interaction
/// </summary>
public class SampleController : MonoBehaviour
{
    //public bool automaticCoLocation = false;
    public bool cachedAnchorSample = false;

    //[HideInInspector]
    //public SharedAnchor colocationAnchor;

    //[HideInInspector]
    //public CachedSharedAnchor colocationCachedAnchor;

    [SerializeField]
    private Transform rightHandAnchor;

    //[SerializeField]
    //private GameObject placementPreview;

    [SerializeField]
    private Transform placementRoot;

    [SerializeField]
    public TextMeshProUGUI logText;

    [SerializeField]
    public TextMeshProUGUI pageText;


    [SerializeField]
    public OVRSpatialAnchor anchorPrefab;

    [SerializeField]
    private Transform grabbableCube;

    private Transform instantiatedPlacement;

    public static SampleController Instance;
    private bool _isPlacementMode;

    //private List<SharedAnchor> sharedanchorList = new List<SharedAnchor>();

    private RayInteractor _rayInteractor;

    public Transform RightHandAnchor
    {
        get
        {
            return rightHandAnchor;
        }
    }

    //private void Start()
    //{
    //    PlaceAnchorAtRoot(rightHandAnchor);
    //}

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

        //gameObject.AddComponent<SharedAnchorLoader>();

        //placementPreview.transform.parent = rightHandAnchor;
        //placementPreview.transform.localPosition = Vector3.zero;
        //placementPreview.transform.localRotation = Quaternion.identity;
        //placementPreview.transform.localScale = Vector3.one;
        //placementPreview.SetActive(false);
        _rayInteractor = FindObjectOfType<RayInteractor>();
    }

    //private void Update()
    //{
    //    var rayInteractorHoveringUI = _rayInteractor == null || (_rayInteractor != null && _rayInteractor.Candidate == null);
    //    var shouldPlaceNewAnchor = _isPlacementMode && OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger) && rayInteractorHoveringUI;

    //    if (shouldPlaceNewAnchor)
    //    {
    //        PlaceAnchorAtRoot();
    //    }
    //}

    private void Update()
    {

        //if (Input.GetKeyDown(KeyCode.Alpha1))
        //    PlaceAnchorAtRoot(rightHandAnchor);
    }


    public void CreateLogoCube()
    {
        instantiatedPlacement = Instantiate(grabbableCube, rightHandAnchor.position, rightHandAnchor.rotation);

        var lpo = instantiatedPlacement.GetComponent<LogoPlacementObject>();
        //lpo.GetSaveButton().onClick.AddListener(PlaceAnchorAtRoot);

        lpo.SampleController = this;
    }

    //public void StartPlacementMode()
    //{
    //    _isPlacementMode = true;
    //    placementPreview.SetActive(true);
    //}

    //public void EndPlacementMode()
    //{

    //    _isPlacementMode = false;
    //    placementPreview.SetActive(false);
    //}

    //public void PlaceAnchorAtRoot()
    //{
    //    //Log("PlaceAnchorAtRoot: root: " + instantiatedPlacement.ToOVRPose().ToPosef());

    //    colocationAnchor = Instantiate(anchorPrefab, rightHandAnchor.position, rightHandAnchor.rotation).GetComponent<SharedAnchor>();

    //    colocationAnchor.OnSaveLocalButtonPressed();

    //    //colocationAnchor.OnShareButtonPressed();


    //    //if (automaticCoLocation)
    //    //    StartCoroutine(WaitingForAnchorLocalization());
    //}

    public void PlaceAnchorAtRoot(Transform placementCubeTransform)
    {
        //Log("PlaceAnchorAtRoot: root: " + placementCubeTransform.ToOVRPose().ToPosef());

        //colocationAnchor = Instantiate(anchorPrefab, placementCubeTransform.position, placementCubeTransform.rotation).GetComponent<SharedAnchor>();

        //colocationAnchor.OnSaveLocalButtonPressed();
        //Log("Anchor saved");

        //colocationAnchor.OnShareButtonPressed();
        //Log("Anchor shared");

        //StartCoroutine(WaitingForAnchorLocalization());

        //Log("Anchor saved and shared and aligned, Cube destroyed");
        //Destroy(placementCubeTransform.gameObject);

        //if (automaticCoLocation)
        //    StartCoroutine(WaitingForAnchorLocalization());
    }

    public void ShareAnchor()
    {
        //if (colocationAnchor != null)
        //{
        //    colocationAnchor.OnShareButtonPressed();
        //}
    }


    public void AlignWithAnchor()
    {
        //if (colocationAnchor != null)
        //{
        //    colocationAnchor.OnAlignButtonPressed();
        //}
    }

    //private System.Collections.IEnumerator WaitingForAnchorLocalization()
    //{
    //    //while (!colocationAnchor.GetComponent<OVRSpatialAnchor>().Localized)
    //    //{
    //    //    Log(nameof(WaitingForAnchorLocalization) + "...");
    //    //    yield return null;
    //    //}

    //    //Log($"{nameof(WaitingForAnchorLocalization)}: Anchor Localized");
    //    //colocationAnchor.OnAlignButtonPressed();
    //}

    public void Log(string message, bool error = false)
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

        logText.text = SampleController.Instance.logText.text + "\n" + message;
        //Debug.Log(logText.pageToDisplay);
        //Debug.Log(SampleController.Instance.logText.textInfo.pageCount);

        logText.pageToDisplay = SampleController.Instance.logText.textInfo.pageCount;

        // Console logging (goes to logcat on device)

        const string anchorTag = "SpatialAnchorsUnity: ";
        if (error)
            Debug.LogError(anchorTag + message);
        else
            Debug.Log(anchorTag + message);

        pageText.text = SampleController.Instance.logText.pageToDisplay + "/" + logText.textInfo.pageCount;
    }

    //public void Clear()
    //{
    //    logText.text = "";
    //}

    public void LogError(string message)
    {
        Log(message, true);
    }

    //public void AddSharedAnchorToLocalPlayer(SharedAnchor anchor)
    //{
    //    sharedanchorList.Add(anchor);
    //}

    //public List<SharedAnchor> GetLocalPlayerSharedAnchors()
    //{
    //    return sharedanchorList;
    //}
}
