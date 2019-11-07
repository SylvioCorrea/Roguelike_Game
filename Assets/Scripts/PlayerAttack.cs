﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Transform attackPos;
    public float hitboxRadius;
    public float scalar;
    public LayerMask enemyLayerMask; //Maybe use a tag instead
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Set attack hitbox in place
        Vector3 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        target.z = 0f;
        target = target - transform.position;
        attackPos.position = transform.position + (target.normalized * scalar);
        
        //Attack
        if(Input.GetButtonDown("Fire1")) {
            Collider2D[] enemiesHit = Physics2D.OverlapCircleAll(attackPos.position, hitboxRadius, enemyLayerMask);
            foreach(Collider2D e in enemiesHit) {
                Debug.Log("enemy hit!!");
                e.GetComponent<EnemyState>().TakeDamage(10);
            }
        }
    }

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, hitboxRadius);
    }
}