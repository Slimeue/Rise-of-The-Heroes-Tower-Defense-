using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroCardHolder : MonoBehaviour
{

    [SerializeField] CharacterData characterData;
    [SerializeField] Image charImage;



    private void Awake()
    {
        charImage.sprite = characterData.charArtWork;
    }

}
