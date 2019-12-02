using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskedOrcCoreScript : EnemyCoreScript
{
    // Start is called before the first frame update
    void Start()
    {
        health = healthMax;
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0) {
            Die();
        }
        if(hurt > 0) {
            hurt -= Time.deltaTime;
        } else {
            GetComponent<SpriteRenderer>().color = Color.white;
        }

    }

    public override void TakeHit(AttackInfo aInfo) {
        TakeDamage(aInfo.attackPower);
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        GetComponent<Rigidbody2D>().AddForce(aInfo.forceVector, ForceMode2D.Impulse);
        GetComponent<SpriteRenderer>().color = Color.red;
        hurt = 0.25f;
    }
}
