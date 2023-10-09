using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

    [SerializeField] TextMeshProUGUI _waveUIText;
    [SerializeField] Slider slider;

    public int currentWaveIndex = 1;
    public int maxWave;
    float nextWaveTime;
    public bool isSpawning;

    WaveData waveData;

    private void Awake()
    {
        maxWave = objectPooler.WaveCount;
        nextWaveTime = timeBetweenWave;
    }

    void Start()
    {
        _waveUIText.text = "Wave " + currentWaveIndex + "/" + maxWave;
    }

    void Update()
    {
        isSpawning = NotSpawning();

        if (currentWaveIndex <= maxWave)
        {
            StartWave();

        }
        else
        {
            //TODO: WaveCompletion() 
        }
        inActives = GameObject.FindGameObjectsWithTag("Enemy");

    }

    void StartWave()
    {
        if (!isSpawning)
        {
            StartNextWave();
            Debug.Log("Current Wave: " + currentWaveIndex);
        }

    }

    void StartNextWave()
    {


        if (nextWaveTime > 0)
        {
            nextWaveTime -= Time.deltaTime;
            float normalizedValue = 1.0f - (nextWaveTime / timeBetweenWave); // Normalize to 0 - 1
            slider.value = normalizedValue;

        }
        else
        {
            NextWave();
            UpdateWaveText();
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

                for (int x = 0; x < waveData.enemyCount * waveData.enemies.Length; x++)
                {
                    GameObject enemy = objectPooler.GetPooledEnemy(i);
                    float offset = 5;
                    Vector3 newPosition = spawnPoint.transform.position;
                    newPosition.y += offset;
                    enemy.transform.position = newPosition;
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

    public void UpdateWaveText()
    {
        _waveUIText.text = "Wave " + currentWaveIndex + "/" + maxWave;
    }

}
