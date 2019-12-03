using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskedOrcMovement : EnemyMovementScript
{
    
    public Transform target;
    public float idleMaxTime;
    public float idleTimer;

    public float waitMaxTime;
    public float waitTimer;

    public float speed;
    
    public float attackTimerMax;
    public float attackTimer;
    public float alertDistance;
    public float targetTooCloseDistance; //Will flee if at this distance
    public float targetTooFarDistance; //Will pursuit if at this distance
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
        StartIdle();
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
        if(attackTimer>0) { //attackTimer always decreases no matter what state the enemy is in
            attackTimer -= Time.deltaTime;
        }

        switch(coreScript.state) {
            
            case EnemyStateEnum.WAIT: //Brief inactivity right after an attack
                if(waitTimer<=0) {
                    StartIdle();
                } else {
                    waitTimer -= Time.deltaTime;
                }
                break;
            
            
            case EnemyStateEnum.ENGAGE:
                if(!TargetIsClose()) { //Target is too far. Stop engaging.
                    StartIdle();
                } else {
                    
                    if(attackTimer <= 0) { //Enemy can attack again
                        ThrowProjectile();
                        attackTimer = attackTimerMax;
                        StartWait();


                    } else { //It's too soon to attack again
                        if(TargetIsWayTooClose()) { //Flee from target
                            Vector3 speedUnitVector = (transform.position - target.position).normalized;
                            rigidBody.velocity = speedUnitVector * speed;
                            TurnIfNeeded();
                        } else if(TargetIsWayTooFar()) {
                            Vector3 speedUnitVector = (target.position - transform.position).normalized;
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
                } else if(idleTimer>0) { //Enemy resting between patrols
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
        idleTimer = idleMaxTime;
        coreScript.state = EnemyStateEnum.IDLE;
    }

    void StartWait()
    {
        waitTimer = waitMaxTime;
        coreScript.state = EnemyStateEnum.WAIT;
    }

    bool TargetIsClose()
    {
        return Vector3.Distance(target.position, transform.position) < alertDistance;
    }

    bool TargetIsWayTooClose()
    {
        return Vector3.Distance(target.position, transform.position) <= targetTooCloseDistance;
    }

    bool TargetIsWayTooFar()
    {
        return Vector3.Distance(target.position, transform.position) >= targetTooFarDistance;
    }

    override public void PatrolPointReached()
    {
        StartIdle();
    }

    public void ThrowProjectile()
    {
        Vector3 speedUnitVector = (target.position - transform.position).normalized;
        
        //Turn sprite towards player
        float vecX = speedUnitVector.x;
        if(vecX != 0 && Mathf.Sign(vecX)!=Mathf.Sign(transform.localScale.x)) {
            transform.localScale = Vector3.Scale(transform.localScale, new Vector3(-1,1,1));
        }
        
        //Spawn projectile slightly away from enemy to avoid instantly hitting walls
        Transform instantiatedProjectile = Instantiate(projectile, transform.position + speedUnitVector * 0.5f, Quaternion.identity);

        instantiatedProjectile.GetComponent<Rigidbody2D>().velocity = speedUnitVector * instantiatedProjectile.GetComponent<EnemyProjectile>().speed;
    }
}
