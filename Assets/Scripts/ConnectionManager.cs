using Oculus.Interaction;
using UnityEngine;

public class ConnectionManager : MonoBehaviour
{
    public short nConnections = 0;
    public short compareNumbers = 2;
    public PokeInteractable power_button;
    public RayInteractable ray_interactable;

    public GameObject lights;
    public ServerAudioManager audioManager;
    private bool isServerOn = false;
    private FlickerLight flickerLight;

    public void OnConnected()
    {

        nConnections++;
        SampleController.Instance.Log("Connection done, total connections " + nConnections);

        if (IsReady())
        {
            // perform the work  here and server is able to get on
            // enable the button
            SampleController.Instance.Log("Server is ready");
            ray_interactable.enabled = true;
            power_button.enabled = true;
        }
        else
        {
            SampleController.Instance.Log("Server not ready");
        }
    }

    public void OnDisconnected()
    {
        nConnections--;
        SampleController.Instance.Log("Disconnection done, total connections " + nConnections);

        ray_interactable.enabled = false;
        power_button.enabled = false;
        OnServerStop();

    }

    private bool IsReady()
    {
        if (nConnections == compareNumbers)
        {
            SampleController.Instance.Log("Server ready, returning true");
            return true;
        }

        return false;
    }

    public void ToggleButton()
    {
        isServerOn = !isServerOn;

        SampleController.Instance.Log("Server is set to " + isServerOn);

        if (isServerOn)
        {
            SampleController.Instance.Log("On server start function called");

            OnServerStart();
        }
        else
        {
            SampleController.Instance.Log("On server stop function called");

            OnServerStop();
        }
    }

    public void OnServerStart()
    {
        lights.SetActive(true);
        flickerLight = lights.GetComponent<FlickerLight>();
        flickerLight.StartFlickering();

        audioManager.PlayStart();
    }

    public void OnServerStop()
    {
        lights.SetActive(false);
        if (flickerLight == null)
            lights.GetComponent<FlickerLight>().StartFlickering();
        else
        {
            flickerLight.StopFlickering();
        }
        audioManager.PlayEnd();
    }
}
