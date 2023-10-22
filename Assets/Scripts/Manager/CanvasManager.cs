using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] GameObject squadCanvas;
    [SerializeField] GameObject selectedSquadPanel;
    [SerializeField] GameObject squadSelection;
    [SerializeField] GameObject heroeStatusPanel;
    [SerializeField] GameObject mythicalHeroesStatusPanel;

    [Space(5)]
    [Header("HeroesScreen")]
    [SerializeField] GameObject heroesScreenPanel; //To deactive when click a card
    [SerializeField] GameObject heroesScreenPanelBackButton;
    [SerializeField] GameObject heroesScrollArea;
    [SerializeField] GameObject heroesStatusScreen;
    public CharacterData heroesStatusCharacterData;

    [Space(5)]
    [Header("LevelScreen")]
    [SerializeField] GameObject levelWindow;


    [Space(5)]
    [Header("Currency")]
    [SerializeField] TextMeshProUGUI currentCurrency;


    public static CanvasManager instance;
    [Space(5)]
    [Header("Character Datas")]
    public CharacterData selectedHero;
    public CharacterData selectedMythicalHero;
    public CharacterData selectedHeroForStatus;

    IDataService dataService = new JsonDataService();
    DataPathClass dataPathClass = new DataPathClass();


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
            mythicalHeroesStatusPanel.transform.localScale = Vector2.zero;
            heroesScreenPanel.transform.localScale = Vector2.zero;
            heroesStatusScreen.SetActive(false);
            levelWindow.SetActive(false);

        }
    }

    private void Update()
    {
        currentCurrency.text = CurrencyManager.instance.currentCurrency.ToString();
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
        selectedHeroForStatus = characterData;
        squadSelection.transform.localScale = Vector3.zero;
        heroeStatusPanel.transform.localScale = Vector3.one;
    }

    public void MythicalHeroesStatusButton(CharacterData characterData)
    {
        selectedMythicalHero = characterData;
        selectedHeroForStatus = characterData;
        selectedSquadPanel.transform.localScale = Vector3.zero;
        mythicalHeroesStatusPanel.transform.localScale = Vector3.one;
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

    public void CloseMythicalHeroesStatus()
    {
        selectedSquadPanel.transform.localScale = Vector3.one;
        mythicalHeroesStatusPanel.transform.localScale = Vector2.zero;
    }

    #endregion


    #endregion



    #region HEROES SCREEN


    public void OpenHeroesPanel()
    {
        heroesScreenPanel.transform.localScale = Vector3.one;
    }

    public void CloseHeroesPanel()
    {
        heroesScreenPanel.transform.localScale = Vector3.zero;
    }

    public void OpenHeroesStatus(CharacterData characterData)
    {
        heroesStatusCharacterData = characterData;
        heroesScrollArea.SetActive(false);
        heroesStatusScreen.SetActive(true);
        heroesScreenPanelBackButton.SetActive(false);
    }

    public void CloseHeroesStatusScreen()
    {
        heroesScrollArea.SetActive(true);
        heroesStatusScreen.SetActive(false);
        heroesScreenPanelBackButton.SetActive(true);
    }

    public void OpenLevelWindow()
    {
        levelWindow.SetActive(true);
    }

    public void CloseLevelWindows()
    {
        levelWindow.SetActive(false);
    }

    #endregion



}
