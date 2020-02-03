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

    private void Start()
    {
        CreateCells();
    }

    private void CreateCells()
    {
        board = new UICell[rowLimit, columnLimit];

        for (int i = 0; i < rowLimit; i++)
        {
            for (int j = 0; j < columnLimit; j++)
            {
                board[i,j] = Instantiate(cellPrefab);
                board[i, j].gameObject.transform.SetParent(boardParent);
            }
                
        }

        Level level = GetComponent<Level>();
        level.LoadLevel();
        RenderLevel(level);
    }

    public void ChangeRenderingReference(int newY, int newX, Direction direction)
    {
        //rowRendering = newY;
        //columnRendering = newX;
        switch(direction)
        {
            case Direction.Right:
                if (newX > columnLimit - 1)
                    columnRendering = 1;
                else
                    columnRendering++;
                break;

            case Direction.Left:
                if (newX < 1)
                    columnRendering = columnLimit - 1;
                else
                    columnRendering--;
                break;
                
        }
        //rowRendering = newY;
        //columnRendering = newX;

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
