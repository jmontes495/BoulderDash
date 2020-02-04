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

    private PlayerMovementController playerMovementController;
    private BoulderMovementController boulderMovementController;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            playerMovementController = new PlayerMovementController();
            boulderMovementController = new BoulderMovementController();
            gameInProgress = true;
            DontDestroyOnLoad(gameObject);
        }
        else
            DestroyImmediate(this);
    }

    [SerializeField]
    protected LevelRenderer levelRenderer;
    [SerializeField]
    protected Level currentLevel;

    protected bool gameInProgress;
    public bool GameInProgress
    {
        get { return gameInProgress; }
        set { gameInProgress = value; }
    }

    public CellKind GetCellByPosition(int newX, int newY)
    {
        return currentLevel.GetCellByPosition(newX, newY);
    }

    public void ChangeCell(int newX, int newY, CellKind cellKind)
    {
        currentLevel.ChangeCell(newX, newY, cellKind);
    }

    public bool TryMovePlayer(int lastX, int lastY, int newX, int newY, Direction direction)
    {
        bool result = playerMovementController.TryMovePlayerByDirection(lastX, lastY, newX, newY, direction);
        if (result)
        {
            levelRenderer.ChangeRenderingReference(newX, newY, direction);
            levelRenderer.RenderLevel(currentLevel);
        }
        return result;
    }

    public Direction TryToDropBoulder(int x, int y, Vector2Int fallingStart)
    {
        Direction result = boulderMovementController.TryToDropBoulder(x, y, fallingStart);
        if (result != Direction.None)
        {
            levelRenderer.RenderLevel(currentLevel);
        }
        return result;
    }

    public void PushBoulder(int oldX, int oldY, int newX, int newY)
    {
        boulderMovementController.PushBoulder(oldX, oldY, newX, newY);
    }

    public List<Boulder> GetBoulders()
    {
        return currentLevel.GetBoulders();
    }
}
