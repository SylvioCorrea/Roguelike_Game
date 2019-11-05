using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPursuit : MonoBehaviour
{
    public Transform target;
    public float speed;
    public float alertDistance;
    public float targetDistance;
    public Rigidbody2D rigidBody;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector2.Distance(transform.position, target.position);
        
        if(distance > targetDistance && distance < alertDistance) {
            Vector3 vec = target.position - transform.position;
            rigidBody.velocity = vec.normalized * speed;
            
            //Vira o sprite se necessario
            if(vec.x != 0 && Mathf.Sign(vec.x)!=Mathf.Sign(transform.localScale.x)) {
                transform.localScale = Vector3.Scale(transform.localScale, new Vector3(-1,1,1));
            }

            animator.SetBool("isRunning", true);
            

        
        } else {
            animator.SetBool("isRunning", false);
        }
    }
}
