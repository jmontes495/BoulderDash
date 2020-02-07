using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStats : MonoBehaviour
{
    public delegate void StatsChange();
    public static event StatsChange GemsIncreased;
    public static event StatsChange LifeLost;
    public static event StatsChange TimeUpdated;

    public static int gemValue = 100;
    private int lifes;
    private int gemsNeeded;
    private int gemsCollected;
    private int score;
    private float timeRemaining;
    private bool timeIsRUnning;

    public int TimeRemaining
    {
        get { return (int) timeRemaining; }
    }

    public int Score
    {
        get { return score; }
    }

    public int GemsNeeded
    {
        get { return gemsNeeded; }
    }

    public int GemsCollected
    {
        get { return gemsCollected; } 
    }

    public void Initialize(int playerLifes, int gemsNeededForLevel)
    {
        lifes = playerLifes;
        gemsNeeded = gemsNeededForLevel;
        gemsCollected = 0;
        score = 0;
        GemsIncreased();
    }

    public void IncreaseGems()
    {
        gemsCollected++;
        score += gemValue;
        GemsIncreased();
    }

    public void LoseLife()
    {
        lifes--;
        LifeLost();
        if (lifes == 0)
            GameController.Instance.GameInProgress = false;

    }    

    public void SetTime(float time)
    {
        timeRemaining = time;
    }

    public void StartTimer()
    {
        timeIsRUnning = true;
    }

    private void FixedUpdate()
    {
        if (!timeIsRUnning)
            return;

        timeRemaining -= Time.fixedDeltaTime;

        TimeUpdated();
        Debug.LogError(timeRemaining);

        if (timeRemaining <= 0)
        {
            timeIsRUnning = false;
            GameController.Instance.GameInProgress = false;
        }
    }

}
