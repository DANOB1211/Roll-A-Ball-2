using System.Collections;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using TMPro;


public class Timer : MonoBehaviour
{
    public float currentTime;
    float bestTime;
    bool timing = false;


    

    SceneController sceneController;

    [Header("UI Countdown Panel")]
    public GameObject countdownPanel;
    public TextMeshProUGUI countdownText;

    [Header("UI In Game Panel")]
    public TextMeshProUGUI timerText;

    [Header("UI Game Over Panel")]
    public GameObject timesPanel;
    public TextMeshProUGUI myTimeResult;
    public TextMeshProUGUI bestTimeResult;


    // Update is called once per frame
    private void Start()
    {
        timesPanel.SetActive(false);
        countdownPanel.SetActive(false);
        timerText.text = "";
        sceneController = FindObjectOfType<SceneController>();
    }

    void Update()
    {
        if(timing)
        {
            currentTime += Time.deltaTime;
            timerText.text = currentTime.ToString("F5");
        }
       
    }

    public float GetTime()
    {
        return currentTime;
    }

    public IEnumerator StartCountdown()
    {
        yield return new WaitForEndOfFrame();
        if (PlayerPrefs.HasKey("BestTime"))
            bestTime = PlayerPrefs.GetFloat("BestTime" + sceneController.GetSceneName());
        else
            bestTime = 1000f;

        countdownPanel.SetActive(true);
        countdownText.text = "5";
        yield return new WaitForSeconds(1);
        countdownText.text = "4";
        yield return new WaitForSeconds(1);
        countdownText.text = "3";
        yield return new WaitForSeconds(1);
        countdownText.text = "2";
        yield return new WaitForSeconds(1);
        countdownText.text = "1";
        yield return new WaitForSeconds(1);
        countdownText.text = "Go!";
        yield return new WaitForSeconds(1);
        StartTimer();
        countdownPanel.SetActive(false);
    }
    public void StartTimer()
    {
        timing = true;
        currentTime = 0;
    }

    public void StopTimer()
    {
        timing = false;
        timesPanel.SetActive(true);
        myTimeResult.text = currentTime.ToString("F5");
        myTimeResult.text = bestTime.ToString("F5");

        if (currentTime <= bestTime)
        {
            bestTime = currentTime;
            PlayerPrefs.SetFloat("BestTime" + sceneController.GetSceneName(), bestTime);
            bestTimeResult.text = bestTime.ToString("F3") + "!! NEW BEST !!";
        }
    }

    public bool isTiming()
    {
        return timing;
    }
}
