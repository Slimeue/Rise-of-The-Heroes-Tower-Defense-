using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreaturesBookInfoModel : MonoBehaviour
{

    CanvasManager canvasManager;

    EnemiesData enemiesData;

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
        if (canvasManager.selectedEnemyForBookInfo == null)
        {
            return;
        }
        enemiesData = canvasManager.selectedEnemyForBookInfo;

        //
        loreCharImage.sprite = enemiesData.enemyArtWork;
        loreCharName.text = enemiesData.enemyName;
        personalityCharImage.sprite = enemiesData.enemyArtWork;
        personalityCharName.text = enemiesData.enemyName;


        loreCharPart1.text = enemiesData.enemiesInfoData.lorePart1;
        loreCharPart2.text = enemiesData.enemiesInfoData.lorePart2;
        loreCharPart3.text = enemiesData.enemiesInfoData.lorePart3;

        personalityCharPart1.text = enemiesData.enemiesInfoData.personalityPart1;
        personalityCharPart2.text = enemiesData.enemiesInfoData.personalityPart2;
        personalityCharPart3.text = enemiesData.enemiesInfoData.personalityPart3;

    }



}
