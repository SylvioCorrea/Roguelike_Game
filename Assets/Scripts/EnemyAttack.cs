using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    
    public int targetLayer;
    public float attackPower;
    public float attackForce;
    public Element element;
    // Start is called before the first frame update
    void Start()
    {
        targetLayer = LayerMask.NameToLayer("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.layer == targetLayer) {
            Vector3 forceVector = (col.gameObject.transform.position - transform.position).normalized * attackForce;
            AttackInfo aInfo = new AttackInfo(attackPower, forceVector, element);
            col.gameObject.GetComponent<PlayerCoreScript>().TakeHit(aInfo);
        }
    }

    void OnCollisionStay2D(Collision2D col)
    {
        if(col.gameObject.layer.Equals(targetLayer)) {
            Vector3 forceVector = (col.gameObject.transform.position - transform.position).normalized * attackForce;
            AttackInfo aInfo = new AttackInfo(attackPower, forceVector, element);
            col.gameObject.GetComponent<PlayerCoreScript>().TakeHit(aInfo);
            //Debug.Log("Player hit!");
        }
    }

    

    /* void OnTriggerEnter2D() {
        Debug.Log("Trigger");
    } */
}
