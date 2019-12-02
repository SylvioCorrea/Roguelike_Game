using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskedOrcMovement : EnemyMovementScript
{
    
    public Transform target;
    public float idleWaitTime;
    public float idleTimer;

    public float speed;
    
    public float attackTimerMax;
    public float attackTimer;
    public float alertDistance;
    public float targetDistance;
    Animator animator;
    MaskedOrcCoreScript coreScript;
    public Transform projectile;
    public EnemyPatrollingScript patrolScript;
    
    void Awake()
    {
        target = GameObject.FindWithTag("Player").transform;
        coreScript = GetComponent<MaskedOrcCoreScript>();
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        patrolScript = GetComponent<EnemyPatrollingScript>();
    }
    // Start is called before the first frame update
    void Start()
    {
        idleTimer = idleWaitTime;
    }

    // Update is called once per frame
    // void Update()
    // {
    //     float distance = Vector3.Distance(target.position, transform.position);
    //     //Debug.Log(distance);
    //     if(idleTimer <= 0){
    //         if(Vector3.Distance(target.transform.position, transform.position) < alertDistance) {
    //             ThrowProjectile();
    //             idleTimer = idleWaitTime;
    //         }
    //     } else {
    //         idleTimer -= Time.deltaTime;
    //     }
    // }

    void Update()
    {
        if(attackTimer>0) { //attackTimer always counts down no matter what state the enemy is in
            attackTimer -= Time.deltaTime;
        }

        switch(coreScript.state) {
            
            case EnemyStateEnum.ENGAGE:
                if(!TargetIsClose()) { //Target is too far. Stop engaging.
                    StartIdle();
                } else {
                    
                    if(attackTimer <= 0) { //Enemy can attack again
                        ThrowProjectile();
                        attackTimer = attackTimerMax;

                    } else { //It's too soon to attack again
                        if(TargetIsWayTooClose()) { //Flee from target
                            Vector3 speedUnitVector = (transform.position - target.position).normalized;
                            rigidBody.velocity = speedUnitVector * speed;
                            TurnIfNeeded();
                        }
                    }
                }
                break;
            
            case EnemyStateEnum.PATROL:
                if(TargetIsClose()) {
                    patrolScript.patrolling = false;
                    StartEngage();
                }
                break;
            
            default: //IDLE
                //Debug.Log("idle");
                if(TargetIsClose()) { //Player is close enough for pursuit
                    StartEngage();
                } else if(idleTimer>0) { //Enemy resting
                    idleTimer -= Time.deltaTime;
                } else {
                    StartPatrol();
                }
                break;
                
        }
    }


    public void StartEngage()
    {
        coreScript.state = EnemyStateEnum.ENGAGE;
    }

    void StartPatrol()
    {
        if(patrolScript) {
            patrolScript.patrolling = true;
            coreScript.state = EnemyStateEnum.PATROL;
        } else {
            StartIdle();
        }

    }

    void StartIdle()
    {
        coreScript.state = EnemyStateEnum.IDLE;
    }

    bool TargetIsClose()
    {
        return Vector3.Distance(target.position, transform.position) < alertDistance;
    }

    bool TargetIsWayTooClose()
    {
        return Vector3.Distance(target.position, transform.position) < targetDistance;
    }

    override public void PatrolPointReached()
    {
        StartIdle();
    }

    public void ThrowProjectile()
    {
        Transform instantiatedProjectile = Instantiate(projectile, transform.position, Quaternion.identity);
        Vector3 speedUnitVector = (target.position - transform.position).normalized;
        
        //Turn sprite if needed
        float vecX = speedUnitVector.x;
        if(vecX != 0 && Mathf.Sign(vecX)!=Mathf.Sign(instantiatedProjectile.localScale.x)) {
            instantiatedProjectile.localScale = Vector3.Scale(transform.localScale, new Vector3(-1,1,1));
        }

        instantiatedProjectile.GetComponent<Rigidbody2D>().velocity = speedUnitVector * instantiatedProjectile.GetComponent<EnemyProjectile>().speed;
    }
}
