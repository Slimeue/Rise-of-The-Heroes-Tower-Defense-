using System.Collections;
using System.Collections.Generic;
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

    string saveDataPath = "/data-data.json";

    private SpecialCharacterData specialCharacterData = new SpecialCharacterData();

    bool EncryptionEnabled;

    private void Awake()
    {
        canvasManager = FindObjectOfType<CanvasManager>();

    }

    private void Update()
    {
        if (canvasManager.selectedHero != null || canvasManager.selectedHeroForStatus != null)
        {
            selectedCharacter = canvasManager.selectedHero;
            selectedCharacterForStatus = canvasManager.selectedHeroForStatus;
            charArtWork.sprite = selectedCharacterForStatus.charArtWork;
            charName.text = selectedCharacterForStatus.charName;
            level.text = selectedCharacter.charLevel.ToString();
            health.text = selectedCharacterForStatus.maxHp.ToString();
            attack.text = selectedCharacterForStatus.dmgValue.ToString();
            defense.text = selectedCharacterForStatus.baseArmor.ToString();
        }
    }

    public void EquipHero()
    {
        CanvasManager.instance.CloseScreenHeroeStatus(gameObject);
        SquadManager.instance.GetSpecialHeroCharacterData(selectedCharacter);
        specialCharacterData.charName = selectedCharacter.charName;
        if (DataService.SaveData(saveDataPath, specialCharacterData, EncryptionEnabled))
        {
            Debug.Log("Data Save");
            SpecialCharacterData data = DataService.LoadData<SpecialCharacterData>(saveDataPath, EncryptionEnabled);
            Debug.Log("Name " + data.charName);
        }
        else
        {
            Debug.Log("Can't Save Data");
        }

    }





}
