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

    public bool TryMovePlayer(int lastX, int lastY, int newX, int newY, Direction direction)
    {
        CellKind futureCell = currentLevel.GetCellByPosition(newX, newY);
        bool result = false;

        switch(futureCell)
        {
            case CellKind.Dirt:
            case CellKind.Empty:
            case CellKind.Gem:
                currentLevel.ChangeCell(newX, newY, CellKind.Player);
                currentLevel.ChangeCell(lastX, lastY, CellKind.Empty);
                result = true;
                break;
            case CellKind.Boulder:
                if (direction == Direction.Left && currentLevel.GetCellByPosition(newX, newY - 1) == CellKind.Empty)
                {
                    currentLevel.ChangeCell(newX, newY - 1, CellKind.Boulder);
                    currentLevel.ChangeCell(newX, newY, CellKind.Player);
                    currentLevel.ChangeCell(lastX, lastY, CellKind.Empty);
                    result = true;
                }
                else if (direction == Direction.Right && currentLevel.GetCellByPosition(newX, newY + 1) == CellKind.Empty)
                {
                    currentLevel.ChangeCell(newX, newY + 1, CellKind.Boulder);
                    currentLevel.ChangeCell(newX, newY, CellKind.Player);
                    currentLevel.ChangeCell(lastX, lastY, CellKind.Empty);
                    result = true;
                }
                break;
            default:
                result = false;
                break;
        }

        if(result)
        {
            levelRenderer.ChangeRenderingReference(newX, newY, direction);
            levelRenderer.RenderLevel(currentLevel);
        }

        return result;
    }

    public Direction TryToDropBoulder(int x, int y, Vector2Int fallingStart)
    {
        Direction result = Direction.None;
        if (currentLevel.GetCellByPosition(x + 1, y) == CellKind.Empty)
        {
            currentLevel.ChangeCell(x, y, CellKind.Empty);
            currentLevel.ChangeCell(x + 1, y, CellKind.Boulder);
            result = Direction.Down;
        }
        else if (currentLevel.GetCellByPosition(x + 1, y) == CellKind.Player && fallingStart.x != -1)
        {
            result = Direction.None;
            Debug.LogError("You Died!");
        }
        else if (currentLevel.GetCellByPosition(x + 1, y) == CellKind.Brick || currentLevel.GetCellByPosition(x + 1, y) == CellKind.Boulder)
        {
            if (currentLevel.GetCellByPosition(x, y + 1) == CellKind.Empty && currentLevel.GetCellByPosition(x + 1, y + 1) == CellKind.Empty)
            {
                currentLevel.ChangeCell(x, y, CellKind.Empty);
                currentLevel.ChangeCell(x, y + 1, CellKind.Boulder);
                result = Direction.Right;
            }
            else if (currentLevel.GetCellByPosition(x, y - 1) == CellKind.Empty && currentLevel.GetCellByPosition(x + 1, y - 1) == CellKind.Empty)
            {
                currentLevel.ChangeCell(x, y, CellKind.Empty);
                currentLevel.ChangeCell(x, y - 1, CellKind.Boulder);
                result = Direction.Left;
            }
        }

        if (result != Direction.None)
        {
            levelRenderer.RenderLevel(currentLevel);
        }

        return result;
    }

    public List<Vector2Int> GetBoulderPositions()
    {
        return currentLevel.GetBoulderPositions();
    }
}
