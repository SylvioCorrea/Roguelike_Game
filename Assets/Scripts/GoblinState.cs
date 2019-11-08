using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinState : EnemyState
{
    // Start is called before the first frame update
    void Start()
    {
        health = 100;
        hurt = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0) {
            Destroy(gameObject);
        }
        if(hurt > 0) {
            hurt -= Time.deltaTime;
        } else {
            GetComponent<SpriteRenderer>().color = Color.white;
        }

    }

    public override void TakeHit(float damage, Vector3 attackerPosition, float forceScalar) {
        health -= damage;
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        Vector3 forceVector = transform.position - attackerPosition;
        GetComponent<Rigidbody2D>().AddForce(forceVector.normalized * forceScalar, ForceMode2D.Impulse);
        GetComponent<SpriteRenderer>().color = Color.red;
        hurt = 0.25f;
    }
}
