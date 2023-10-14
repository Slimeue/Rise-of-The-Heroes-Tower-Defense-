using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HeroSelection : MonoBehaviour
{

    public CharacterData selectedCharacter;
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

            charArtWork.sprite = selectedCharacter.charArtWork;
            charName.text = selectedCharacter.charName;
            //TODO Level
            health.text = selectedCharacter.maxHp.ToString();
            attack.text = selectedCharacter.dmgValue.ToString();
            defense.text = selectedCharacter.baseArmor.ToString();


        }
    }





}
