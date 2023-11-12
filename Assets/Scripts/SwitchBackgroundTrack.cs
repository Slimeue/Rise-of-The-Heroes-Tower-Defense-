using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchBackgroundTrack : MonoBehaviour
{
    public string audioClipName;
    public AudioClip audioClip;

    private void Start()
    {
        AudioBackGroundManager.instance.ChangeAudioClip(audioClipName, audioClip);
    }
}
