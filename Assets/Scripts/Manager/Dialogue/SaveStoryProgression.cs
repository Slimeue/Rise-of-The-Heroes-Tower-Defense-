using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveStoryProgression
{

    public static SaveStoryProgression instance = new SaveStoryProgression();

    private IDataService DataService = new JsonDataService();
    string storyProgressDataPath = "/data-storyProgression.json";

    public void SaveLoadStory(SO_Dialogue_Messages conversation)
    {
        string path = Application.persistentDataPath + storyProgressDataPath;

        StoryModel storyLoad = DataService.LoadData<StoryModel>(storyProgressDataPath, false);


        StoryModel.storyModel storyModel = new StoryModel.storyModel
        {
            storyId = conversation.storyID,
            storyValue = conversation.storyValue,
            isCompleted = conversation.isCompleted
        };

        if (!storyLoad.story.ContainsKey(conversation.name))
        {
            storyLoad.story.Add(conversation.name, storyModel);
        }
        else
        {
            storyLoad.story[conversation.name] = storyModel;
        }

        DataService.SaveData(storyProgressDataPath, storyLoad, false);


    }

}
