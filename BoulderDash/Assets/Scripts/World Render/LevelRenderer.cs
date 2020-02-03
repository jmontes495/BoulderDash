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
        RenderLevel(level);
    }

    public void RenderLevel(Level level)
    {
        int rows = level.GetRows();
        int columns = level.GetColumns();

        if (level.GetRows() < rowLimit)
            rows = rowLimit;

        if (level.GetColumns() < columnLimit)
            columns = columnLimit;

        for (int i = rowRendering; i < rowRendering + rows; i++)
        {
            for (int j = columnRendering; j < columnRendering + columns; j++)
            {
                board[i, j].SetCell(level.GetCellByPosition(i, j));
            }

        }

    }
}
