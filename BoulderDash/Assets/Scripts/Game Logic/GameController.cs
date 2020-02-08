using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private static GameController instance;
    public static GameController Instance { get { return instance; } }
    private PlayerMovementController playerMovementController;
    private BoulderMovementController boulderMovementController;
    private PlayerPosition playerPosition;

    [SerializeField] private LevelRenderer levelRenderer;
    [SerializeField] private LevelConfig[] levels;
    [SerializeField] private int lifes;
    [SerializeField] private GameStats gameStats;
    public GameStats GameStats { get { return gameStats; } }

    private bool gameInProgress;
    public bool GameInProgress { get { return gameInProgress; } set { gameInProgress = value; } }
    public bool LevelsFinished { get { return levelIndex >= levels.Length - 1; } }
    private LevelConfig currentLevel;
    private int levelIndex = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            playerMovementController = new PlayerMovementController();
            boulderMovementController = new BoulderMovementController();
            playerPosition = GetComponent<PlayerPosition>();
            gameInProgress = false;
            DontDestroyOnLoad(gameObject);
        }
        else
            DestroyImmediate(this);
    }

    public void LoadGame()
    {
        levelIndex = 0;
        LoadCurrentLevel();
        gameStats.Initialize(lifes);
        gameInProgress = true;
    }

    public void AdvanceLevel()
    {
        levelIndex++;
        if (levelIndex >= levels.Length)
            return;

        LoadCurrentLevel();
        gameInProgress = true;
    }

    private void LoadCurrentLevel()
    {
        currentLevel = levels[levelIndex];
        currentLevel.LoadLevel();
        gameStats.SetLevelVariables(currentLevel.GetSecondsToComplete(), currentLevel.GetRequiredGems());
        levelRenderer.LoadLevel(currentLevel, currentLevel.GetPlayerInitialPosition().x - 3, currentLevel.GetPlayerInitialPosition().y - 3);
        playerPosition.InitializePosition(currentLevel.GetPlayerInitialPosition().x, currentLevel.GetPlayerInitialPosition().y);
    }

    public void OutOfLifes()
    {
        levelIndex = 0;
    }

    public void ReloadAfterDeath()
    {
        currentLevel.ChangeCell(playerPosition.XPosition, playerPosition.YPosition, CellKind.Empty);
        currentLevel.ChangeCell(currentLevel.GetPlayerInitialPosition().x, currentLevel.GetPlayerInitialPosition().y, CellKind.Player);
        playerPosition.InitializePosition(currentLevel.GetPlayerInitialPosition().x, currentLevel.GetPlayerInitialPosition().y);
        levelRenderer.LoadLevel(currentLevel, currentLevel.GetPlayerInitialPosition().x - 3, currentLevel.GetPlayerInitialPosition().y - 3);
        
        gameInProgress = true;
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
        bool playerDied = gameInProgress;
        Direction result = boulderMovementController.TryToDropBoulder(x, y, fallingStart);
        playerDied = playerDied && !gameInProgress;

        if (result != Direction.None || playerDied)
            levelRenderer.RenderLevel(currentLevel);
        if (playerDied)
            gameStats.LoseLife();

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
        gameStats.IncreaseGems();
    }

    public bool ExitAvailable
    {
        get { return gameStats.GemsCollected >= currentLevel.GetRequiredGems(); }
    }

    public void PlayerReachedExit()
    {
        gameInProgress = false;
        gameStats.PlayerFinishedLevel();
    }
}
