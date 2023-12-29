using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageCompletedManager : MonoBehaviour
{

    public static StageCompletedManager instance;


    public bool isCompleted;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }

    public bool StageCompleted(StageVineRef stageVineRef)
    {
        isCompleted = stageVineRef.isCompleted;
        Debug.Log("Stage is " + isCompleted);
        return isCompleted;
    }
}
