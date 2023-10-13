using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] GameObject squadCanvas;
    [SerializeField] GameObject selectedSquadPanel;
    [SerializeField] GameObject squadSelection;

    private void Awake()
    {
        if (squadCanvas != null)
        {
            squadCanvas.transform.localScale = Vector2.zero;
            selectedSquadPanel.transform.localScale = Vector2.zero;
            squadSelection.transform.localScale = Vector2.zero;
        }
    }

    public void SquadButton()
    {
        squadCanvas.transform.localScale = Vector3.one;
        selectedSquadPanel.transform.localScale = Vector3.one;
    }

    public void SquadSelectionButton()
    {
        selectedSquadPanel.transform.localScale = Vector2.zero;
        squadSelection.transform.localScale = Vector3.one;
    }

    public void CloseSelectedSquad()
    {
        squadCanvas.transform.localScale = Vector2.zero;
        selectedSquadPanel.transform.localScale = Vector2.zero;
    }

    public void CloseSquadSelection()
    {
        selectedSquadPanel.transform.localScale = Vector3.one;
        squadSelection.transform.localScale = Vector3.zero;
    }
}
