using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StoryModel
{
    public struct storyModel
    {
        public string storyId;
        public int storyValue;
        public bool isCompleted;
    }

    public Dictionary<string, storyModel> story = new();

}
