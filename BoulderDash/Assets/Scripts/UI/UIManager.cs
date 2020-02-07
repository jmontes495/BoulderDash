using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject intro;
    [SerializeField] private GameObject nextLevel;
    [SerializeField] private GameObject board;
    [SerializeField] private GameObject gameVariables;
    [SerializeField] private GameObject endScreen;
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
        gameVariables.SetActive(false);

        GameStats.LevelCompleted += ShowNextLevel;
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space) && !GameController.Instance.GameInProgress && !showingTransition && !GameController.Instance.LevelsFinished)
        {
            GameController.Instance.LoadNextLevel();
            intro.SetActive(false);
            nextLevel.SetActive(false);
            board.SetActive(true);
            gameVariables.SetActive(true);
        }
    }

    private void ShowNextLevel()
    {
        if(!GameController.Instance.LevelsFinished)
            StartCoroutine(LevelScreen());
        else
            StartCoroutine(EndScreen());
    }

    private IEnumerator LevelScreen()
    {
        showingTransition = true;
        yield return new WaitForSeconds(delayBeforeScreen);

        nextLevel.SetActive(true);
        board.SetActive(false);
        gameVariables.SetActive(false);
        showingTransition = false;
    }

    private IEnumerator EndScreen()
    {
        showingTransition = true;
        yield return new WaitForSeconds(delayBeforeScreen);

        endScreen.gameObject.SetActive(true);
        board.gameObject.SetActive(false);
        gameVariables.SetActive(false);
        finalScore.text = "Your final score is " + GameController.Instance.GameStats.Score;
        showingTransition = false;
    }

    private void OnDestroy()
    {
        GameStats.LevelCompleted -= ShowNextLevel;
    }
}
