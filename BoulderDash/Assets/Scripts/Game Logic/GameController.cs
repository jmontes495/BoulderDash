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
    [SerializeField]
    private LevelConfig[] levels;

    private PlayerMovementController playerMovementController;
    private BoulderMovementController boulderMovementController;

    private bool gameInProgress;
    public bool GameInProgress
    {
        get { return gameInProgress; }
        set { gameInProgress = value; }
    }
    private int gemsCollected;
    private LevelConfig currentLevel;
    private int levelIndex = 0;

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

    private void Start()
    {
        LoadCurrentLevel();
    }

    private void LoadCurrentLevel()
    {
        if (levelIndex >= levels.Length)
            return;

        currentLevel = levels[levelIndex];
        currentLevel.LoadLevel();
        levelRenderer.LoadLevel(currentLevel, currentLevel.GetPlayerInitialPosition().x - 3, currentLevel.GetPlayerInitialPosition().y - 3);
        if (GetComponent<PlayerPosition>() != null)
            GetComponent<PlayerPosition>().InitializePosition(currentLevel.GetPlayerInitialPosition().x, currentLevel.GetPlayerInitialPosition().y);
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
        if (currentLevel == null)
            return null;

        return currentLevel.GetBoulders();
    }

    public void IncreaseGems()
    {
        gemsCollected++;
    }

    public bool ExitAvailable
    {
        get { return gemsCollected >= currentLevel.GetRequiredGems(); }
    }

    public void PlayerReachedExit()
    {
        gameInProgress = false;
    }
}
