using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityEffect : MonoBehaviour
{
    float lastTimeChecked;
    [SerializeField]
    private float fallDelay = 1f;

    private void FixedUpdate()
    {
        if (!GameController.Instance.GameInProgress)
            return;

        lastTimeChecked += Time.fixedDeltaTime;

        if (lastTimeChecked <= fallDelay)
            return;

        lastTimeChecked = 0;
        foreach (Boulder boulder in GameController.Instance.GetBoulders())
        {
            Vector2Int position = boulder.GetPosition();
            Direction directionMayFall = GameController.Instance.TryToDropBoulder(position.x, position.y, boulder.GetInitialPosition());

            if (directionMayFall != Direction.None)
            {
                if (boulder.GetInitialPosition().x == -1)
                    boulder.SetInitialPosition(position);

                switch (directionMayFall)
                {
                    case Direction.Down:
                        position.x++;
                        break;
                    case Direction.Left:
                        position.y--;
                        break;
                    case Direction.Right:
                        position.y++;
                        break;
                }
                boulder.SetPosition(position);
            }
            else
                boulder.SetInitialPosition(new Vector2Int(-1, -1));
        }
    }
}
