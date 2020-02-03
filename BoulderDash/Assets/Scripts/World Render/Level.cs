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

    public void LoadLevel()
    {
        board = new Cell[rows, columns];

        foreach (Vector2Int boulderPos in boulderPositions)
        {
            int x = boulderPos.x >= rows ? rows - 1 : boulderPos.x;
            int y = boulderPos.y >= columns ? columns - 1 : boulderPos.y;
            board[x, y] = new Cell(CellKind.Boulder);
        }

        foreach (Vector2Int brickPos in brickPosition)
        {
            int x = brickPos.x >= rows ? rows - 1 : brickPos.x;
            int y = brickPos.y >= columns ? columns - 1 : brickPos.y;
            board[x, y] = new Cell(CellKind.Brick);
        }

        board[playerInitialPosition.x >= rows ? rows - 1 : playerInitialPosition.x, playerInitialPosition.y >= columns ? columns - 1 : playerInitialPosition.y] = new Cell(CellKind.Player);
        board[exitPosition.x >= rows ? rows - 1 : exitPosition.x, exitPosition.y >= columns ? columns - 1 : exitPosition.y] = new Cell(CellKind.Exit);

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                if (board[i, j] == null)
                    board[i, j] = new Cell(CellKind.Dirt);
            }
        }
    }

    public int GetRows()
    {
        return rows;
    }

    public int GetColumns()
    {
        return columns;
    }

    public CellKind GetCellByPosition(int x, int y)
    {
        if (x >= rows || y >= columns || x < 0 || y < 0)
            return CellKind.OutOfBounds;
        
        return board[x, y].GetCellKind();
    }

    public void ChangeCell(int x, int y, CellKind cell)
    {
        if (x >= rows || y >= columns || x < 0 || y < 0)
            return;
        
        board[x, y].SetCellKind(cell);
    }
}
