using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private int rows;
    [SerializeField] private int columns;
    [SerializeField] private List<Vector2Int> boulderPositions;
    [SerializeField] private List<Vector2Int> brickPosition;
    [SerializeField] private Vector2Int playerInitialPosition;
    [SerializeField] private Vector2Int exitPosition;

    private Cell[,] board;
    // Start is called before the first frame update
    void Start()
    {
        LoadLevel();
    }

    void LoadLevel()
    {
        board = new Cell[rows, columns];

        foreach (Vector2Int boulderPos in boulderPositions)
            board[boulderPos.x, boulderPos.y] = new Cell(CellKind.Boulder);

        foreach (Vector2Int brickPos in brickPosition)
            board[brickPos.x, brickPos.y] = new Cell(CellKind.Brick);

        board[playerInitialPosition.x, playerInitialPosition.y] = new Cell(CellKind.Player);
        board[exitPosition.x, exitPosition.y] = new Cell(CellKind.Exit);
    }

    public int GetRows()
    {
        return rows;
    }

    public int GetColumns()
    {
        return columns;
    }
}
