using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SlimeState : EnemyCoreScript
{
    
    /*
    Inherited variables from EnemyCoreScript:
    public float health;
    public float hurt;
    */

    //Variables taking care of division process ==============================
    public float dividing; //Time it takes for division to complete
    public float timeToDivide; //Time it takes for division process to trigger
    public float divideRangeBegin;
    public float divideRangeEnd;
    public int divisionsLeft;
    //========================================================================

    Animator animator;
    public GameObject slimePrefab;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        health = healthMax;
    }

    // Update is called once per frame
    void Update()
    {
        //Division logic
        if(timeToDivide > 0) {
            timeToDivide-=Time.deltaTime;
            if(timeToDivide<=0) {
                dividing = 2;
                animator.SetBool("isDividing", true);
            }
        } else if (dividing > 0) {
            dividing -= Time.deltaTime;
            if(dividing <= 0) {
                TriggerDivision();
                animator.SetBool("isDividing", false);
            }
        }

        if(hurt > 0) {
            hurt-=Time.deltaTime;
            if(hurt <= 0) {
                GetComponent<SpriteRenderer>().color = Color.white;
            }
        }
    }

    public override void TakeHit(AttackInfo aInfo)
    {
        TakeDamage(aInfo.attackPower);
        if(health<=0) {
            Destroy(gameObject);
        }
        //GetComponent<Rigidbody2D>().AddForce(aInfo.forceVector, ForceMode2D.Impulse);
        GetComponent<SpriteRenderer>().color = Color.red;
        hurt = 0.25f;
    }

    public void StartDivisionTimer()
    {
        if(divisionsLeft > 0 && timeToDivide <= 0) {
            timeToDivide = Random.Range(divideRangeBegin, divideRangeEnd);
        }
    }

    public void TriggerDivision()
    {
        Debug.Log("Divided");
        GameObject newSlime = Instantiate(slimePrefab, transform.position, Quaternion.identity);
        newSlime.GetComponent<Rigidbody2D>().AddForce(Vector3.left*4, ForceMode2D.Impulse);
        divisionsLeft--;
    }
}
