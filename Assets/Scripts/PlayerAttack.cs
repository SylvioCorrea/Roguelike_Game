using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Transform attackPos;
    public float attackRange;
    public LayerMask enemyLayerMask;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1")) {
            Collider2D[] enemiesHit = Physics2D.OverlapCircleAll(attackPos.position, attackRange, enemyLayerMask);
            foreach(Collider2D e in enemiesHit) {
                Debug.Log("enemy hit!!");
                e.GetComponent<EnemyState>().TakeDamage(10);
            }
        }
    }

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
