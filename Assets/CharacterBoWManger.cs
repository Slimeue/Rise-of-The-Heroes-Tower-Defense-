using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CharacterBoWManger : MonoBehaviour
{
    [SerializeField] CharacterData characterData;

    IDataService dataService = new JsonDataService();
    string saveDataPath = "/character-data"; //TODO static reference

    public bool isUnlocked;

    void LoadData()
    {
        string newSaveDataPath = $"{saveDataPath}-{characterData.charName}.json";

        string path = Application.persistentDataPath + newSaveDataPath;
        if (!File.Exists(path))
        {
            return;
        }

        CharacterStats data = dataService.LoadData<CharacterStats>(newSaveDataPath, false);

        isUnlocked = true;

    }
}
