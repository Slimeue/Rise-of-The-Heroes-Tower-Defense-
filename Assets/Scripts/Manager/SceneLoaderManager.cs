using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoaderManager : MonoBehaviour
{

    public StageVineRef stageVineRef;

    CanvasManager canvasManager;

    private IDataService DataService = new JsonDataService();

    string saveDataPath = "/data-squad.json";

    private SpecialCharacterData specialCharacterData = new SpecialCharacterData();

    bool EncryptionEnabled;

    LoadingScreenManager loadingScreenManager;

    private void Awake()
    {
        canvasManager = FindAnyObjectByType<CanvasManager>();
        loadingScreenManager = FindObjectOfType<LoadingScreenManager>();
    }

    public void LoadStageScene(string scene)
    {

        Debug.Log("Wat");

        string path = Application.persistentDataPath + saveDataPath;
        if (!File.Exists(path))
        {
            canvasManager.SquadButton();
            canvasManager.CloseStageSelection();
            return;
        }


        loadingScreenManager.LoadLevel(scene);

    }

    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }


}
