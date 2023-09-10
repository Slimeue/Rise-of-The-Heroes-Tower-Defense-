using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField]
    ObjectPooler objectPooler;
    [SerializeField]
    Transform spawnPoint;
    [SerializeField]
    float timeBetweenWave = 10f;
    [SerializeField]
    float spawnInterval = 2f;

    [SerializeField] GameObject[] inActives;

    int currentWaveIndex = 1;
    int maxWave;
    float nextWaveTime;
    bool isSpawning;

    WaveData waveData;

    private void Awake()
    {
        maxWave = objectPooler.WaveCount;
    }

    void Start()
    {

    }


    void Update()
    {

        isSpawning = NotSpawning();
        // for (int i = 0; i < objectPooler.WaveCount; i++)
        // {
        //     
        //     maxWave = waveData.enemies.Length;
        // }


        if (currentWaveIndex <= maxWave)
        {
            StartWave();
        }
        else
        {
            //TODO: WaveCompletion() 
        }

    }

    void StartWave()
    {
        if (!isSpawning)
        {
            StartNextWave();
            Debug.Log("Current Wave: " + currentWaveIndex);
        }
        inActives = GameObject.FindGameObjectsWithTag("Enemy");
    }

    void StartNextWave()
    {
        if (nextWaveTime > 0)
        {
            nextWaveTime -= Time.deltaTime;
        }
        else
        {
            NextWave();
        }
    }

    void NextWave()
    {
        isSpawning = true;
        StartCoroutine(SpawnEnemyWave(currentWaveIndex));
    }
    IEnumerator SpawnEnemyWave(int wave)
    {
        for (int i = 0; i < wave; i++)
        {

            if (i < objectPooler.WaveCount)
            {

                waveData = objectPooler._waveData[i];

                for (int x = 0; x < waveData.enemyCount * 2; x++)
                {
                    GameObject enemy = objectPooler.GetPooledEnemy(i);
                    enemy.transform.position = spawnPoint.transform.position;
                    enemy.transform.rotation = spawnPoint.transform.rotation;
                    enemy.SetActive(true);
                    Debug.Log(enemy.name);
                    yield return new WaitForSeconds(spawnInterval);

                }
                nextWaveTime = timeBetweenWave;
            }

            yield return null;
        }
        currentWaveIndex++;
    }


    public bool NotSpawning()
    {
        if (inActives == null || inActives.Length == 0)
        {
            return false;
        }
        else
        {
            return true;
        }


    }




}
