using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinMovement : MonoBehaviour, IEnemyPursuit
{
    public Transform target;
    public float idleWaitTime;
    public float idleTimer;
    public float pursuitSpeed;
    public float patrolSpeed;
    
    //Enemies who patrol must have at least 2 patrol points to go back and forth
    public Transform[] patrolPoints;
    public int currentPatrolPointIndex;
    public bool reachedEndOfPatrolPath;
    
    public float alertDistance;
    public float targetDistance;
    public Rigidbody2D rigidBody;
    Animator animator;
    GoblinCoreScript coreScript;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        coreScript = GetComponent<GoblinCoreScript>();
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        idleTimer = idleWaitTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(coreScript.hurt<=0) { //The enemy does not try to move if it's hurt
            
            
            switch (coreScript.state) {
                
                
                
                case EnemyStateEnum.PATROL:
                    //Debug.Log("patrol");
                    if(Vector3.Distance(target.position, transform.position)<=alertDistance) { //Player is close enough for pursuit
                        StartPursuit();
                    } else {
                        //Checks if already arrived target location
                        Vector3 targetLocation = patrolPoints[currentPatrolPointIndex].position;
                        float patrolPointDistance = Vector3.Distance(transform.position, targetLocation);
                        
                        
                        if(patrolPointDistance > 0.1f) { //Keep moving
                            Vector3 velVector = (targetLocation - transform.position).normalized;
                            rigidBody.velocity = velVector * patrolSpeed;
                            TurnIfNeeded();
                            // Debug.Log(patrolPointDistance);
                            // Debug.Log(rigidBody.velocity.x);
                            // Debug.Log(rigidBody.velocity.y);
                        } else { //Rest for a while before going to the next patrol point
                            StartIdle();
                        }
                    }
                    break;
                
                
                case EnemyStateEnum.PURSUIT:
                    //Debug.Log("pursuit");
                    float distanceFromPlayer = Vector3.Distance(target.position, transform.position);
                    if(distanceFromPlayer > alertDistance) { //Player is too far
                        StartIdle();
                    } else if(distanceFromPlayer > targetDistance) { //Isn't close enough to the player
                        Vector3 velVector = (target.position - transform.position).normalized;
                        rigidBody.velocity = velVector * pursuitSpeed;
                        TurnIfNeeded();
                    }
                    break;
                
                
                default: //IDLE
                    //Debug.Log("idle");
                    if(Vector3.Distance(target.position, transform.position)<=alertDistance) { //Player is close enough for pursuit
                        StartPursuit();
                    } else if(idleTimer>0) { //Enemy resting
                        idleTimer -= Time.deltaTime;
                    } else {
                        StartPatrol();
                    }
                    break;

            }
        }
    }

    void StartPatrol()
    {
        if(patrolPoints.Length>1) {
            coreScript.state = EnemyStateEnum.PATROL;
            SetNextPatrolPoint();
            animator.SetBool("isRunning", true);
        } else {
            StartIdle();
        }

    }

    void StartPursuit()
    {
        coreScript.state = EnemyStateEnum.PURSUIT;
        animator.SetBool("isRunning", true);
    }

    void StartIdle()
    {
        coreScript.state = EnemyStateEnum.IDLE;
        animator.SetBool("isRunning", false);
        idleTimer = idleWaitTime;
    }

    //Will set the index of the next patrol point the enemy should go after
    void SetNextPatrolPoint() {
        if(!reachedEndOfPatrolPath) { //Has yet to complete patrol path
            currentPatrolPointIndex++;
            if(currentPatrolPointIndex==patrolPoints.Length-1) { //Completed route
                reachedEndOfPatrolPath = true;
            }

        } else { //Has completed route and is going back one step at a time
            currentPatrolPointIndex--;
            if(currentPatrolPointIndex==0) { //Went all the way back, redo route
                reachedEndOfPatrolPath = false;
            }
        }
    }

    //Turn the sprie if needed
    public void TurnIfNeeded() {
        float vecX = rigidBody.velocity.x;
        if(vecX != 0 && Mathf.Sign(vecX)!=Mathf.Sign(transform.localScale.x)) {
            transform.localScale = Vector3.Scale(transform.localScale, new Vector3(-1,1,1));
        }
    }

    public void SetAlertDistance(float n)
    {
        alertDistance = n;
    }
}
