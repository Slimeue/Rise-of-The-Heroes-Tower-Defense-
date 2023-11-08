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
    string storyProgressDataPath = "/data-storyProgression.json";

    StageDataModel stageDataModel = new StageDataModel();

    StoryModel story = new StoryModel();

    // public void StartDialogue()
    // {
    //     foreach (SO_Dialogue_Messages messages in conversation)
    //     {


    //         if (stageReached == messages.storyValue && !messages.isCompleted)
    //         {
    //             SO_Dialogue_Messages latestConversation = messages;
    //             FindAnyObjectByType<DialogueManager>().OpenDialogue(latestConversation);
    //         }
    //     }
    // }

    private void OnEnable()
    {
        LoadData();
        StartLoadDialogue();
    }

    private void StartLoadDialogue()
    {
        string path = Application.persistentDataPath + storyProgressDataPath;

        if (File.Exists(path))
        {
            bool firstStory = false;
            StoryModel storyLoad = DataService.LoadData<StoryModel>(storyProgressDataPath, false);
            foreach (SO_Dialogue_Messages conversations in conversation)
            {
                StoryModel.storyModel storyModel = new StoryModel.storyModel
                {
                    storyId = conversations.storyID,
                    storyValue = conversations.storyValue,
                    isCompleted = conversations.isCompleted
                };

                if (!storyLoad.story.ContainsKey(conversations.name))
                {
                    storyLoad.story.Add(conversations.name, storyModel);
                    DataService.SaveData(storyProgressDataPath, storyLoad, false);
                }



                if (stageReached == storyLoad.story[conversations.name].storyValue && !storyLoad.story[conversations.name].isCompleted)
                {
                    if (!firstStory)
                    {
                        SO_Dialogue_Messages latestConversation = conversations;
                        FindAnyObjectByType<DialogueManager>().OpenDialogue(latestConversation);
                        firstStory = true;
                    }

                }



            }
        }
        else
        {
            foreach (SO_Dialogue_Messages item in conversation)
            {
                StoryModel.storyModel storyModel = new StoryModel.storyModel
                {
                    storyId = item.storyID,
                    storyValue = item.storyValue,
                    isCompleted = item.isCompleted
                };

                if (!story.story.ContainsKey(item.name))
                {
                    story.story.Add(item.name, storyModel);
                    DataService.SaveData(storyProgressDataPath, story, false);
                }

            }
        }




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
