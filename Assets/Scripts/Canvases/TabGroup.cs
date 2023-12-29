using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabGroup : MonoBehaviour
{

    public List<TabButtons> tabButtons;
    public Color tabIdle;
    public Color tabHover;
    public Color tabActive;
    public TabButtons selectedTab;
    public List<GameObject> objectsToSwap;

    private void Start()
    {
        foreach (TabButtons tabBut in tabButtons)
        {
            if (tabBut.defaultButton)
            {
                OnTabSelected(tabBut);
            }
        }
    }

    public void Subscribe(TabButtons button)
    {

        if (tabButtons == null)
        {
            tabButtons = new List<TabButtons>();

        }
        tabButtons.Add(button);

    }

    public void OnTabEnter(TabButtons button)
    {
        ResetTabs();
        if (selectedTab == null || button != selectedTab)
        {
            button.background.color = tabHover;
        }

    }

    public void OnTabExit(TabButtons button)
    {
        ResetTabs();
    }

    public void OnTabSelected(TabButtons button)
    {
        selectedTab = button;
        ResetTabs();
        button.background.color = tabActive;
        int index = button.transform.GetSiblingIndex();
        for (int i = 0; i < objectsToSwap.Count; i++)
        {
            if (i == index)
            {
                objectsToSwap[i].SetActive(true);
            }
            else
            {
                objectsToSwap[i].SetActive(false);
            }
        }
    }

    public void ResetTabs()
    {
        foreach (TabButtons buttons in tabButtons)
        {
            if (selectedTab != null && buttons != selectedTab)
            {
                buttons.background.color = tabIdle;
            }
        }
    }




}
