using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon Scrip Obj")]
public class Weapon : ScriptableObject {
    public float attackPower;
    public float attackForce;
    public Element element;
    public Sprite sprite;
}
