using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICell : MonoBehaviour
{
    [SerializeField]
    private SpriteConfig spriteConfig;

    private Image image;
    private CellKind currentLook;
    
    public void SetCell(CellKind cellKind)
    {
        if(image == null)
            image = GetComponent<Image>();

        image.color = Color.white;
        currentLook = cellKind;

        switch (cellKind)
        {
            case CellKind.Empty:
                image.sprite = null;
                image.color = Color.black;
                break;

            case CellKind.Player:
                image.sprite = spriteConfig.Character2;
                if(gameObject.activeInHierarchy)
                    StartCoroutine(Jump());
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

    private IEnumerator Jump()
    {
        yield return new WaitForSeconds(0.15f);
        if(currentLook == CellKind.Player)
            image.sprite = spriteConfig.Character1;
    }
    
    private void FixedUpdate()
    {
        if(currentLook == CellKind.Gem)
        {
            int gem = (int) Time.realtimeSinceStartup % 2;
            image.sprite = spriteConfig.Gem[gem];
        }

        if (currentLook == CellKind.Exit && GameController.Instance.ExitAvailable)
        {
            int exit = (int)Time.realtimeSinceStartup % 3;
            image.sprite = spriteConfig.Exit[exit + 1]; 
        }
    }


}
