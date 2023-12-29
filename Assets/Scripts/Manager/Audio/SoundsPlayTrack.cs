using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsPlayTrack : MonoBehaviour
{
    public Sounds[] sounds;

    private void Awake()
    {
        foreach (Sounds sound in sounds)
        {
            sound.audioSource = gameObject.AddComponent<AudioSource>();
            sound.audioSource.outputAudioMixerGroup = sound.audioMixer;
            sound.audioSource.clip = sound.audioClip;
            sound.audioSource.volume = sound.volume;
            sound.audioSource.pitch = sound.pitch;
            sound.audioSource.spatialBlend = sound.spatialBlend;
            sound.audioSource.loop = sound.loop;
            sound.audioSource.playOnAwake = sound.playOnAwake;


        }
    }


    public void Play(string name)
    {
        Sounds s = Array.Find(sounds, sound => sound.soundName == name);
        if (s == null)
        {
            return;
        }
        if (s.playOneShot)
        {
            s.audioSource.PlayOneShot(s.audioClip);
            return;
        }
        s.audioSource.Play();
    }


    public void StopPlay(string name)
    {
        Sounds s = Array.Find(sounds, sound => sound.soundName == name);
        if (s == null)
        {
            return;
        }
        s.audioSource.Stop();
    }

}
