using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelUI : MonoBehaviour
{
    private float timeRemaining;
    private bool timeIsRUnning;

    [SerializeField] private TextMeshProUGUI timer;
    [SerializeField] private TextMeshProUGUI gemsCounter;
    [SerializeField] private TextMeshProUGUI score;

    private void Start()
    {
        if (timer != null)
            timer.text = "<sprite=0>000";

        if (gemsCounter != null)
            gemsCounter.text = "<sprite=1>0/0";

        GameStats.GemsIncreased += UpdateGemsCounter;
        GameStats.TimeUpdated += UpdateTimer;
    }

    public void SetTime(float time)
    {
        timeRemaining = time;
    }

    public void StartTimer()
    {
        timeIsRUnning = true;
    }

    private void UpdateTimer()
    {
        if(timer != null)
            timer.text = "<sprite=0>" + GameController.Instance.GameStats.TimeRemaining;
    }

    private void UpdateGemsCounter()
    {
        if (gemsCounter != null)
            gemsCounter.text = "<sprite=1>" + GameController.Instance.GameStats.GemsCollected + "/" + GameController.Instance.GameStats.GemsNeeded;

        if (score != null)
            score.text = GameController.Instance.GameStats.Score.ToString();
    }

    private void OnDestroy()
    {
        GameStats.GemsIncreased -= UpdateGemsCounter;
        GameStats.TimeUpdated -= UpdateTimer;
    }

    public void ShowElements(bool state)
    {
        timer.gameObject.SetActive(state);
        score.gameObject.SetActive(state);
        gemsCounter.gameObject.SetActive(state);
    }
}
