using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchBackgroundTrack : MonoBehaviour
{
    public AudioClip audioClip;

    private void Start()
    {
        AudioBackGroundManager.instance.PlayBackground(audioClip);
    }
}
