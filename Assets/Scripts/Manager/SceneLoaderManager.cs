using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoaderManager : MonoBehaviour
{

    public StageVineRef stageVineRef;

    public void LoadScene(string scene)
    {

        SceneManager.LoadScene(scene);
    }
}
