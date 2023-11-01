using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class SpecialCharacterManager : MonoBehaviour
{

    public Button[] towerHolder;
    public Button[] heroesTowerHolder;

    private IDataService DataService = new JsonDataService();

    string saveDataPath = "/data-stageProgress.json";

    StageDataModel stageDataModel = new StageDataModel();

    //
    int stageReached;
    int stageCounter;
    int chapterReached;

    [SerializeField] Color lockedColor;

    private void Awake()
    {
        SaveLoadData();
    }

    // Start is called before the first frame update
    void Start()
    {
        CheckCharacterLocked();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void SaveLoadData()
    {
        string path = Application.persistentDataPath + saveDataPath;
        if (!File.Exists(path))
        {

            stageDataModel.chapterReached = 1;
            stageDataModel.stageCounter = 1;
            stageDataModel.stageReached = 1;

            chapterReached = stageDataModel.chapterReached;
            stageReached = stageDataModel.stageReached;
            stageCounter = stageDataModel.stageCounter;

            DataService.SaveData(saveDataPath, stageDataModel, false);
            return;
        }

        StageDataModel stageData = DataService.LoadData<StageDataModel>(saveDataPath, false);

        chapterReached = stageData.chapterReached;
        stageReached = stageData.stageReached;
        stageCounter = stageData.stageCounter;

    }

    private void CheckCharacterLocked()
    {
        for (int i = 0; i < towerHolder.Length; i++)
        {
            SpecialHeroHolder specialHeroHolder = towerHolder[i].GetComponent<SpecialHeroHolder>();
            MythicHeroHolder mythicHeroHolder = heroesTowerHolder[i].GetComponent<MythicHeroHolder>();
            if (i >= chapterReached)
            {
                heroesTowerHolder[i].interactable = false;
                mythicHeroHolder.lockedChain.SetActive(true);
                mythicHeroHolder.charArtWork.color = lockedColor;
                mythicHeroHolder.charName.color = lockedColor;
                //SpecialHolder
                towerHolder[i].interactable = false;
                specialHeroHolder.chainLocked.SetActive(true);
                specialHeroHolder.charArtWork.color = lockedColor;
                specialHeroHolder.charName.color = lockedColor;

            }
        }
    }
}
