using System.Collections;
using System.Collections.Generic;
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
        SpecialCharacterData data = DataService.LoadData<SpecialCharacterData>(saveDataPath, EncryptionEnabled);

        for (int i = 0; i < sCSquadHolder.characterDatas.Length; i++)
        {
            if (data.charName == sCSquadHolder.characterDatas[i].charName)
            {
                selectedSpecialHero = sCSquadHolder.characterDatas[i];
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

    private void Update()
    {

        if (selectedSpecialHero != null)
        {
            Debug.Log(selectedSpecialHero);

        }
        else return;



    }

    public CharacterData GetSpecialHeroCharacterData(CharacterData characterData)
    {
        selectedSpecialHero = characterData;
        return characterData;
    }





}
