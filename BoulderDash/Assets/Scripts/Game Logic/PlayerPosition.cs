using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPosition : MonoBehaviour
{
    private int xPosition;
    private int yPosition;

    private bool inGame;

    private void Start()
    {
        InitializePosition(1, 1);
    }

    public void InitializePosition(int x, int y)
    {
        xPosition = x;
        yPosition = y;
    }

    private void Update()
    {
        if (!GameController.Instance.GameInProgress)
            return;

        if (Input.GetKeyUp(KeyCode.UpArrow))
            ManageInput(Direction.Up);

        if (Input.GetKeyUp(KeyCode.DownArrow))
            ManageInput(Direction.Down);

        if (Input.GetKeyUp(KeyCode.LeftArrow))
            ManageInput(Direction.Left);

        if (Input.GetKeyUp(KeyCode.RightArrow))
            ManageInput(Direction.Right);
            
    }

    private void ManageInput(Direction direction)
    {
        int x = xPosition;
        int y = yPosition;

        switch(direction)
        {
            case Direction.Left:
                y--;
                break;

            case Direction.Right:
                y++;
                break;

            case Direction.Up:
                x--;
                break;

            case Direction.Down:
                x++;
                break;
        }

        bool result = GameController.Instance.TryMovePlayer(xPosition, yPosition, x, y, direction);
        if (result)
        {
            xPosition = x;
            yPosition = y;
        }
    }
}
