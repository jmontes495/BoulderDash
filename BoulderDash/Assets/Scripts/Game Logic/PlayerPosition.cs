using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPosition : MonoBehaviour
{
    private int xPosition;
    private int yPosition;

    float lastTimeChecked;
    [SerializeField]
    private float playerSpeed = 1f;

    public void InitializePosition(int x, int y)
    {
        xPosition = x;
        yPosition = y;
    }

    private void FixedUpdate()
    {
        if (!GameController.Instance.GameInProgress)
            return;

        lastTimeChecked += Time.fixedDeltaTime;

        if (lastTimeChecked <= 1/playerSpeed)
            return;
        
        if (Input.GetKey(KeyCode.UpArrow))
        {
            ManageInput(Direction.Up);
            lastTimeChecked = 0;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            ManageInput(Direction.Down);
            lastTimeChecked = 0;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            ManageInput(Direction.Left);
            lastTimeChecked = 0;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            ManageInput(Direction.Right);
            lastTimeChecked = 0;
        }   
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
