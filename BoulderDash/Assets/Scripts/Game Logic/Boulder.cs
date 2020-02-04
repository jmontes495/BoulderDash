using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boulder
{
    private Vector2Int position;
    private Vector2Int initialPosition;

    private float fallSpeed = 5f;

    public Boulder(Vector2Int pos)
    {
        position = pos;
        initialPosition = new Vector2Int(-1, -1);
    }

    public Vector2Int GetPosition()
    {
        return position;
    }

    public void SetPosition(Vector2Int pos)
    {
        position = pos;
    }

    public Vector2Int GetInitialPosition()
    {
        return initialPosition;
    }

    public void SetInitialPosition(Vector2Int pos)
    {
        initialPosition = pos;
    }
}
