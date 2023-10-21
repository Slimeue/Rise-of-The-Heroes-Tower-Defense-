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

    [SerializeField] private Image fastForward;
    [SerializeField] private TextMeshProUGUI fastForwardText;

    [SerializeField] private Sprite normalSpeed;
    [SerializeField] private Sprite fastSpeed;

    bool isNormalSpeed;

    public bool isVictory;
    public bool isStageFinished;

    float timeToContinue = 5f;

    float starsCounter;
    float empytStarCounter;
    float maxStar = 3;

    private void Awake()
    {
        isNormalSpeed = true;

        waveManager = FindObjectOfType<WaveManager>();
        baseManager = FindObjectOfType<BaseManager>();
        starsManager = FindObjectOfType<StarsManager>();
        coinsManager = FindObjectOfType<CoinsManager>();
    }

    private void Update()
    {
        LevelComplete();
        LevelFailed();
        FastForward();
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
            CurrencyManager.instance.GetCoin(coinsManager._CurrentCoin);
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
                return;
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
                return;
            }
            else
            {
                starsCounter = 3f;
                for (int i = 0; i < starsCounter; i++)
                {
                    starsManager.filledStar[i].SetActive(true);
                }
                Debug.Log("3Star");
                return;
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



}
