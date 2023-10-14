using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] GameObject squadCanvas;
    [SerializeField] GameObject selectedSquadPanel;
    [SerializeField] GameObject squadSelection;
    [SerializeField] GameObject heroeStatusPanel;

    public static CanvasManager instance;

    public CharacterData selectedHero;

    private void Awake()
    {

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        if (squadCanvas != null)
        {
            squadCanvas.transform.localScale = Vector2.zero;
            selectedSquadPanel.transform.localScale = Vector2.zero;
            squadSelection.transform.localScale = Vector2.zero;
            heroeStatusPanel.transform.localScale = Vector2.zero;
        }
    }

    #region SquadCanvasManip


    #region openingLogic


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

    public void HeroeStatusButton(CharacterData characterData)
    {
        selectedHero = characterData;
        squadSelection.transform.localScale = Vector3.zero;
        heroeStatusPanel.transform.localScale = Vector3.one;
    }

    public void CloseScreenHeroeStatus(GameObject gameObject)
    {
        gameObject.transform.localScale = Vector3.zero;
        selectedSquadPanel.transform.localScale = Vector3.one;
    }

    #endregion end of openingLogic


    #region Closing Button

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

    public void CloseHeroesStatus()
    {
        squadSelection.transform.localScale = Vector3.one;
        heroeStatusPanel.transform.localScale = Vector2.zero;
    }

    #endregion


    #endregion





}
