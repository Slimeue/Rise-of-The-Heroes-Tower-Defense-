using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HeroesBookInfoModel : MonoBehaviour
{

    CanvasManager canvasManager;

    CharacterData characterData;

    [Space(5)]
    [Header("Lore")]
    [SerializeField] Image loreCharImage;
    [SerializeField] TextMeshProUGUI loreCharName;
    [SerializeField] TextMeshProUGUI loreCharPart1;
    [SerializeField] TextMeshProUGUI loreCharPart2;
    [SerializeField] TextMeshProUGUI loreCharPart3;


    [Space(5)]
    [Header("Personality")]
    [SerializeField] Image personalityCharImage;
    [SerializeField] TextMeshProUGUI personalityCharName;
    [SerializeField] TextMeshProUGUI personalityCharPart1;
    [SerializeField] TextMeshProUGUI personalityCharPart2;
    [SerializeField] TextMeshProUGUI personalityCharPart3;







    private void Awake()
    {
        canvasManager = FindObjectOfType<CanvasManager>();
    }

    // Start is called before the first frame update
    void OnEnable()
    {
        if (canvasManager.selectedHeroesForBookInfo == null)
        {
            return;
        }
        characterData = canvasManager.selectedHeroesForBookInfo;

        //
        loreCharImage.sprite = characterData.charArtWork;
        loreCharName.text = characterData.charName;
        personalityCharImage.sprite = characterData.charArtWork;
        personalityCharName.text = characterData.charName;

        loreCharPart1.text = characterData.charInfoData.lorePart1;
        loreCharPart2.text = characterData.charInfoData.lorePart2;
        loreCharPart3.text = characterData.charInfoData.lorePart3;

        personalityCharPart1.text = characterData.charInfoData.personalityPart1;
        personalityCharPart2.text = characterData.charInfoData.personalityPart2;
        personalityCharPart3.text = characterData.charInfoData.personalityPart3;

    }


}
