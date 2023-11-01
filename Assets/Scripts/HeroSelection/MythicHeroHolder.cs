using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MythicHeroHolder : MonoBehaviour
{
    [SerializeField] public CharacterData characterData;
    [SerializeField] public Image charArtWork;
    [SerializeField] public TextMeshProUGUI charName;
    [SerializeField] public GameObject lockedChain;

    private void Awake()
    {
        charArtWork.sprite = characterData.charArtWork;
        charName.text = characterData.charName;

        if (lockedChain != null)
        {
            lockedChain.SetActive(false);
        }

    }
}
