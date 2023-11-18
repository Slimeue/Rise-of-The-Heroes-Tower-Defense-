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
    public CharacterData selectedHeroesForBookInfo;
    public EnemiesData selectedEnemyForBookInfo;

    IDataService dataService = new JsonDataService();
    DataPathClass dataPathClass = new DataPathClass();

    [Space(5)]
    [Header("StageSelection")]
    public GameObject StageSelectionUI;
    public GameObject StageSelectionBackButton;

    [Space(5)]
    [Header("Book Of Wisdom UI")]
    public GameObject BookOfWisdomUI;
    public GameObject BookOfWisdomUICloseButton;
    public GameObject ClosedBookUI;
    public GameObject OpenBookUI;
    //
    public GameObject BookOfWisdomHeroesInfo;
    public GameObject BookOfWisdomCreaturesInfo;



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
            Time.timeScale = 1f;
            squadCanvas.SetActive(false);
            selectedSquadPanel.SetActive(false);
            squadSelection.SetActive(false);
            heroeStatusPanel.SetActive(false);
            mythicalHeroesStatusPanel.SetActive(false);
            heroesScreenPanel.SetActive(false);
            heroesStatusScreen.SetActive(false);
            levelWindow.SetActive(false);
            StageSelectionUI.SetActive(false);
            BookOfWisdomUI.SetActive(false);
            BookOfWisdomCreaturesInfo.SetActive(false);
            BookOfWisdomHeroesInfo.SetActive(false);
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
        squadCanvas.SetActive(true);
        selectedSquadPanel.SetActive(true);

    }

    public void SquadSelectionButton()
    {
        selectedSquadPanel.SetActive(false);
        squadSelection.SetActive(true);
    }

    public void HeroeStatusButton(CharacterData characterData)
    {
        selectedHero = characterData;
        selectedHeroForStatus = characterData;
        squadSelection.SetActive(false);
        heroeStatusPanel.SetActive(true);
    }

    public void MythicalHeroesStatusButton(CharacterData characterData)
    {
        selectedMythicalHero = characterData;
        selectedHeroForStatus = characterData;
        selectedSquadPanel.SetActive(false);
        mythicalHeroesStatusPanel.SetActive(true);
    }

    public void CloseScreenHeroeStatus(GameObject gameObject)
    {
        gameObject.SetActive(false);
        selectedSquadPanel.SetActive(true);
    }

    #endregion end of openingLogic


    #region Closing Button

    public void CloseSelectedSquad()
    {
        squadCanvas.SetActive(false);
        selectedSquadPanel.SetActive(false);
    }

    public void CloseSquadSelection()
    {
        selectedSquadPanel.SetActive(true);
        squadSelection.SetActive(false);
    }

    public void CloseHeroesStatus()
    {
        squadSelection.SetActive(true);
        heroeStatusPanel.SetActive(false);
    }

    public void CloseMythicalHeroesStatus()
    {
        selectedSquadPanel.SetActive(true);
        mythicalHeroesStatusPanel.SetActive(false);
    }

    #endregion


    #endregion



    #region HEROES SCREEN


    public void OpenHeroesPanel()
    {
        heroesScreenPanel.SetActive(true);
    }

    public void CloseHeroesPanel()
    {
        heroesScreenPanel.SetActive(false);
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
        heroesStatusScreen.SetActive(false);
    }

    public void CloseLevelWindows()
    {
        levelWindow.SetActive(false);
        heroesStatusScreen.SetActive(true);
    }


    //StageSelection
    public void StageSelectionOpen()
    {
        StageSelectionUI.SetActive(true);
    }

    public void CloseStageSelection()
    {
        StageSelectionUI.SetActive(false);
    }

    //BookOfWisdom

    public void BookOfWisdomOpen()
    {
        BookOfWisdomUI.SetActive(true);
    }

    public void CloseBookOfWisdomUI()
    {

        if (OpenBookUI.activeInHierarchy)
        {
            OpenBookUI.SetActive(false);
            ClosedBookUI.SetActive(true);
            return;
        }
        BookOfWisdomUI.SetActive(false);
    }

    public void OpenBookOpenUI()
    {
        OpenBookUI.SetActive(true);
        ClosedBookUI.SetActive(false);
    }

    //

    public void OpenHeroesBookInfo(CharacterData characterData)
    {
        selectedHeroesForBookInfo = characterData;
        BookOfWisdomHeroesInfo.SetActive(true);
        OpenBookUI.SetActive(false);
    }

    public void CloseHeroesBookInfo()
    {
        BookOfWisdomHeroesInfo.SetActive(false);
        OpenBookUI.SetActive(true);

    }

    public void OpenCreaturesBookInfo(EnemiesData enemiesData)
    {
        selectedEnemyForBookInfo = enemiesData;
        BookOfWisdomCreaturesInfo.SetActive(true);
        OpenBookUI.SetActive(false);
    }

    public void CloseCreaturesBookInfo()
    {
        BookOfWisdomCreaturesInfo.SetActive(false);
        OpenBookUI.SetActive(true);
    }

    #endregion



}
