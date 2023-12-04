using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TriviaWindow : MonoBehaviour
{


    [SerializeField] CharacterData characterData;
    [SerializeField] TextMeshProUGUI triviaText;
    [SerializeField] Image charImage;

    SpecialHeroSpawn specialHeroSpawn;



    // Start is called before the first frame update
    void Start()
    {
        specialHeroSpawn = FindObjectOfType<SpecialHeroSpawn>();
        characterData = specialHeroSpawn.characterData;
        int randomNum = Random.Range(0, characterData.charInfoData.charTrivias.Length);
        triviaText.text = characterData.charInfoData.charTrivias[randomNum];
        charImage.sprite = characterData.charArtWork;

    }



    // Update is called once per frame
    void Update()
    {

    }
}
