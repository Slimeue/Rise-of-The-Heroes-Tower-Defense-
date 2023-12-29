using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HeroSelection : MonoBehaviour
{

    public CharacterData selectedCharacter;
    public CharacterData selectedCharacterForStatus;
    public GameObject heroeStatus;

    private IDataService DataService = new JsonDataService();

    [SerializeField] Image charArtWork;
    [SerializeField] TextMeshProUGUI charName;
    [SerializeField] TextMeshProUGUI level;
    [SerializeField] TextMeshProUGUI health;
    [SerializeField] TextMeshProUGUI attack;
    [SerializeField] TextMeshProUGUI defense;

    CanvasManager canvasManager;

    string newSaveDataPath;

    string saveDataPathSelectedSquad = "/data-squad.json";

    private SpecialCharacterData specialCharacterData = new SpecialCharacterData();


    IDataService dataService = new JsonDataService();

    string saveDataPath = "/character-data"; //TODO static reference

    private CharacterStats characterStats = new CharacterStats();


    bool EncryptionEnabled;

    private void Awake()
    {
        canvasManager = FindObjectOfType<CanvasManager>();

    }

    private void OnEnable()
    {

        selectedCharacter = canvasManager.selectedHero;
        selectedCharacterForStatus = canvasManager.selectedHeroForStatus;
        if (selectedCharacterForStatus != null)
        {
            charArtWork.sprite = selectedCharacterForStatus.charArtWork;
            LoadData();
        }



    }

    public void LoadData()
    {

        newSaveDataPath = $"{saveDataPath}-{selectedCharacterForStatus.charName}.json";

        string path = Application.persistentDataPath + newSaveDataPath;


        if (!File.Exists(path))
        {
            if (!characterStats.stats.ContainsKey(selectedCharacterForStatus.charName))
            {
                CharacterStats.charStats characterStatsData = new CharacterStats.charStats
                {
                    charName = selectedCharacterForStatus.charName,
                    level = selectedCharacterForStatus.charLevel,
                    experienceToNextLevel = 50,
                    hp = selectedCharacterForStatus.maxHp,
                    damage = selectedCharacterForStatus.dmgValue,
                    armor = selectedCharacterForStatus.baseArmor
                };
                characterStats.stats.Clear();
                characterStats.stats.Add(selectedCharacterForStatus.charName, characterStatsData);
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
            CharacterStats.charStats charData = data.stats[selectedCharacterForStatus.charName];

            charName.text = charData.charName;

            health.text = charData.hp.ToString("f0");
            attack.text = charData.damage.ToString("f0");
            defense.text = charData.armor.ToString("f0");
            level.text = charData.level.ToString();

        }
        catch (Exception e)
        {
            Debug.Log(e.Message + " " + e.StackTrace);
        }
    }



    public void EquipHero()
    {
        CanvasManager.instance.CloseScreenHeroeStatus(gameObject);
        SquadManager.instance.GetSpecialHeroCharacterData(selectedCharacter);
        specialCharacterData.charName = selectedCharacter.charName;
        if (DataService.SaveData(saveDataPathSelectedSquad, specialCharacterData, EncryptionEnabled))
        {
            Debug.Log("Data Save");
            SpecialCharacterData data = DataService.LoadData<SpecialCharacterData>(saveDataPathSelectedSquad, EncryptionEnabled);
            Debug.Log("Name " + data.charName);
        }
        else
        {
            Debug.Log("Can't Save Data");
        }

    }





}
