using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterBoWManger : MonoBehaviour
{
    [SerializeField] CharacterData characterData;

    IDataService dataService = new JsonDataService();
    string saveDataPath = "/character-data"; //TODO static reference

    public bool isUnlocked;
    [SerializeField] Color unlockedColor;
    //UI
    [SerializeField] GameObject chain;
    [SerializeField] Image charImage;
    [SerializeField] TextMeshProUGUI charName;

    Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.interactable = false;
    }

    private void OnEnable()
    {
        LoadData();

        charImage.sprite = characterData.charArtWork;
        charName.text = characterData.charName;
        if (isUnlocked)
        {
            chain.SetActive(false);
            charImage.color = unlockedColor;
            button.interactable = true;
        }
    }



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
