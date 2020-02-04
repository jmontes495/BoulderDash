using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityEffect : MonoBehaviour
{
    private List<Boulder> boulders;
    float lastTimeChecked;
    private float fallSpeed = 1f;

    private void Start()
    {
        Initialize(GameController.Instance.GetBoulderPositions());    
    }

    public void Initialize(List<Vector2Int> boulderPositions)
    {
        boulders = new List<Boulder>();

        foreach(Vector2Int boulderPos in boulderPositions)
            boulders.Add(new Boulder(boulderPos));
    }

    private void FixedUpdate()
    {
        if (boulders == null)
            return;

        lastTimeChecked += Time.fixedDeltaTime;

        if (lastTimeChecked <= fallSpeed)
            return;

        lastTimeChecked = 0;
        foreach (Boulder boulder in boulders)
        {
            Vector2Int position = boulder.GetPosition();
            Direction directionMayFall = GameController.Instance.TryToDropBoulder(position.x, position.y);

            if (directionMayFall != Direction.None)
            {
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
        }
    }
}
