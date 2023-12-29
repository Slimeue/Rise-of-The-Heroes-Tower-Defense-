using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public static bool isGamePause;

    [SerializeField] GameObject pauseMenuScreen;
    [SerializeField] GameObject restartConfirmationPanel;
    [SerializeField] GameObject pauseObj;
    [SerializeField] GameObject optionSettings;
    [SerializeField] GameObject soundSettings;
    [SerializeField] GameObject videoSettings;

    bool isInConfirmation;


    private void Awake()
    {
        pauseMenuScreen.SetActive(false);
        restartConfirmationPanel.SetActive(false);
        optionSettings.SetActive(false);
        soundSettings.SetActive(false);
        videoSettings.SetActive(false);
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
            pauseObj.SetActive(true);
            restartConfirmationPanel.SetActive(false);
            isInConfirmation = false;
        }
        else
        {//Open
            isInConfirmation = true;
            pauseObj.SetActive(false);
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


    public void OptionOpen()
    {
        optionSettings.SetActive(true);
        pauseObj.SetActive(false);
    }

    public void OptionClose()
    {
        optionSettings.SetActive(false);
        pauseObj.SetActive(true);
    }

    public void SoundSettingOpen()
    {
        optionSettings.SetActive(false);
        soundSettings.SetActive(true);
    }

    public void SoundSettingsClose()
    {
        optionSettings.SetActive(true);
        soundSettings.SetActive(false);
    }

    public void VideoSettingsOpen()
    {
        optionSettings.SetActive(false);
        videoSettings.SetActive(true);
    }
    public void VideoSettingsClose()
    {
        optionSettings.SetActive(true);
        videoSettings.SetActive(false);
    }
}
