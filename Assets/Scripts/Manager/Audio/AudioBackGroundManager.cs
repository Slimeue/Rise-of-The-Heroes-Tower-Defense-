using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioBackGroundManager : MonoBehaviour
{
    public static AudioBackGroundManager instance;

    AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;

        }
        DontDestroyOnLoad(gameObject);
    }

    public void PlayBackground(AudioClip audioClip)
    {
        audioSource.Stop();
        audioSource.clip = audioClip;
        audioSource.Play();
    }

}
