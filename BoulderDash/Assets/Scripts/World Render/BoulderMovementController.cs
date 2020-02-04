using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderMovementController
{
    public Direction TryToDropBoulder(int x, int y, Vector2Int fallingStart)
    {
        Direction result = Direction.None;
        if (GameController.Instance.GetCellByPosition(x + 1, y) == CellKind.Empty)
        {
            GameController.Instance.ChangeCell(x, y, CellKind.Empty);
            GameController.Instance.ChangeCell(x + 1, y, CellKind.Boulder);
            result = Direction.Down;
        }
        else if (GameController.Instance.GetCellByPosition(x + 1, y) == CellKind.Player && fallingStart.x != -1)
        {
            result = Direction.None;
            GameController.Instance.GameInProgress = false;
        }
        else if (GameController.Instance.GetCellByPosition(x + 1, y) == CellKind.Brick || GameController.Instance.GetCellByPosition(x + 1, y) == CellKind.Boulder)
        {
            if (GameController.Instance.GetCellByPosition(x, y + 1) == CellKind.Empty && GameController.Instance.GetCellByPosition(x + 1, y + 1) == CellKind.Empty)
            {
                GameController.Instance.ChangeCell(x, y, CellKind.Empty);
                GameController.Instance.ChangeCell(x, y + 1, CellKind.Boulder);
                result = Direction.Right;
            }
            else if (GameController.Instance.GetCellByPosition(x, y - 1) == CellKind.Empty && GameController.Instance.GetCellByPosition(x + 1, y - 1) == CellKind.Empty)
            {
                GameController.Instance.ChangeCell(x, y, CellKind.Empty);
                GameController.Instance.ChangeCell(x, y - 1, CellKind.Boulder);
                result = Direction.Left;
            }
        }

        return result;
    }
}
