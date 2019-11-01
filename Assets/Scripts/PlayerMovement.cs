using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed = 5f;
    bool isWalking;
    Rigidbody2D rigidBody;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        isWalking = false;
    }

    void Update()
    {
        float inputH = Input.GetAxis("Horizontal");
        float inputV = Input.GetAxis("Vertical");
        
        float deltaH = inputH * walkSpeed;
        float deltaV = inputV * walkSpeed;
        
        if(inputH != 0 || inputV != 0) {
            isWalking = true;
        }
        
        Vector2 vel = new Vector2(deltaH, deltaV);
        rigidBody.velocity = vel;
    }
}