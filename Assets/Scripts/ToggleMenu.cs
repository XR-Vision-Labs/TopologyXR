using UnityEngine;

public class ToggleMenu : MonoBehaviour
{
    public Transform menu;
    public Transform leftHand;
    public Transform camera;

    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.Start))
        {
            ToggleMenufn();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            ToggleMenufn();
        }
    }

    

    public void ToggleMenufn()
    {
        //SampleController.Instance.Log("Menu button pressed");

        menu.gameObject.SetActive(!menu.gameObject.activeSelf);
        menu.transform.position = leftHand.position;
        //menu.transform.rotation = Quaternion.identity;

        // Calculate the direction from the menu to the camera
        Vector3 directionToFace = camera.position - menu.transform.position;

        // Make the menu face the camera by adjusting its rotation
        // This ensures the front of the menu is always facing the user
        // Ignore the vertical difference by setting y component to 0 if you want the menu to only rotate horizontally
        directionToFace.y = 0; // Optional: remove this line if you want the menu to tilt up/down as well
        menu.transform.rotation = Quaternion.LookRotation(directionToFace);
    }
}
