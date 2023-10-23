using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectedSquadDisplay : MonoBehaviour
{
    [Space(5)]
    [Header("Special Hero")]
    [SerializeField] public CharacterData characterData;
    [SerializeField] Image charArtWork;
    [SerializeField] TextMeshProUGUI charName;


    private void Awake()
    {

    }

    private void Update()
    {
        if (SquadManager.instance.selectedSpecialHero == null)
        {
            return;
        }
        else
        {
            characterData = SquadManager.instance.selectedSpecialHero;
            charArtWork.sprite = characterData.charArtWork;
            charName.text = characterData.charName;
        }

    }
}

