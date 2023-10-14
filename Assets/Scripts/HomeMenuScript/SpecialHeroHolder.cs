using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpecialHeroHolder : MonoBehaviour
{
    [SerializeField] public CharacterData characterData;
    [SerializeField] Image charArtWork;
    [SerializeField] TextMeshProUGUI charName;

    private void Awake()
    {
        charArtWork.sprite = characterData.charArtWork;
        charName.text = characterData.charName;
    }

}
