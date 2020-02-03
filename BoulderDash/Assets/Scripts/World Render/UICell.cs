using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICell : MonoBehaviour
{
    public void SetCell(CellKind cellKind)
    {
        Image image = GetComponent<Image>();
        switch(cellKind)
        {
            case CellKind.Empty:
                image.color = Color.black;
                break;

            case CellKind.Brick:
                image.color = Color.red;
                break;

            case CellKind.Player:
                image.color = Color.cyan;
                break;

            case CellKind.Exit:
                image.color = Color.magenta;
                break;

            case CellKind.Dirt:
                image.color = Color.gray;
                break;

            default:
                image.color = Color.green;
                break;
        }
    }
}
