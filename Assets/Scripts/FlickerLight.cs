using System.Collections;
using UnityEngine;

public class FlickerLight : MonoBehaviour
{
    public GameObject[] lights = new GameObject[5];
    public AudioSource source;
    public AudioClip beepClip;
    public GameObject continuousFlickerLight;
    public float minTime = 0.1f;
    public float maxTime = 0.5f;
    public bool playSound = true;

    private Coroutine continuousCoroutine;
    private Coroutine randomCoroutine;
    private bool isFlickering = false;

    //private void Start()
    //{
    //    StartFlickering();
    //}
    public void ToggleFlickering()
    {
        if(isFlickering)
        {
            StopFlickering();
            isFlickering = false;
        }
        else
        {
            StartFlickering();
            isFlickering=true;
        }
    }

    public void StartFlickering()
    {
        // Start the coroutines and store the references
        randomCoroutine = StartCoroutine(FlickerRandomLights());
        continuousCoroutine = StartCoroutine(FlickerContinuousLight());
    }

    public void StopFlickering()
    {
        // Stop the coroutines using the stored references
        if (continuousCoroutine != null)
        {
            StopCoroutine(continuousCoroutine);
        }
        if (randomCoroutine != null)
        {
            StopCoroutine(randomCoroutine);
        }
        // Additionally, ensure all lights are turned on when stopping the flicker
        foreach (var light in lights)
        {
            light.SetActive(false);
        }
        continuousFlickerLight.SetActive(false);
    }

    private IEnumerator FlickerContinuousLight()
    {
        while (true)
        {
            continuousFlickerLight.SetActive(!continuousFlickerLight.activeSelf);

            if (continuousFlickerLight.activeSelf && playSound)
            {
                source.PlayOneShot(beepClip);
            }

            yield return new WaitForSeconds(Random.Range(minTime, maxTime));
        }
    }

    private IEnumerator FlickerRandomLights()
    {
        while (true)
        {
            int index1 = Random.Range(0, lights.Length);
            int index2;
            do
            {
                index2 = Random.Range(0, lights.Length);
            } while (index1 == index2);

            lights[index1].SetActive(false);
            lights[index2].SetActive(false);

            yield return new WaitForSeconds(Random.Range(minTime, maxTime));

            lights[index1].SetActive(true);
            lights[index2].SetActive(true);

            // Play sound for each light if they are different
            if (index1 != index2&& playSound)
            {
                source.PlayOneShot(beepClip);
            }

            if(playSound)
            source.PlayOneShot(beepClip);

            yield return new WaitForSeconds(Random.Range(minTime, maxTime));
        }
    }
}
