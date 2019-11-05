using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public int health;
    public bool flinched;
    float flinchCooldown;
    public bool invulnerable;
    float invulnerabilityCooldown;

    AnimationScript aniscr;
    // Start is called before the first frame update
    void Start()
    {
        health = 100;
        flinched = false;
        invulnerable = false;
        aniscr = GetComponent<AnimationScript>();
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

    public void TakeDamage(int damage)
    {
        if(!invulnerable) {
            health -= damage;
            flinched = true;
            flinchCooldown = 0.5f;
            invulnerable = true;
            invulnerabilityCooldown = 1f;
            aniscr.Flinch();
        }
    }
}
