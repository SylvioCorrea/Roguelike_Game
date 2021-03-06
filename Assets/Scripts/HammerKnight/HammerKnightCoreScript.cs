﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerKnightCoreScript : EnemyCoreScript
{
    SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        health = healthMax;
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0) {
            Die();
        }
    }

    override public void TakeHit(AttackInfo aInfo) {
        TakeDamage(aInfo.attackPower);
        //GetComponent<Rigidbody2D>().AddForce(aInfo.forceVector, ForceMode2D.Impulse);
        StartCoroutine(ShowHurtFrames());
    }

    public IEnumerator ShowHurtFrames() {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.25f);
        spriteRenderer.color = Color.white;
    }
}
