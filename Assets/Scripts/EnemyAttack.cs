using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    
    public int targetLayer;
    public float attackPower;
    public float attackForce;
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
        if(col.gameObject.layer.Equals(targetLayer)) {
            col.gameObject.GetComponent<PlayerState>().TakeHit(attackPower, transform.position, attackForce);
            Debug.Log("Player hit!");
        }
    }
}
