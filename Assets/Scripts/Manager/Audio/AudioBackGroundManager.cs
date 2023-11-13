using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioBackGroundManager : MonoBehaviour
{
    public static AudioBackGroundManager instance;

    SwitchBackgroundTrack switchBackgroundTrack;

    public Sounds[] sounds;


    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;

        }
        DontDestroyOnLoad(gameObject);

        switchBackgroundTrack = FindAnyObjectByType<SwitchBackgroundTrack>();
        CreateSounds();
        if (switchBackgroundTrack != null)
        {
            return;
        }
        Play("Background");
    }


    void CreateSounds()
    {
        foreach (Sounds sound in sounds)
        {
            sound.audioSource = gameObject.AddComponent<AudioSource>();
            sound.audioSource.outputAudioMixerGroup = sound.audioMixer;
            sound.audioSource.clip = sound.audioClip;
            sound.audioSource.volume = sound.volume;
            sound.audioSource.pitch = sound.pitch;
            sound.audioSource.loop = sound.loop;
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

    public void ChangeAudioClip(string name, AudioClip audioClip)
    {
        Sounds s = Array.Find(sounds, sounds => sounds.soundName == name);
        if (s == null)
        {
            return;
        }
        s.audioClip = audioClip;
        s.audioSource.clip = s.audioClip;
        s.audioSource.Play();

    }

}
