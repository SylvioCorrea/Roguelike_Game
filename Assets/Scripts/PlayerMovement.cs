using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed = 5f;
    bool isWalking;
    Rigidbody2D rigidBody;

    PlayerState playerState;
    AnimationScript aniscr;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        playerState = GetComponent<PlayerState>();
        aniscr = GetComponent<AnimationScript>();
    }

    void Update()
    {
        if(playerState.flinched==false) {
            float inputH = Input.GetAxis("Horizontal");
            float inputV = Input.GetAxis("Vertical");
            
            float deltaH = inputH * walkSpeed;
            float deltaV = inputV * walkSpeed;
            
            Vector2 vel = new Vector2(deltaH, deltaV);
            rigidBody.velocity = vel;
            if(inputH != 0 || inputV != 0){
                aniscr.Walk(inputH);
            } else {
                aniscr.Iddle();
            }
        }
    }
}