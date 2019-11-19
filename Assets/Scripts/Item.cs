using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : ScriptableObject {
    //Superclass of collectibles
    public abstract Sprite GetSprite();
}
