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
    private int renderLimit = 1;

    private void Start()
    {
        CreateCells();
    }

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

        Level level = GetComponent<Level>();
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

    public void RenderLevel(Level level)
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
