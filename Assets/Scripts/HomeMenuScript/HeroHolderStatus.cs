using System.Collections;
using System.Collections.Generic;
using TMPro;
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



    private void OnEnable()
    {
        if (CanvasManager.instance.heroesStatusCharacterData != null)
        {
            characterData = CanvasManager.instance.heroesStatusCharacterData;
            charArtWork.sprite = characterData.charArtWork;
            foreach (TextMeshProUGUI _charName in charName)
            {
                _charName.text = characterData.charName;
            }
            charHealth.text = "Health: " + characterData.maxHp;
            charAttack.text = "Attack: " + characterData.dmgValue;
            charDefense.text = "Defense: " + characterData.baseArmor;
            charLevel.text = "Lvl: " + characterData.charLevel;
            charLore.text = characterData.charLore;
        }
        else return;

    }


}
