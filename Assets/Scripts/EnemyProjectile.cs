using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public float attackPower;
    public float attackForce;
    public Element element;
    public float speed;
    
    void OnCollisionEnter2D(Collision2D col) {
        if(col.transform.CompareTag("Player")) {
            Vector3 forceVector = (col.transform.position - transform.position).normalized * attackForce;
            col.transform.GetComponent<PlayerCoreScript>().TakeHit(new AttackInfo(attackPower, forceVector, element));
        }
        Destroy(gameObject);
    } 
}
