using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteConfig : ScriptableObject
{
    [SerializeField]  private Sprite character1;
    [SerializeField]  private Sprite character2;
    [SerializeField]  private Sprite characterDead;
    [SerializeField]  private Sprite[] exit;
    [SerializeField]  private Sprite boulder;
    [SerializeField]  private Sprite dirt;
    [SerializeField]  private Sprite brick;
    [SerializeField]  private Sprite[] gem;

    public Sprite Character1
    {
        get { return character1; }
    }

    public Sprite Character2
    {
        get { return character2; }
    }

    public Sprite CharacterDead
    {
        get { return characterDead; }
    }

    public Sprite[] Exit
    {
        get { return exit; }
    }

    public Sprite Boulder
    {
        get { return boulder; }
    }

    public Sprite Dirt
    {
        get { return dirt; }
    }

    public Sprite Brick
    {
        get { return brick; }
    }

    public Sprite[] Gem
    {
        get { return gem; }
    }
}
