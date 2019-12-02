using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Key : Item
{
    
    public Sprite keySprite;

    
    override public Sprite GetSprite()
    {
        return keySprite;
    }
}
