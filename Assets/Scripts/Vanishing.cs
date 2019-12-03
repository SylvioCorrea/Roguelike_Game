using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vanishing : MonoBehaviour
{
    public float timeToVanish;
    public float timeLeft;
    public bool isVanishing;
    public Animator animator;
    
    void Awake()
    {
        animator = GetComponent<Animator>();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        timeLeft = timeToVanish;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isVanishing) {
            if(timeLeft <= timeToVanish * 0.4) {
                isVanishing = true;
                animator.Play("PotionVanish");
            }
        }
        
        if(timeLeft <= 0) {
            Destroy(gameObject);
        } else {
            timeLeft -= Time.deltaTime;
        }
    }
}
