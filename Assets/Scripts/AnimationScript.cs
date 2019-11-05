using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationScript : MonoBehaviour
{
    Animator animator;
    
    void Start() {
        animator = GetComponent<Animator>();
    }
    
    void Update() {
        
    }

    public void Flinch()
    {
        animator.SetBool("isWalking", false);
        animator.SetBool("isFlinched", true);
    }

    public void Unflinch()
    {
        animator.SetBool("isFlinched", false);
    }

    public void Walk(float inputH)
    {
        animator.SetBool("isWalking", true);
        //Flips sprite if needed
        if(inputH !=0 && Mathf.Sign(inputH) != Mathf.Sign(transform.localScale.x)) {
            transform.localScale = Vector3.Scale(transform.localScale, new Vector3(-1, 1, 1));
        }
    }
    
    public void Iddle()
    {
        animator.SetBool("isWalking", false);
    }
    
    
    
    
    
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
