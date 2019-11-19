using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Transform attackPos;
    public float hitboxRadius;
    public float hitboxDistance; //Hitbox center distance from the player
    public float attackCoolDown;
    float coolDown;
    
    public float attackPower;
    public float attackForce; //Impulse caused by attack
    public Element element;
    
    public LayerMask enemyLayerMask; //Maybe use a tag instead
    public GameObject slashEffectPrefab;

    PlayerCoreScript playerCoreScript;

    // Start is called before the first frame update
    void Start()
    {
        coolDown = 0;
        playerCoreScript = GetComponent<PlayerCoreScript>();
    }

    // Update is called once per frame
    void Update()
    {
        //Set attack hitbox in place
        //Get mouse position in world coordinates
        Vector3 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Set z of this position to zero. We are in 2d after all.
        target.z = 0f;
        //find the unit vector that goes from player to target
        target = (target - transform.position).normalized;
        //Multiply unit vector by a scalar end set its starting point at the center of the player
        attackPos.position = transform.position + (target * hitboxDistance);
        
        //Attack
        if(coolDown > 0) {
            coolDown -= Time.deltaTime;

        } else if(playerCoreScript.CanMove() && Input.GetButtonDown("Fire1")) {
                Collider2D[] enemiesHit = Physics2D.OverlapCircleAll(attackPos.position, hitboxRadius, enemyLayerMask);
                AttackInfo aInfo = new AttackInfo(attackPower, Vector3.zero, element);
                foreach(Collider2D e in enemiesHit) {
                    //forceVector is different for each enemy hit
                    aInfo.forceVector = (e.gameObject.transform.position - transform.position).normalized * attackForce;
                    e.GetComponent<EnemyCoreScript>().TakeHit(aInfo);
                }
                //Instantiate slash effect
                
                GameObject slash = Instantiate(slashEffectPrefab, attackPos.position, Quaternion.FromToRotation(Vector3.right, target));
                slash.transform.localScale *= hitboxRadius;

                coolDown = attackCoolDown;
        }
    }

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, hitboxRadius);
    }
}
