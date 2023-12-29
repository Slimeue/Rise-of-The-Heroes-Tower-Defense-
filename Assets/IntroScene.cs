using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroScene : MonoBehaviour
{

    LoadingScreenManager loadingScreenManager;

    private void Awake()
    {
        loadingScreenManager = FindObjectOfType<LoadingScreenManager>();

    }
    private void Start()
    {
        TransitionToScene();
    }

    public void TransitionToScene()
    {
        SceneManager.LoadScene("MainMenuScreen");
    }
}
