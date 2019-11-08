﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyState : MonoBehaviour
{
    public float health;
    public float hurt;
    
    public abstract void TakeHit(float damage, Vector3 attackerPosition, float forceScalar);
}
