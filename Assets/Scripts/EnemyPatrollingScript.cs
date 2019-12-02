using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrollingScript : MonoBehaviour
{
    public bool patrolling;
    public float patrolSpeed;
    
    //Enemies who patrol must have at least 2 patrol points to go back and forth
    public Transform[] patrolPoints;
    public int currentPatrolPointIndex;
    public bool reachedEndOfPatrolPath;
    
    public Rigidbody2D rigidBody;
    
    Animator animator;

    public EnemyMovementScript enemyMovementScript;

    void Awake()
    {
        enemyMovementScript = GetComponent<EnemyMovementScript>();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(patrolling) {
            Vector3 targetLocation = patrolPoints[currentPatrolPointIndex].position;
            float patrolPointDistance = Vector3.Distance(transform.position, targetLocation);
            
            
            if(patrolPointDistance > 0.1f) { //Keep moving
                Vector3 velVector = (targetLocation - transform.position).normalized;
                rigidBody.velocity = velVector * patrolSpeed;
                enemyMovementScript.TurnIfNeeded();
            } else { //Notify movement script that one of the patrol points has been reached
                enemyMovementScript.PatrolPointReached();
            }
        }
    }

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
}
