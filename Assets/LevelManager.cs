using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    float starsCounter;
    float maxStar = 3;

    private void Awake()
    {
        waveManager = FindObjectOfType<WaveManager>();
        baseManager = FindObjectOfType<BaseManager>();
        starsManager = FindObjectOfType<StarsManager>();
    }

    private void Update()
    {
        LevelComplete();
        LevelFailed();
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
                for (int i = 0; i < starsCounter; i++)
                {
                    starsManager.filledStar[i].SetActive(true);
                }
                //1filled star
                //2empty star

                Debug.Log("1Star");
                return;
            }
            else if (healthPercentage < 100f)
            {
                starsCounter = 2f;
                for (int i = 0; i < starsCounter; i++)
                {
                    starsManager.filledStar[i].SetActive(true);
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

}
