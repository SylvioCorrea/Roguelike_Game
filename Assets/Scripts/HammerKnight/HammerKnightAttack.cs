using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerKnightAttack : MonoBehaviour
{
    public float attackPower;
    public float attackForce;
    public Element element;
    
    public void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Player")) {
            Vector3 forceVector = (col.transform.position - transform.position).normalized * attackForce;
            AttackInfo aInfo = new AttackInfo(attackPower, forceVector, element);
            col.GetComponent<PlayerState>().TakeHit(aInfo);
        }
    }


    public void OnTriggerStay2D(Collider2D col)
    {
        if(col.CompareTag("Player")) {
            Vector3 forceVector = (col.transform.position - transform.position).normalized * attackForce;
            AttackInfo aInfo = new AttackInfo(attackPower, forceVector, element);
            col.GetComponent<PlayerState>().TakeHit(aInfo);
        }
    }
}
