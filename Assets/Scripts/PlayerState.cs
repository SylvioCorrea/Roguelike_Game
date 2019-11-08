using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public float health;
    public bool flinched;
    float flinchCooldown;
    public bool invulnerable;
    float invulnerabilityCooldown;

    AnimationScript aniscr;
    Rigidbody2D playerRigidbody;
    // Start is called before the first frame update
    void Start()
    {
        health = 100;
        flinched = false;
        invulnerable = false;
        aniscr = GetComponent<AnimationScript>();
        playerRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(flinched == true) {
            flinchCooldown -= Time.deltaTime;
            if(flinchCooldown <= 0) {
                flinched = false;
                aniscr.Unflinch();
            }
        }

        if(invulnerable == true) {
            invulnerabilityCooldown -= Time.deltaTime;
            if(invulnerabilityCooldown <= 0) {
                invulnerable = false;
            }
        }
    }

    public void TakeHit(float attackPower, Vector3 enemyPosition, float attackForce)
    {
        if(!invulnerable) {
            health -= attackPower;
            flinched = true;
            flinchCooldown = 0.5f;
            invulnerable = true;
            invulnerabilityCooldown = 1f;
            playerRigidbody.velocity = Vector3.zero;
            Vector3 forceVector = (transform.position - enemyPosition).normalized * attackForce;
            playerRigidbody.AddForce(forceVector, ForceMode2D.Impulse);
            aniscr.Flinch();
        }
    }
}
