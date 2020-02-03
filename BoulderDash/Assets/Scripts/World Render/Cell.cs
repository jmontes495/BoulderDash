using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell
{
    private CellKind cellKind;

    public Cell(CellKind kind)
    {
        cellKind = kind;
    }

    public CellKind GetCellKind()
    {
        return cellKind;
    }
}
