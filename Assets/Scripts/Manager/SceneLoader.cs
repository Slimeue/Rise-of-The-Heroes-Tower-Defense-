using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

    public Transform title;

    public GameObject startButton;
    public GameObject mainMenu;



    private void Start()
    {
        if (mainMenu != null)
        {
            mainMenu.transform.localScale = Vector2.zero;
        }

    }

    public void MainMenuStart()
    {
        title.transform.LeanMoveLocal(new Vector2(title.transform.position.x, 300), 2).setEaseInOutExpo();

        title.transform.LeanScale(new Vector3(1.5f, 1.5f, 1f), 2).setEaseInOutExpo();

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
