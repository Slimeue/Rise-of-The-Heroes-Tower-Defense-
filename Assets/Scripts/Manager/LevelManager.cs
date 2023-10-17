using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
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

    [SerializeField] private GameObject victoryScreen;
    [SerializeField] private GameObject failedScreen;

    [SerializeField] private Image fastForward;
    [SerializeField] private TextMeshProUGUI fastForwardText;

    [SerializeField] private Sprite normalSpeed;
    [SerializeField] private Sprite fastSpeed;

    bool isNormalSpeed;

    float starsCounter;
    float empytStarCounter;
    float maxStar = 3;

    private void Awake()
    {
        isNormalSpeed = true;
        waveManager = FindObjectOfType<WaveManager>();
        baseManager = FindObjectOfType<BaseManager>();
        starsManager = FindObjectOfType<StarsManager>();
    }

    private void Update()
    {
        LevelComplete();
        LevelFailed();
        FastForward();
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
        if (waveManager.currentWaveIndex > waveManager.maxWave && !waveManager.isSpawning)
        {
            victoryScreen.gameObject.SetActive(true);
            float healthPercentage = baseManager.currentBaseHp / baseManager.maxBaseHp * 100f;
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
        if (baseManager.currentBaseHp <= 0)
        {
            failedScreen.SetActive(true);
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
