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

    int currentWaveIndex = 0;
    float nextWaveTime;

    void Start()
    {
        Debug.Log(" Wave " + objectPooler.GetPooledEnemy(1));
    }


    void Update()
    {

    }

    void StartNextWave()
    {

    }

    void SpawnWave()
    {

    }

}
