using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [SerializeField]
    public List<WaveData> _waveData;
    [SerializeField]
    [Range(1, 20)] int _poolSize = 10;


    private List<GameObject>[] _pooledEnemiesWave;
    GameObject enemy;
    private void Awake()
    {

        _pooledEnemiesWave = new List<GameObject>[_waveData.Count];
        for (int i = 0; i < _waveData.Count; i++)
        {
            _pooledEnemiesWave[i] = new List<GameObject>();
        }

        PopulatePool();
    }

    void PopulatePool()
    {

        int poolIndex = 0;
        int waveNum = 1;

        for (int waveIndex = 0; waveIndex < _waveData.Count; waveIndex++)
        {
            WaveData wave = _waveData[waveIndex];
            GameObject parentObject = new GameObject("Wave " + waveNum);
            parentObject.transform.SetParent(transform);

            for (int y = 0; y < wave.enemies.Length; y++)
            {
                for (int x = 0; x < wave.enemyCount; x++)
                {
                    if (_pooledEnemiesWave[waveIndex].Count < _poolSize)
                    {
                        enemy = Instantiate(wave.enemies[y].enemyPrefab, parentObject.transform);
                        enemy.SetActive(false);
                        _pooledEnemiesWave[waveIndex].Add(enemy);
                        poolIndex++;
                    }

                }
            }

            waveNum++;
        }
    }

    public GameObject GetPooledEnemy(int waveIndex)
    {

        if (waveIndex >= 0 && waveIndex < _waveData.Count)
        {
            for (int i = 0; i < _pooledEnemiesWave[waveIndex].Count; i++)
            {
                if (!_pooledEnemiesWave[waveIndex][i].activeInHierarchy)
                {
                    return _pooledEnemiesWave[waveIndex][i];
                }
            }
        }
        return null;

    }

    public int WaveCount
    {
        get { return _waveData.Count; }
    }


    public WaveData GetWave(int index)
    {
        if (index >= 0 && index < _waveData.Count)
        {
            return _waveData[index];
        }
        return new WaveData();
    }

}
