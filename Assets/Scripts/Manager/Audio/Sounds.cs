using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


[System.Serializable]
public class Sounds
{
    public AudioClip audioClip;

    public string soundName;
    [Range(0f, 1f)]
    public float volume;
    [Range(.1f, 3f)]
    public float pitch;

    public bool loop;
    public bool playOneShot;

    [HideInInspector]
    public AudioSource audioSource;
}
