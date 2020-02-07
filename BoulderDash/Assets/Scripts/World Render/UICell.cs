using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICell : MonoBehaviour
{
    [SerializeField]
    private SpriteConfig spriteConfig;

    private Image image;
    
    public void SetCell(CellKind cellKind)
    {
        if(image == null)
            image = GetComponent<Image>();

        image.color = Color.white;

        switch (cellKind)
        {
            case CellKind.Empty:
                image.sprite = null;
                image.color = Color.black;
                break;

            case CellKind.Player:
                image.sprite = spriteConfig.Character1;
                break;

            case CellKind.Gem:
                image.sprite = spriteConfig.Gem[0];
                break;

            case CellKind.Exit:
                image.sprite = spriteConfig.Exit[0];
                break;

            case CellKind.Dirt:
                image.sprite = spriteConfig.Dirt;
                break;

            case CellKind.Boulder:
                image.sprite = spriteConfig.Boulder;
                break;

            case CellKind.Brick:
            case CellKind.OutOfBounds:
            default:
                image.sprite = spriteConfig.Brick;
                break;
        }
    }


}
