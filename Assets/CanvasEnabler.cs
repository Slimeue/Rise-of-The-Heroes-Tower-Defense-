using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasEnabler : MonoBehaviour
{
    [SerializeField] public GameObject _platformHighlight;

    public void EnableCanvas()
    {
        _platformHighlight.SetActive(true);
    }

    public void DisableCanvas()
    {
        _platformHighlight.SetActive(false);
    }

}
