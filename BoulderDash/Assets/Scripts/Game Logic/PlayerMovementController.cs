using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController
{
    public bool TryMovePlayerByDirection(int lastX, int lastY, int newX, int newY, Direction direction)
    {
        CellKind futureCell = GameController.Instance.GetCellByPosition(newX, newY);
        bool result = false;

        switch (futureCell)
        {
            case CellKind.Dirt:
            case CellKind.Empty:
                GameController.Instance.ChangeCell(newX, newY, CellKind.Player);
                GameController.Instance.ChangeCell(lastX, lastY, CellKind.Empty);
                result = true;
                break;
            case CellKind.Gem:
                GameController.Instance.ChangeCell(newX, newY, CellKind.Player);
                GameController.Instance.ChangeCell(lastX, lastY, CellKind.Empty);
                GameController.Instance.IncreaseGems();
                result = true;
                break;
            case CellKind.Boulder:
                if (direction == Direction.Left && GameController.Instance.GetCellByPosition(newX, newY - 1) == CellKind.Empty)
                {
                    GameController.Instance.PushBoulder(newX, newY, newX, newY - 1);
                    GameController.Instance.ChangeCell(newX, newY - 1, CellKind.Boulder);
                    GameController.Instance.ChangeCell(newX, newY, CellKind.Player);
                    GameController.Instance.ChangeCell(lastX, lastY, CellKind.Empty);
                    result = true;
                }
                else if (direction == Direction.Right && GameController.Instance.GetCellByPosition(newX, newY + 1) == CellKind.Empty)
                {
                    GameController.Instance.PushBoulder(newX, newY, newX, newY + 1);
                    GameController.Instance.ChangeCell(newX, newY + 1, CellKind.Boulder);
                    GameController.Instance.ChangeCell(newX, newY, CellKind.Player);
                    GameController.Instance.ChangeCell(lastX, lastY, CellKind.Empty);
                    result = true;
                }
                break;
            case CellKind.Exit:
                if(GameController.Instance.ExitAvailable)
                {
                    GameController.Instance.ChangeCell(newX, newY, CellKind.Player);
                    GameController.Instance.ChangeCell(lastX, lastY, CellKind.Empty);
                    GameController.Instance.PlayerReachedExit();
                    result = true;
                }
                break;
            default:
                result = false;
                break;
        }

        return result;
    }
}
