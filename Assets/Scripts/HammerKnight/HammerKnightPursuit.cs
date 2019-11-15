using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerKnightPursuit : MonoBehaviour
{
    public Transform target;
    public bool lockedOn;
    public Vector3 targetLocation;
    public float speed;
    public float alertDistance;
    public float targetDistance;
    public float attackCoolDownPreset;
    float attackCoolDown;
    Rigidbody2D rigidBody;
    Animator animator;
    EnemyState state;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        state = GetComponent<EnemyState>();
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if(lockedOn) {
            float distance = Vector2.Distance(transform.position, targetLocation);
            if(distance > targetDistance) {
                    Vector3 vec = (targetLocation - transform.position).normalized;
                    rigidBody.velocity = vec * speed;
                    animator.Play("HammerKnightDash2");

                //Attack
                } else {
                    animator.Play("HammerKnightAttack");
                    attackCoolDown = attackCoolDownPreset;
                    lockedOn = false;
                }

        } else if(attackCoolDown<=0) {
            float distance = Vector2.Distance(transform.position, target.position);

            if(distance < alertDistance) {
                Vector3 vec = (target.position - transform.position).normalized;
                //Flip sprite if necessary
                if(vec.x != 0 && Mathf.Sign(vec.x)==Mathf.Sign(transform.localScale.x)) {
                    transform.localScale = Vector3.Scale(transform.localScale, new Vector3(-1,1,1));
                }

                //Dash towards player
                if(distance > targetDistance) {
                    rigidBody.velocity = vec * speed;
                    targetLocation = target.position;
                    animator.Play("HammerKnightDash2");
                    lockedOn = true;
                
                //Attack
                } else {
                    animator.Play("HammerKnightAttack");
                    attackCoolDown = attackCoolDownPreset;
                }

                
            }
        } else {
            attackCoolDown -= Time.deltaTime;
        }
    }
}
