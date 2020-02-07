using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStats : MonoBehaviour
{
    public delegate void StatsChange();
    public static event StatsChange GemsIncreased;
    public static event StatsChange LifeLost;
    public static event StatsChange TimeUpdated;
    public static event StatsChange LevelCompleted;

    public static int gemValue = 100;
    public static int extraSecondValue = 100;

    private int lifes;
    private int gemsNeeded;
    private int gemsCollected;
    private int score;
    private float timeRemaining;
    private bool timeIsRUnning;
    private bool levelFinished;

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

    public bool LevelFinished
    {
        get { return levelFinished; }
    }

    public void Initialize(int playerLifes, int gemsNeededForLevel)
    {
        lifes = playerLifes;
        gemsNeeded = gemsNeededForLevel;
        gemsCollected = 0;
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

    public void PlayerFinishedLevel()
    {
        levelFinished = true;
        score += TimeRemaining * extraSecondValue;
        timeRemaining = 0;
        timeIsRUnning = false;
        GemsIncreased();
        LevelCompleted();
    }

    private void FixedUpdate()
    {
        if (!timeIsRUnning)
            return;

        if (!GameController.Instance.GameInProgress)
        {
            timeIsRUnning = false;
            return;
        }

        timeRemaining -= Time.fixedDeltaTime;
        TimeUpdated();

        if (timeRemaining <= 0)
        {
            timeIsRUnning = false;
            GameController.Instance.GameInProgress = false;
        }
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.N))
        {
            GameController.Instance.GameInProgress = false;
            PlayerFinishedLevel();
        }
    }

}
