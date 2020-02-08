using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelRenderer : MonoBehaviour
{
    [SerializeField] private Transform boardParent;
    [SerializeField] private UICell cellPrefab; 
    [SerializeField] private UICell[,] board; 
    [SerializeField] private int rowLimit;
    [SerializeField] private int columnLimit;
    [SerializeField] private int renderLimit = 3;

    private int rowRendering = 0;
    private int columnRendering = 0;
    private int realBoardRows = 0;
    private int realBoardColumns = 0;

    private void CreateCells()
    {
        if (board == null)
        {
            board = new UICell[rowLimit, columnLimit];

            for (int i = 0; i < rowLimit; i++)
            {
                for (int j = 0; j < columnLimit; j++)
                {
                    board[i, j] = Instantiate(cellPrefab);
                    board[i, j].gameObject.transform.SetParent(boardParent);
                }

            }
        }
    }

    public void LoadLevel(LevelConfig level, int renderX, int renderY)
    {
        if (board == null)
            CreateCells();

        rowRendering = renderX;
        columnRendering = renderY;
        realBoardRows = level.GetRows();
        realBoardColumns = level.GetColumns();
        RenderLevel(level);
    }

    public void ChangeRenderingReference(int newX, int newY, Direction direction)
    {
        switch(direction)
        {
            case Direction.Right:
                if (columnRendering + columnLimit <= realBoardColumns && columnRendering + columnLimit - newY <= renderLimit)
                    columnRendering++;
                break;

            case Direction.Left:
                if (columnRendering >= 0 && newY - columnRendering < renderLimit)
                    columnRendering--;
                break;

            case Direction.Down:
                if (rowRendering + rowLimit <= realBoardRows && rowRendering + rowLimit - newX <= renderLimit)
                    rowRendering++;
                break;

            case Direction.Up:
                if (rowRendering >= 0 && newX - rowRendering < renderLimit)
                    rowRendering--;
                break;
                
        }

    }

    public void RenderLevel(LevelConfig level)
    {
        for (int i = 0; i < rowLimit; i++)
        {
            for (int j = 0; j < columnLimit; j++)
            {
                board[i, j].SetCell(level.GetCellByPosition(rowRendering + i, columnRendering + j));
            }

        }

    }
}
