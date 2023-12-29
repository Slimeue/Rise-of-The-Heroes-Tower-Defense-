using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class LevelWindow : MonoBehaviour
{
    [SerializeField] CharacterData characterData;

    //[SerializeField] TextMeshProUGUI charHealth;
    // [SerializeField] TextMeshProUGUI charAttack;
    // [SerializeField] TextMeshProUGUI charDefense;
    [SerializeField] TextMeshProUGUI charLevel;
    [SerializeField] TextMeshProUGUI charExperienceToNextLever;
    [SerializeField] TextMeshProUGUI charNextLevel;
    [SerializeField] TextMeshProUGUI currentCurrency;

    int level;
    float currentPearl;
    float experienceToNextLevel;

    //stats

    public float hp;
    public float damage;
    public float armor;

    float armorPerLevel;
    float damagePerLevel;
    float hpPerLevel;
    float experiencePerLevel;

    //
    CharacterStats.charStats charData;
    DataPathClass dataPathClass = new DataPathClass();
    IDataService dataService = new JsonDataService();
    string saveDataPath = "/character-data"; //TODO static reference

    CanvasManager canvasManager;


    private void OnEnable()
    {
        canvasManager = FindObjectOfType<CanvasManager>();
        if (canvasManager.heroesStatusCharacterData != null)
        {
            characterData = canvasManager.heroesStatusCharacterData;
            LoadDataLevel();
        }


    }

    private void LoadDataLevel()
    {
        string newSaveDataPath = $"{saveDataPath}-{characterData.charName}.json";
        string path = Application.persistentDataPath + newSaveDataPath;

        CharacterStats data = dataService.LoadData<CharacterStats>(newSaveDataPath, false);
        CurrencyModel coinData = dataService.LoadData<CurrencyModel>(dataPathClass.coinPath, false);

        currentCurrency.text = coinData.coins.ToString();
        currentPearl = coinData.coins;


        try
        {
            charData = data.stats[characterData.charName];

            //charHealth.text = "Health: " + charData.hp;
            // charAttack.text = "Attack: " + charData.damage;
            // charDefense.text = "Defense: " + charData.armor;
            charLevel.text = "Level: " + charData.level;
            charExperienceToNextLever.text = "Requried: " + charData.experienceToNextLevel;
            if (charData.level >= 5)
            {
                charNextLevel.text = "MAX LEVEL";
                charExperienceToNextLever.text = "Requried: NONE ";

            }
            else
            {
                charNextLevel.text = "Level: " + (charData.level + 1);
            }
            level = charData.level;
            experienceToNextLevel = charData.experienceToNextLevel;
            hp = charData.hp;
            damage = charData.damage;
            armor = charData.armor;

            hpPerLevel = hp * 0.1f;
            damagePerLevel = damage * 0.1f;
            armorPerLevel = damage * 0.1f;
            experiencePerLevel = experienceToNextLevel * 0.5f;

        }
        catch (Exception e)
        {
            Debug.Log(e.Message + " " + e.StackTrace);
        }
    }

    public void SaveDataLevel()
    {

        level++;
        currentPearl -= experienceToNextLevel;
        float newHp = hp + (level * hpPerLevel);
        float newDamage = damage + (level * damagePerLevel);
        float newArmor = armor + (level * armorPerLevel);
        float newExperienceToNextLevel = experienceToNextLevel + (level * experiencePerLevel);

        CharacterStats characterStats = new CharacterStats();

        string newSaveDataPath = $"{saveDataPath}-{characterData.charName}.json";

        string path = Application.persistentDataPath + newSaveDataPath;



        if (!characterStats.stats.ContainsKey(characterData.charName))
        {
            CharacterStats.charStats characterStatsData = new CharacterStats.charStats
            {
                charName = characterData.charName,
                level = level,
                experienceToNextLevel = newExperienceToNextLevel,
                hp = newHp,
                damage = newDamage,
                armor = newArmor
            };

            CurrencyModel coinData = new CurrencyModel();

            coinData.coins = currentPearl;

            characterStats.stats.Add(characterData.charName, characterStatsData);
            dataService.SaveData(newSaveDataPath, characterStats, false);
            dataService.SaveData(dataPathClass.coinPath, coinData, false);


        }
        else
        {
            return;
        }


    }

    public void AddLevel()
    {

        if (currentPearl >= experienceToNextLevel && level < 5)
        {
            Debug.Log("LevelUp");
            SaveDataLevel();
            LoadDataLevel();
            CurrencyManager.instance.LoadCurrency();
        }

    }
}
