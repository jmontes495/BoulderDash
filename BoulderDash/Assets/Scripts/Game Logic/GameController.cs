using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private static GameController instance;

    public static GameController Instance
    {
        get { return instance; }
        set { }
    }

    [SerializeField]
    private LevelRenderer levelRenderer;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            DestroyImmediate(this);
    }

    [SerializeField]
    private Level currentLevel;

    public bool TryMovePlayer(int lastX, int lastY, int newX, int newY)
    {
        CellKind futureCell = currentLevel.GetCellByPosition(newX, newY);
        bool result = false;

        switch(futureCell)
        {
            case CellKind.Dirt:
            case CellKind.Empty:
                currentLevel.ChangeCell(newX, newY, CellKind.Player);
                currentLevel.ChangeCell(lastX, lastY, CellKind.Empty);
                result = true;
                break;
            default:
                result = false;
                break;
        }

        levelRenderer.RenderLevel(currentLevel);
        return result;
    }
}
