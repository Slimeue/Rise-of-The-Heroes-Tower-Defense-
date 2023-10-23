using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public static bool isGamePause;

    [SerializeField] GameObject pauseMenuScreen;
    [SerializeField] GameObject restartConfirmationPanel;

    bool isInConfirmation;


    private void Awake()
    {
        pauseMenuScreen.SetActive(false);
        restartConfirmationPanel.SetActive(false);
        isGamePause = false;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ResumeGame()
    {
        pauseMenuScreen.SetActive(false);
        isGamePause = false;
    }

    public void PauseGame()
    {
        pauseMenuScreen.SetActive(true);
        Time.timeScale = 0f;
        isGamePause = true;
    }

    public void OpenRestartConfirmation()
    {
        if (isInConfirmation)
        {//Closes
            restartConfirmationPanel.SetActive(false);
            isInConfirmation = false;
        }
        else
        {//Open
            isInConfirmation = true;
            restartConfirmationPanel.SetActive(true);
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        isGamePause = false;
    }

    public void ExitGame(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        Time.timeScale = 1f;
        isGamePause = false;
    }

}
