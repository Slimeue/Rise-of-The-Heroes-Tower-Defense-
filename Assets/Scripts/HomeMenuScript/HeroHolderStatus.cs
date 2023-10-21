using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;

public class HeroHolderStatus : MonoBehaviour
{
    [SerializeField] public CharacterData characterData;
    [SerializeField] Image charArtWork;
    [SerializeField] TextMeshProUGUI[] charName;
    [SerializeField] TextMeshProUGUI charHealth;
    [SerializeField] TextMeshProUGUI charAttack;
    [SerializeField] TextMeshProUGUI charDefense;
    [SerializeField] TextMeshProUGUI charLevel;
    [SerializeField] TextMeshProUGUI charLore;

    IDataService dataService = new JsonDataService();

    string saveDataPath = "/character-data"; //TODO static reference

    private SpecialCharacterData specialCharacterData = new SpecialCharacterData();

    private CharacterStats characterStats = new CharacterStats();

    bool EncryptionEnabled;

    private void OnEnable()
    {
        if (CanvasManager.instance.heroesStatusCharacterData != null)
        {
            characterData = CanvasManager.instance.heroesStatusCharacterData;
            charArtWork.sprite = characterData.charArtWork;



            string newSaveDataPath = $"{saveDataPath}-{characterData.charName}.json";

            string path = Application.persistentDataPath + newSaveDataPath;


            if (!File.Exists(path))
            {
                if (!characterStats.stats.ContainsKey(characterData.charName))
                {
                    CharacterStats.charStats characterStatsData = new CharacterStats.charStats
                    {
                        charName = characterData.charName,
                        level = characterData.charLevel,
                        hp = characterData.maxHp,
                        damage = characterData.dmgValue,
                        armor = characterData.baseArmor
                    };

                    characterStats.stats.Add(characterData.charName, characterStatsData);
                    dataService.SaveData(newSaveDataPath, characterStats, EncryptionEnabled);


                }
                else
                {
                    return;
                }

            }


            CharacterStats data = dataService.LoadData<CharacterStats>(newSaveDataPath, EncryptionEnabled);
            Debug.Log("File exist!");
            try
            {
                CharacterStats.charStats charData = data.stats[characterData.charName];
                foreach (TextMeshProUGUI _charName in charName)
                {
                    _charName.text = charData.charName;
                }
                charHealth.text = "Health: " + charData.hp;
                charAttack.text = "Attack: " + charData.damage;
                charDefense.text = "Defense: " + charData.armor;
                charLevel.text = "Lvl: " + charData.level;
                charLore.text = characterData.charLore;

            }
            catch (Exception e)
            {
                Debug.Log(e.Message + " " + e.StackTrace);
            }





        }
        else return;

    }


}
