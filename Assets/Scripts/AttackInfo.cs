using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackInfo
{
    public float attackPower;
    public float attackForce;
    public Vector3 forceVector;
    public Element element;

    public AttackInfo(float attackPower, float attackForce, Vector3 forceVector, Element element) {
        this.attackPower = attackPower;
        this.attackForce = attackForce;
        this.forceVector = forceVector;
        this.element = element;
    }
}

