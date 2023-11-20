using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{

    [SerializeField]
    private GameObject _filledStar;
    [SerializeField]
    private GameObject _emptyStar;

    private WaveManager waveManager;
    private BaseManager baseManager;
    private StarsManager starsManager;
    private CoinsManager coinsManager;


    [SerializeField] private GameObject victoryScreen;
    [SerializeField] private GameObject failedScreen;

    [SerializeField] private TextMeshProUGUI rewardText;

    [SerializeField] private Image fastForward;
    [SerializeField] private TextMeshProUGUI fastForwardText;

    [SerializeField] private Sprite normalSpeed;
    [SerializeField] private Sprite fastSpeed;


    //

    private IDataService DataService = new JsonDataService();
    string saveDataPath = "/data-stageProgress.json";
    StageDataModel newStageDataModel = new StageDataModel();

    LoadingScreenManager loadingScreenManager;

    //

    bool isNormalSpeed;

    public bool isVictory;
    public bool isStageFinished;

    float timeToContinue = 10f;

    float rewardValue;

    float starsCounter;
    float empytStarCounter;
    float maxStar = 3;

    //StageProgress Attributes
    int stageCounter;
    int stageReached;
    int chapterReached;
    bool runOnce;
    private bool hasStageCompleted;


    private void Awake()
    {
        isNormalSpeed = true;
        loadingScreenManager = FindObjectOfType<LoadingScreenManager>();
        waveManager = FindObjectOfType<WaveManager>();
        baseManager = FindObjectOfType<BaseManager>();
        starsManager = FindObjectOfType<StarsManager>();
        coinsManager = FindObjectOfType<CoinsManager>();
        runOnce = false;
        LoadData();
    }

    private void Update()
    {
        LevelComplete();
        LevelFailed();
        if (!PauseMenu.isGamePause)
        {
            if (!TowerManager._isPlacing)
            {
                FastForward();
            }
        }

        if (isStageFinished)
        {

            AutoToContinue();


        }


    }

    private void AutoToContinue()
    {
        timeToContinue -= Time.deltaTime;
        if (timeToContinue <= 0f)
        {
            StageComplete();
            hasStageCompleted = true; // Set the flag to true after completion

        }
    }

    public void ContinueLevel()
    {
        if (!runOnce)
        {
            StageComplete();
            runOnce = true;

        }

    }


    private void StageComplete()
    {
        // Check if the stage has already completed
        if (!hasStageCompleted)
        {
            // After some time, it'll automatically throw us to the continue scene
            loadingScreenManager.LoadLevel("MainHomeScreen");
            //SceneManager.LoadScene("MainHomeScreen");
            hasStageCompleted = true; // Set the flag to true after completion
        }
    }

    private void Start()
    {
        if (starsManager != null)
        {
            Debug.Log("STar manager is NOT NULL");
        }
        else
        {
            Debug.Log("Star manager is NULL");
        }
    }


    void LevelComplete()
    {
        if (waveManager.currentWaveIndex > waveManager.maxWave && !waveManager.isSpawning && !isStageFinished)
        {
            victoryScreen.gameObject.SetActive(true);
            float healthPercentage = baseManager.currentBaseHp / baseManager.maxBaseHp * 100f;
            isVictory = true;
            isStageFinished = true;
            Time.timeScale = 1f;
            //TODO:: if 100% 3 stars
            //if if less than 100% 2stars
            //if less than = 25% 1 star
            if (healthPercentage <= 25f)
            {
                starsCounter = 1f;
                empytStarCounter = 2f;
                for (int i = 0; i < starsCounter; i++)
                {
                    starsManager.filledStar[i].SetActive(true);
                }

                for (int i = 0; i < empytStarCounter; i++)
                {
                    starsManager.empytStar[i].SetActive(true);
                }
                //1filled star
                //2empty star

                Debug.Log("1Star");
                rewardValue = coinsManager._CurrentCoin * 0.25f;
            }
            else if (healthPercentage < 100f)
            {
                starsCounter = 2f;
                empytStarCounter = 1f;
                for (int i = 0; i < starsCounter; i++)
                {
                    starsManager.filledStar[i].SetActive(true);
                }

                for (int i = 0; i < empytStarCounter; i++)
                {
                    starsManager.empytStar[i].SetActive(true);
                }

                Debug.Log("2Star");
                rewardValue = coinsManager._CurrentCoin * 0.5f;
            }
            else
            {
                starsCounter = 3f;
                for (int i = 0; i < starsCounter; i++)
                {
                    starsManager.filledStar[i].SetActive(true);
                }
                Debug.Log("3Star");
                rewardValue = coinsManager._CurrentCoin * 1f;
            }

            CurrencyManager.instance.GetCoin(rewardValue);

            rewardText.text = rewardValue.ToString();

            if (!StageCompletedManager.instance.isCompleted)
            {
                SaveStageProgress();
            }


        }
    }



    void LevelFailed()
    {
        if (baseManager.currentBaseHp <= 0 && !isStageFinished)
        {
            failedScreen.SetActive(true);
            isVictory = false;
            isStageFinished = true;
        }
    }


    void FastForward()
    {
        if (isNormalSpeed)
        {
            fastForward.sprite = normalSpeed;
            Time.timeScale = 1f;
            fastForwardText.text = "1x";

        }
        else
        {
            fastForward.sprite = fastSpeed;
            Time.timeScale = 2f;
            fastForwardText.text = "2x";
            Debug.Log("SPEED2");
        }
    }

    public void SwitchSpeed()
    {
        if (isNormalSpeed)
        {
            isNormalSpeed = false;
        }
        else
        {
            isNormalSpeed = true;
        }
    }

    void LoadData()
    {
        StageDataModel stageData = DataService.LoadData<StageDataModel>(saveDataPath, false);

        stageCounter = stageData.stageCounter;
        stageReached = stageData.stageReached;
        chapterReached = stageData.chapterReached;

    }

    void SaveStageProgress()
    {

        stageCounter++;
        stageReached++;

        if (stageCounter > 5)
        {
            stageCounter = 1;
            chapterReached++;
            newStageDataModel.chapterReached = chapterReached;
            newStageDataModel.stageCounter = stageCounter;
        }
        else
        {
            newStageDataModel.chapterReached = chapterReached;
        }

        newStageDataModel.stageCounter = stageCounter;
        newStageDataModel.stageReached = stageReached;
        Debug.Log("FINISHED STAGE SAVING PROGRESS");
        Debug.Log(stageReached);


        DataService.SaveData(saveDataPath, newStageDataModel, false);
    }


}
