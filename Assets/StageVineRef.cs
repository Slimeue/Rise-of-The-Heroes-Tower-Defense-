using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class StageVineRef : MonoBehaviour, IPointerClickHandler
{
    public GameObject vineSprite;
    public GameObject stageCompeletText;
    public TextMeshProUGUI title;
    public bool isCompleted;

    public void OnPointerClick(PointerEventData eventData)
    {
        StageCompletedManager.instance.StageCompleted(this);
    }

    private void Awake()
    {
        isCompleted = false;
    }

}
