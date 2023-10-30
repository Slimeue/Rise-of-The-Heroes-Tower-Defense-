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

    //

    bool isNormalSpeed;

    public bool isVictory;
    public bool isStageFinished;

    float timeToContinue = 5f;

    float rewardValue;

    float starsCounter;
    float empytStarCounter;
    float maxStar = 3;

    //StageProgress Attributes
    int stageCounter;
    int stageReached;
    int chapterReached;

    private void Awake()
    {
        isNormalSpeed = true;

        waveManager = FindObjectOfType<WaveManager>();
        baseManager = FindObjectOfType<BaseManager>();
        starsManager = FindObjectOfType<StarsManager>();
        coinsManager = FindObjectOfType<CoinsManager>();

        LoadData();
    }

    private void Update()
    {
        LevelComplete();
        LevelFailed();

        if (!PauseMenu.isGamePause)
        {
            FastForward();
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
        }
    }


    private void StageComplete()
    {
        //After some time itll automatically throw us to the continue scene
        SceneManager.LoadScene("MainHomeScreen");
        Debug.Log("New Scene continue to reward scene!!");
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
