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

    int currentWaveIndex = 0;
    float nextWaveTime;
    bool isSpawning;



    void Start()
    {

    }


    void Update()
    {

        isSpawning = NotSpawning();
        if (!isSpawning)
        {
            if (nextWaveTime > 0)
            {
                nextWaveTime -= Time.deltaTime;
            }
            else
            {
                StartNextWave();
            }
        }
        inActives = GameObject.FindGameObjectsWithTag("Enemy");
    }

    void StartNextWave()
    {
        isSpawning = true;
        StartCoroutine(SpawnWave(currentWaveIndex));
    }
    IEnumerator SpawnWave(int wave)
    {
        for (int i = 0; i <= wave; i++)
        {

            WaveData waveData = objectPooler._waveData[i];
            for (int x = 0; x < waveData.enemyCount * 2; x++)
            {
                GameObject enemy = objectPooler.GetPooledEnemy(i);
                enemy.transform.position = spawnPoint.transform.position;
                enemy.transform.rotation = spawnPoint.transform.rotation;
                enemy.SetActive(true);
                Debug.Log(enemy.name);
                yield return new WaitForSeconds(spawnInterval);

            }
            if (currentWaveIndex < waveData.enemies.Length)
            {
                currentWaveIndex++;
            }
            nextWaveTime = timeBetweenWave;
            yield return null;
        }
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
