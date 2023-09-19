using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasEnabler : MonoBehaviour
{
    [SerializeField] public GameObject _platformHighlight;
    [SerializeField] Color notAvailColor;
    [SerializeField] Color availColor;

    public void EnableCanvas()
    {
        _platformHighlight.SetActive(true);
    }

    public void NotAvailColor()
    {
        RawImage color = _platformHighlight.GetComponentInChildren<RawImage>();
        color.color = notAvailColor;
    }
    public void AvailColor()
    {
        RawImage color = _platformHighlight.GetComponentInChildren<RawImage>();
        color.color = availColor;
    }

    public void DisableCanvas()
    {
        _platformHighlight.SetActive(false);
    }

}
