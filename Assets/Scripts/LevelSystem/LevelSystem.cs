using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSystem
{

    public event EventHandler OnExperienceChange;
    public event EventHandler OnLevelChange;

    private int level;
    private int experience;
    private float experienceToNextLevel;

    public LevelSystem()
    {
        level = 0;
        experience = 0;
        experienceToNextLevel = 50;
    }

    public void LevelUp(float amount, float experienceToNextLevelValue)
    {
        if (amount >= experienceToNextLevel)
        {
            level++;
            experienceToNextLevel += experienceToNextLevelValue * 2;
        }
    }

    public int GetLevelNumber()
    {
        return level;
    }

    public float GetExperienceToNextLevelNumber()
    {
        return experienceToNextLevel;
    }


}
