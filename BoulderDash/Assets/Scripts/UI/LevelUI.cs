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
    [SerializeField] private TextMeshProUGUI lifes;

    private void Start()
    {
        if (timer != null)
            timer.text = "<sprite=0>000";

        if (gemsCounter != null)
            gemsCounter.text = "<sprite=1>0/0";

        GameStats.GemsUpdated += UpdateGemsCounter;
        GameStats.TimeUpdated += UpdateTimer;
        GameStats.LifesUpdated += UpdateLifeCounter;
        GameStats.ValuesInitialized += SetAllValues;
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
        if (timer != null)
        {
            string color = GameController.Instance.GameStats.TimeRemaining > 10 ? "<color=white>" : "<color=red>";
            timer.text = color + "<sprite=0>" + GameController.Instance.GameStats.TimeRemaining + "</color>";
        }
    }

    private void UpdateGemsCounter()
    {
        if (gemsCounter != null)
        {
            string color = GameController.Instance.GameStats.GemsCollected >= GameController.Instance.GameStats.GemsNeeded ? "<color=#00FFFF>" : "<color=white>";
            gemsCounter.text = color + "<sprite=1>" + GameController.Instance.GameStats.GemsCollected + "/" + GameController.Instance.GameStats.GemsNeeded + "</color>";
        }

        if (score != null)
            score.text = GameController.Instance.GameStats.Score.ToString();
    }

    private void UpdateLifeCounter()
    {
        if (lifes != null)
        {
            string lifesRemaining = "";
            for (int i = 0; i < GameController.Instance.GameStats.Lifes; i++)
                lifesRemaining += "<sprite=2>";

            lifes.text = lifesRemaining;
        }
    }

    private void SetAllValues()
    {
        UpdateGemsCounter();
        UpdateLifeCounter();
    }

    private void OnDestroy()
    {
        GameStats.GemsUpdated -= UpdateGemsCounter;
        GameStats.TimeUpdated -= UpdateTimer;
        GameStats.LifesUpdated -= UpdateLifeCounter;
        GameStats.ValuesInitialized -= SetAllValues;
    }

    public void ShowElements(bool state)
    {
        timer.gameObject.SetActive(state);
        score.gameObject.SetActive(state);
        gemsCounter.gameObject.SetActive(state);
        lifes.gameObject.SetActive(state);
    }
}
