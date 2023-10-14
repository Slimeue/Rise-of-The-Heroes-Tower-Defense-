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

    [SerializeField] Image charArtWork;
    [SerializeField] TextMeshProUGUI charName;
    [SerializeField] TextMeshProUGUI level;
    [SerializeField] TextMeshProUGUI health;
    [SerializeField] TextMeshProUGUI attack;
    [SerializeField] TextMeshProUGUI defense;

    CanvasManager canvasManager;

    private void Awake()
    {
        canvasManager = FindObjectOfType<CanvasManager>();

    }

    private void Update()
    {
        if (canvasManager.selectedHero != null)
        {
            selectedCharacter = canvasManager.selectedHero;
            selectedCharacterForStatus = canvasManager.selectedHeroForStatus;
            charArtWork.sprite = selectedCharacterForStatus.charArtWork;
            charName.text = selectedCharacterForStatus.charName;
            //TODO Level
            health.text = selectedCharacterForStatus.maxHp.ToString();
            attack.text = selectedCharacterForStatus.dmgValue.ToString();
            defense.text = selectedCharacterForStatus.baseArmor.ToString();
        }
    }

    public void EquipHero()
    {
        CanvasManager.instance.CloseScreenHeroeStatus(gameObject);
        SquadManager.instance.GetSpecialHeroCharacterData(selectedCharacter);
    }





}
