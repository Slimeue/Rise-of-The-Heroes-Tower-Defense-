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

    private void Awake()
    {
        canvasManager = FindAnyObjectByType<CanvasManager>();
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


        SceneManager.LoadScene(scene);

    }

    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }


}
