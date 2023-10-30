
using TMPro;
using UnityEngine;

public class FPSDisplay : MonoBehaviour
{

    public TextMeshProUGUI fpsText;
    float pollingTime = 1f;
    float time;
    int frameCount;

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        frameCount++;

        if (time >= pollingTime)
        {
            int frameRate = Mathf.RoundToInt(frameCount / time);
            fpsText.text = "FPS: " + frameRate;

            time -= pollingTime;
            frameCount = 0;
        }
    }
}
