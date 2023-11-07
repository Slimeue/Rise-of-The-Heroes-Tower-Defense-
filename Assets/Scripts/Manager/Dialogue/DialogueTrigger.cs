using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{

    public SO_Dialogue_Messages[] conversation;

    int stageReached;
    int stageCounter;
    int chapterReached;

    private IDataService DataService = new JsonDataService();

    string saveDataPath = "/data-stageProgress.json";

    StageDataModel stageDataModel = new StageDataModel();

    public void StartDialogue()
    {


        foreach (SO_Dialogue_Messages messages in conversation)
        {
            if (stageReached == messages.storyValue)
            {
                SO_Dialogue_Messages latestConversation = messages;
                FindAnyObjectByType<DialogueManager>().OpenDialogue(latestConversation);
            }
        }



    }

    private void OnEnable()
    {
        LoadData();
        StartDialogue();
    }





    private void LoadData()
    {
        string path = Application.persistentDataPath + saveDataPath;

        StageDataModel stageData = DataService.LoadData<StageDataModel>(saveDataPath, false);

        chapterReached = stageData.chapterReached;
        stageReached = stageData.stageReached;
        stageCounter = stageData.stageCounter;

    }

}
