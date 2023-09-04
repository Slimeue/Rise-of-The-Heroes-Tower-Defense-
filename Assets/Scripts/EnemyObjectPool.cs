using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyObjectPool : MonoBehaviour
{

    [SerializeField] int _poolsize = 5;
    [SerializeField] private float _spawnTimer = 1f;

    [SerializeField] float _waveInterval = 5f;

    [SerializeField] WaveData[] waves;

    GameObject[] _pool;

    int currentWave = 0;
    int maxWaves = 2;

    Boolean isSpawning;

    private void Awake()
    {
        PopulatePool();
    }

    // Update is called once per frame
    void Start()
    {
        StartNextWave();
    }

    void PopulatePool()
    {
        _pool = new GameObject[_poolsize];
        int poolIndex = 0;

        for (int i = 0; i < waves.Length; i++)
        {
            WaveData wave = waves[i];

            for (int y = 0; y < wave.enemies.Length; y++)
            {
                Debug.Log(wave.enemies.Length);
                for (int x = 0; x < wave.enemyCount; x++)
                {
                    if (poolIndex < _poolsize)
                    {
                        _pool[poolIndex] = Instantiate(wave.enemies[y].enemyPrefab, transform);
                        _pool[poolIndex].SetActive(false);
                        poolIndex++;
                    }
                    else
                    {
                        Debug.LogWarning("Pool size exceeded. Consider increasing the pool size.");
                        return; // Exit the function as there's no more space in the pool
                    }
                }

            }
        }
    }


    void StartNextWave()
    {
        if (!isSpawning)
        {
            StartCoroutine(SpawnWaves());
        }
    }

    IEnumerator SpawnWaves()
    {

        isSpawning = true;

        StartCoroutine(SpawnEnemy());

        yield return new WaitForSeconds(_waveInterval);
        isSpawning = false;
        currentWave++;


        if (currentWave < maxWaves)
        {
            StartNextWave();
        }
    }

    IEnumerator SpawnEnemy()
    {

        while (true)
        {

            EnableObjectInpool();
            yield return new WaitForSeconds(_spawnTimer);

        }
    }

    private void EnableObjectInpool()
    {
        for (int i = 0; i < _pool.Length; i++)
        {
            if (_pool[i] != null && !_pool[i].activeInHierarchy)
            {
                _pool[i].SetActive(true);
                Debug.Log(_pool[i].gameObject.name);
                return;
            }
        }
    }
}
