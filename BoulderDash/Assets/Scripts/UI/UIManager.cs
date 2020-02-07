using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject intro;

    [SerializeField]
    private GameObject board;


    private void Start()
    {
        board.gameObject.SetActive(false);
        intro.gameObject.SetActive(true);
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space) && !GameController.Instance.GameInProgress)
        {
            GameController.Instance.GameInProgress = true;
            intro.gameObject.SetActive(false);
            board.gameObject.SetActive(true);
        }
    }
}
