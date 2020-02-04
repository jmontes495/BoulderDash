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
                image.color = Color.yellow;
                break;

            case CellKind.Player:
                image.color = Color.cyan;
                break;

            case CellKind.Gem:
                image.color = Color.white;
                break;

            case CellKind.Exit:
                image.color = Color.magenta;
                break;

            case CellKind.Dirt:
                image.color = Color.gray;
                break;

            case CellKind.Boulder:
                image.color = Color.blue;
                break;

            default:
                image.color = Color.red;
                break;
        }
    }
}
