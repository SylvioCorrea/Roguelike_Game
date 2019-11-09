using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimePursuit : MonoBehaviour
{
    public Transform target;
    public float impulse; //Slimes push forward in steps
    public float alertDistance;
    public float targetDistance;
    public float waitToPush; //Slimes wait for some time aftereach push towards the target
    public Rigidbody2D rigidBody;
    Animator animator;
    SlimeState state;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        state = GetComponent<SlimeState>();
        waitToPush = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(waitToPush <= 0) {
            //The slime does not go after the player if being hurt or during division
            if(state.hurt <= 0 && state.dividing <= 0) {
                float distance = Vector2.Distance(transform.position, target.position);
                
                if(distance > targetDistance && distance < alertDistance) {
                    Vector3 forceVector = (target.position - transform.position).normalized * impulse;
                    rigidBody.AddForce(forceVector, ForceMode2D.Impulse);
                    waitToPush = 0.8f;

                    //flips sprite if necessary
                    if(forceVector.x != 0 && Mathf.Sign(forceVector.x)!=Mathf.Sign(transform.localScale.x)) {
                        transform.localScale = Vector3.Scale(transform.localScale, new Vector3(-1,1,1));
                    }

                    //start division timer if it's not already running
                    state.StartDivisionTimer();
                }
            }
        } else {
            waitToPush -= Time.deltaTime;
        }
    }
}
