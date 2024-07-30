using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerAudioManager : MonoBehaviour
{
    public List<AudioClip> clips;
    private AudioSource source;
    private bool isPlaying = false;


    private void Start()
    {
        source = GetComponent<AudioSource>();
        if (source != null)
            Debug.Log("Got the audio source");
    }

    public void TogglePlay()
    {
        if (isPlaying)
        {
            isPlaying = false;
            PlayEnd();
        }
        else
        {
            isPlaying = true;
            PlayStart();
        }
    }

    public void PlayStart()
    {
        StartPlay();
    }

    private void StartPlay()
    {
        if (source != null)
        {
            SampleController.Instance.Log("Playing start audio");
            source.PlayOneShot(clips[0]);

            //yield return null;
            //yield return new WaitForSeconds(source.clip.length);

            SampleController.Instance.Log("Start audio completed playing now calling repeat method");
            PlayRepeat();
        }
    }

    private void PlayRepeat()
    {
        if (source != null)
        {
            SampleController.Instance.Log("Playing Repeat audio");

            source.loop = true;
            source.clip = clips[1];
            source.Play();
        }
    }

    public void PlayEnd()
    {
        if (source != null)
        {
            SampleController.Instance.Log("Playing End audio");

            if (source.isPlaying)
                source.Stop();

            source.loop = false;
            source.PlayOneShot(clips[2]);

        }
    }
}
