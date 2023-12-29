using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TabButtons : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public TabGroup tabGroup;

    public Image background;

    public bool defaultButton;

    public void OnPointerClick(PointerEventData eventData)
    {
        tabGroup.OnTabSelected(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        tabGroup.OnTabEnter(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tabGroup.OnTabExit(this);
    }

    // Start is called before the first frame update
    void Awake()
    {
        background = GetComponent<Image>();
        tabGroup.Subscribe(this);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
