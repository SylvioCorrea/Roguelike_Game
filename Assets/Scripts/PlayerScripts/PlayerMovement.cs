using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed = 5f;
    bool isWalking;
    Rigidbody2D rigidBody;

    PlayerCoreScript playerCoreScript;
    AnimationScript aniscr;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        playerCoreScript = GetComponent<PlayerCoreScript>();
        aniscr = GetComponent<AnimationScript>();
    }

    void Update()
    {
        if(playerCoreScript.CanMove()) {
            float inputH = Input.GetAxis("Horizontal");
            float inputV = Input.GetAxis("Vertical");
            
            float deltaH = inputH * walkSpeed;
            float deltaV = inputV * walkSpeed;
            
            Vector2 vel = new Vector2(deltaH, deltaV);
            rigidBody.velocity = vel;
            
            //Move this to the animation script
            if(inputH != 0 || inputV != 0){
                aniscr.Walk(inputH);
            } else {
                aniscr.Iddle();
            }
        }
    }
}