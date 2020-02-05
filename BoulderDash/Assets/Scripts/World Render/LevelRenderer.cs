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

    private int rowRendering = 0;
    private int columnRendering = 0;
    private int renderLimit = 5;

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
        level.LoadLevel();
        RenderLevel(level);
    }

    public void ChangeRenderingReference(int newY, int newX, Direction direction)
    {
        switch(direction)
        {
            case Direction.Right:
                if (columnRendering < columnLimit + renderLimit && newX - columnRendering > columnLimit - 2)
                    columnRendering++;
                break;

            case Direction.Left:
                if (columnRendering > -renderLimit && newX - columnRendering <= 0)
                    columnRendering--;
                break;

            case Direction.Down:
                if (rowRendering < rowLimit + renderLimit && newY - rowRendering > rowLimit - 2)
                    rowRendering++;
                break;

            case Direction.Up:
                if (rowRendering > -renderLimit && newY - rowRendering <= 0)
                    rowRendering--;
                break;
                
        }

    }

    public void RenderLevel(LevelConfig level)
    {
        int rows = level.GetRows();
        int columns = level.GetColumns();

        if (rows >= rowLimit)
            rows = rowLimit;

        if (columns >= columnLimit)
            columns = columnLimit;

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                board[i, j].SetCell(level.GetCellByPosition(rowRendering + i, columnRendering + j));
            }

        }

    }
}
