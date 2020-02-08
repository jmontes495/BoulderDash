using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStats : MonoBehaviour
{
    public delegate void StatsChange();
    public static event StatsChange ValuesInitialized;
    public static event StatsChange GemsUpdated;
    public static event StatsChange LifesUpdated;
    public static event StatsChange TimeUpdated;
    public static event StatsChange LevelCompleted;

    public static int gemValue = 100;
    public static int extraSecondValue = 100;

    private int lifes;
    private int gemsNeeded;
    private int gemsCollected;
    private int score;
    private float timeRemaining;
    private bool levelFinished;

    public int Lifes { get { return lifes; } }
    public int GemsNeeded { get { return gemsNeeded; } }
    public int GemsCollected { get { return gemsCollected; } }
    public int Score { get { return score; } }
    public int TimeRemaining { get { return (int) timeRemaining; } }
    public bool LevelFinished { get { return levelFinished; } }

    public void Initialize(int playerLifes)
    {
        lifes = playerLifes;
        score = 0;
        ValuesInitialized();
    }

    public void SetLevelVariables(float time, int gemsNeededForLevel)
    {
        timeRemaining = time;
        gemsNeeded = gemsNeededForLevel;
        gemsCollected = 0;
        GemsUpdated();
    }

    private void FixedUpdate()
    {
        if (!GameController.Instance.GameInProgress)
            return;

        timeRemaining -= Time.fixedDeltaTime;
        TimeUpdated();

        if (timeRemaining <= 0)
        {
            GameController.Instance.GameInProgress = false;
            lifes = 0;
            LifesUpdated();
        }
    }

    public void IncreaseGems()
    {
        gemsCollected++;
        score += gemValue;
        GemsUpdated();
    }

    public void LoseLife()
    {
        lifes--;
        LifesUpdated();
    }   

    public void PlayerFinishedLevel()
    {
        levelFinished = true;
        score += TimeRemaining * extraSecondValue;
        timeRemaining = 0;
        GemsUpdated();
        LevelCompleted();
    }


}
