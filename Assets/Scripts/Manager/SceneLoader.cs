using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

    public Transform title;

    public GameObject startButton;
    public GameObject mainMenu;
    public GameObject menuCanvas;

    //Options

    [SerializeField] GameObject SettingsCanvas;
    [SerializeField] GameObject SettingCanvasBackButton;
    [SerializeField] GameObject SettingsButtons;

    //Video

    [SerializeField] GameObject VideSetting;

    //Sound

    [SerializeField] GameObject SoundSetting;



    private void Awake()
    {
        SettingsCanvas.SetActive(false);
        VideSetting.SetActive(false);
        SoundSetting.SetActive(false);
    }

    public void OpenSettings()
    {
        SettingsCanvas.SetActive(true);
        menuCanvas.SetActive(false);
    }

    public void CloseSettings()
    {
        SettingsCanvas.SetActive(false);
        menuCanvas.SetActive(true);
    }

    public void OpenVideoSetting()
    {
        VideSetting.SetActive(true);
        SettingsButtons.SetActive(false);
    }

    public void CloseVideoSetting()
    {
        SettingsButtons.SetActive(true);
        VideSetting.SetActive(false);
    }

    public void OpenSoundSetting()
    {
        SoundSetting.SetActive(true);
        SettingsButtons.SetActive(false);
    }

    public void CloseSoundSetting()
    {
        SettingsButtons.SetActive(true);
        SoundSetting.SetActive(false);
    }

    private void Start()
    {
        if (mainMenu != null)
        {
            mainMenu.transform.localScale = Vector2.zero;
        }

    }

    public void MainMenuStart()
    {
        title.transform.LeanMoveLocal(new Vector2(title.transform.position.x, 200), 2).setEaseInOutExpo();

        title.transform.LeanScale(new Vector3(1f, 1f, 1f), 2).setEaseInOutExpo();

        // mainMenu.SetActive(true);

        mainMenu.transform.LeanScale(Vector3.one, 0.8f);

        startButton.SetActive(false);
    }

    public void AppExit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }


    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
