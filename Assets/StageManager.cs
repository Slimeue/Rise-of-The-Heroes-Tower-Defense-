using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{



    public StageData[] stageDatas;


    public Color color;

    Button[] buttons;

    [SerializeField]
    private List<Button> buttonList = new List<Button>();

    private IDataService DataService = new JsonDataService();

    string saveDataPath = "/data-stageProgress.json";

    StageDataModel stageDataModel = new StageDataModel();


    //
    int stageReached;
    int stageCounter;
    int chapterReached;

    private void Awake()
    {

        SaveLoadData();

    }

    private void Start()
    {
        StageDataLoader();
    }

    private void Update()
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

    private void StageDataLoader()
    {
        for (int i = 0; i < stageDatas.Length; i++)
        {
            TabButtons tabButton = stageDatas[i].chapter.gameObject.GetComponent<TabButtons>();
            StageVineRef chapterVineRef = stageDatas[i].chapter.gameObject.GetComponent<StageVineRef>();
            if (i >= chapterReached)
            {
                stageDatas[i].chapter.interactable = false;
                chapterVineRef.vineSprite.SetActive(true);
                tabButton.enabled = false;
                if (chapterVineRef.title != null)
                {
                    chapterVineRef.title.color = color;
                }
            }



            buttons = stageDatas[i].stages;

            buttonList.AddRange(buttons);



            for (int j = 0; j < buttonList.Count; j++)
            {
                StageVineRef stageVineRef = buttonList[j].GetComponent<StageVineRef>();

                if (j >= stageReached)
                {
                    stageVineRef.vineSprite.SetActive(true);
                    buttonList[j].interactable = false;
                }

                if (stageReached == 1)
                {
                    stageVineRef.isCompleted = false;
                }
                else if (j < stageReached - 1)
                {
                    stageVineRef.isCompleted = true;
                    stageVineRef.stageCompeletText.SetActive(true);
                }

            }






        }
    }
}
