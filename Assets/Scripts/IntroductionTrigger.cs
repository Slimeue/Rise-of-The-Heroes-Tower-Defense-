using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IntroductionTrigger : MonoBehaviour
{
    [SerializeField] EnemiesData enemiesData;
    [SerializeField] GameObject introductionPanel;

    [SerializeField] Image enemyArtWork;
    [SerializeField] TextMeshProUGUI enemyName;
    [SerializeField] TextMeshProUGUI enemyHp;
    [SerializeField] TextMeshProUGUI enemyDefense;
    [SerializeField] TextMeshProUGUI enemyAttack;
    [SerializeField] TextMeshProUGUI enemyLore;

    bool isIntroduce;

    IDataService dataService = new JsonDataService();
    string dataPath = "/data-enemy";


    private void Awake()
    {
        introductionPanel.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        IEnemyDataGetable enemyDataGetable = other.GetComponent<IEnemyDataGetable>();

        if (other.gameObject.CompareTag("Enemy"))
        {
            enemiesData = enemyDataGetable.GetEnemyData();
            SaveLoadData();
        }
    }

    void SaveLoadData()
    {
        string newSaveDataPath = $"{dataPath}-{enemiesData.enemyName}.json";

        string path = Application.persistentDataPath + newSaveDataPath;

        if (!File.Exists(path))
        {
            isIntroduce = true;
            EnemyModel enemyModel = new EnemyModel
            {
                isIntroduce = isIntroduce
            };

            dataService.SaveData(newSaveDataPath, enemyModel, false);
            Time.timeScale = 0f;
            enemyName.text = enemiesData.enemyName;
            enemyArtWork.sprite = enemiesData.enemyArtWork;
            enemyHp.text = enemiesData.maxHp.ToString();
            enemyDefense.text = enemiesData.baseArmor.ToString();
            enemyAttack.text = enemiesData.dmgValue.ToString();
            enemyLore.text = enemiesData.enemyLore;
            introductionPanel.SetActive(true);
            PauseMenu.isGamePause = true;
        }


        EnemyModel enemyData = dataService.LoadData<EnemyModel>(newSaveDataPath, false);

        isIntroduce = enemyData.isIntroduce;

    }

    public void ClosePanel()
    {
        introductionPanel.SetActive(false);
        PauseMenu.isGamePause = false;
    }
}
