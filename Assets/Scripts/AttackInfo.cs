using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackInfo
{
    public float attackPower;
    public Vector3 forceVector;
    public Element element;

    public AttackInfo(float attackPower, Vector3 forceVector, Element element) {
        this.attackPower = attackPower;
        this.forceVector = forceVector;
        this.element = element;
    }
}

