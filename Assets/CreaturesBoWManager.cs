using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreaturesBoWManager : MonoBehaviour
{

    [SerializeField] EnemiesData enemiesData;

    IDataService dataService = new JsonDataService();
    string dataPath = "/data-enemy";

    public bool isUnlocked;
    public Color unlockedColor;

    //UI
    [SerializeField] GameObject chain;
    [SerializeField] Image artwork;
    [SerializeField] TextMeshProUGUI enemyName;

    private void Awake()
    {
        LoadData();
    }

    private void Start()
    {
        artwork.sprite = enemiesData.enemyArtWork;
        enemyName.text = enemiesData.enemyName;

        if (isUnlocked)
        {
            Debug.Log(enemiesData.enemyName + " " + isUnlocked);
            artwork.color = unlockedColor;
            chain.SetActive(false);
        }
    }

    private void LoadData()
    {
        string newSaveDataPath = $"{dataPath}-{enemiesData.enemyName}.json";

        string path = Application.persistentDataPath + newSaveDataPath;

        if (!File.Exists(path))
        {
            return;
        }
        EnemyModel enemyModel = dataService.LoadData<EnemyModel>(newSaveDataPath, false);


        isUnlocked = enemyModel.isIntroduce;
    }

}
