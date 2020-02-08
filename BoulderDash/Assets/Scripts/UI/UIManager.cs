using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject intro;
    [SerializeField] private GameObject nextLevel;
    [SerializeField] private GameObject board;
    [SerializeField] private LevelUI gameVariables;
    [SerializeField] private GameObject endScreen;
    [SerializeField] private GameObject gameOver;
    [SerializeField] private TextMeshProUGUI finalScore;

    [SerializeField]
    public float delayBeforeScreen;
    bool showingTransition;

    private void Start()
    {
        board.SetActive(false);
        intro.SetActive(true);
        nextLevel.SetActive(false);
        endScreen.SetActive(false);
        gameVariables.ShowElements(false);
        gameOver.SetActive(false);
        GameStats.LevelCompleted += ShowNextLevel;
        GameStats.LifeLost += ShowPlayerDied;
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (!GameController.Instance.GameInProgress && !showingTransition && !GameController.Instance.LevelsFinished && GameController.Instance.GameStats.Lifes > 0)
            {
                GameController.Instance.LoadNextLevel();
                intro.SetActive(false);
                nextLevel.SetActive(false);
                board.SetActive(true);
                gameVariables.ShowElements(true);
                gameOver.SetActive(false);
            }
        }
    }

    private void ShowNextLevel()
    {
        if(!GameController.Instance.LevelsFinished)
            StartCoroutine(LevelScreen());
        else
            StartCoroutine(EndScreen());
    }

    private void ShowPlayerDied()
    {
        showingTransition = true;
        StartCoroutine(DeathTransition());
    }

    private IEnumerator DeathTransition()
    {
        yield return new WaitForSeconds(delayBeforeScreen);

        if (GameController.Instance.GameStats.Lifes > 0)
            GameController.Instance.ReloadAfterDeath();
        else
        {
            gameOver.SetActive(true);
            board.SetActive(false);
        }
        showingTransition = false;
    }

    private IEnumerator LevelScreen()
    {
        showingTransition = true;
        yield return new WaitForSeconds(delayBeforeScreen);

        nextLevel.SetActive(true);
        board.SetActive(false);
        gameVariables.ShowElements(false);
        showingTransition = false;
    }

    private IEnumerator EndScreen()
    {
        showingTransition = true;
        yield return new WaitForSeconds(delayBeforeScreen);

        endScreen.gameObject.SetActive(true);
        board.gameObject.SetActive(false);
        gameVariables.ShowElements(false);
        finalScore.text = "Your final score is " + GameController.Instance.GameStats.Score;
        showingTransition = false;
    }

    private void OnDestroy()
    {
        GameStats.LevelCompleted -= ShowNextLevel;
    }
}
