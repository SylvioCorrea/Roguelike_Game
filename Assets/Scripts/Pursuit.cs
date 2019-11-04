using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pursuit : MonoBehaviour
{
    public Transform target;
    public float speed;
    public float alertDistance;
    public float targetDistance;
    public Rigidbody2D rigidBody;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector2.Distance(transform.position, target.position);
        if(distance > targetDistance && distance < alertDistance) {
            Vector3 vec = target.position - transform.position;
            rigidBody.velocity = vec.normalized * speed;
        }
    }
}
