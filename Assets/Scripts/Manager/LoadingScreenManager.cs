using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreenManager : MonoBehaviour
{
    public GameObject bgLoadingScreen;
    public Image bgImage;
    public Slider slider;
    public TextMeshProUGUI progressText;

    public Sprite[] backgrounds;
    [TextArea(1, 5)] public string[] tips;



    public void LoadLevel(string sceneName)
    {
        StartCoroutine(LoadAsynch(sceneName));
    }

    IEnumerator LoadAsynch(string sceneName)
    {
        bgImage.sprite = backgrounds[Random.Range(0, backgrounds.Length)];
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false;
        bgLoadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);

            slider.value = progress;
            progressText.text = "Loading " + (progress * 100f).ToString("F2") + "%";

            if (progress >= 0.9f)
            {
                operation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
