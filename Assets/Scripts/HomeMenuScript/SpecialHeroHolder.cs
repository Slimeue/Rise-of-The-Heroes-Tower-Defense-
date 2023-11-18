using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpecialHeroHolder : MonoBehaviour
{
    [SerializeField] public CharacterData characterData;
    [SerializeField] public Image charArtWork;
    [SerializeField] public GameObject chainLocked;
    [SerializeField] public TextMeshProUGUI charName;

    public bool isUnlocked;

    private void Awake()
    {
        charArtWork.sprite = characterData.charArtWork;
        charName.text = characterData.charName;
        chainLocked.SetActive(true);
    }

}
