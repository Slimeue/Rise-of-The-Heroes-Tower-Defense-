using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SquadManager : MonoBehaviour
{
    public static SquadManager instance;

    [SerializeField] public CharacterData selectedSpecialHero { get; private set; }


    SCSquadHolder sCSquadHolder;

    private IDataService DataService = new JsonDataService();

    string saveDataPath = "/data-squad.json";

    private SpecialCharacterData specialCharacterData = new SpecialCharacterData();

    bool EncryptionEnabled;

    private void Awake()
    {
        CreateSquadManager();
        sCSquadHolder = FindObjectOfType<SCSquadHolder>();
        string path = Application.persistentDataPath + saveDataPath;
        if (File.Exists(path))
        {
            SpecialCharacterData data = DataService.LoadData<SpecialCharacterData>(saveDataPath, EncryptionEnabled);
            for (int i = 0; i < sCSquadHolder.characterDatas.Length; i++)
            {
                if (data.charName == sCSquadHolder.characterDatas[i].charName)
                {
                    selectedSpecialHero = sCSquadHolder.characterDatas[i];
                }
            }
        }



    }

    private void CreateSquadManager()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }


    public CharacterData GetSpecialHeroCharacterData(CharacterData characterData)
    {
        selectedSpecialHero = characterData;
        return characterData;
    }

    public GameObject SelectedHero()
    {
        return gameObject;
    }





}
