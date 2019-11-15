using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public float maxHealth;
    public float health;
    public bool flinched;
    float flinchCooldown;
    public bool poisoned;

    public float poisonTickTimer;
    public bool invulnerable;
    float invulnerabilityCooldown;

    AnimationScript aniscr;
    Rigidbody2D playerRigidbody;
    SpriteRenderer spriteRenderer;
    public PlayerAttack playerAttackScript;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        flinched = false;
        invulnerable = false;
        aniscr = GetComponent<AnimationScript>();
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerAttackScript = GetComponent<PlayerAttack>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(flinched) {
            flinchCooldown -= Time.deltaTime;
            if(flinchCooldown <= 0) {
                flinched = false;
                aniscr.Unflinch();
            }
        }

        if(invulnerable) {
            invulnerabilityCooldown -= Time.deltaTime;
            if(invulnerabilityCooldown <= 0) {
                invulnerable = false;
            }
        }

        if(poisoned) {
            if(poisonTickTimer <= 0) {
                health -= maxHealth/20;
                poisonTickTimer = 0.5f;
            } else {
                poisonTickTimer -= Time.deltaTime;
            }
        }
    }

    public void TakeHit(AttackInfo aInfo)
    {
        if(!invulnerable) {
            health -= aInfo.attackPower;
            flinched = true;
            flinchCooldown = 0.5f;
            invulnerable = true;
            invulnerabilityCooldown = 1f;
            playerRigidbody.velocity = Vector3.zero;
            playerRigidbody.AddForce(aInfo.forceVector, ForceMode2D.Impulse);
            aniscr.Flinch();
            if(aInfo.element == Element.POISON) {
                StartCoroutine(Poison());
            }
        }
    }

    public IEnumerator Poison() {
        poisoned = true;
        spriteRenderer.color = new Color32(0x9C, 0xB5, 0x50, 0xFF);
        Debug.Log("poisoned");
        yield return new WaitForSeconds(3);
        poisoned = false;
        spriteRenderer.color = Color.white;
        Debug.Log("unpoisoned");
    }

    public void EquipWeapon(Weapon w) {
        playerAttackScript.attackPower = w.attackPower;
        playerAttackScript.attackForce = w.attackForce;
        playerAttackScript.attackCoolDown = w.attackCoolDown;
        playerAttackScript.hitboxRadius = w.attackRadius;
        playerAttackScript.element = w.element;
    }
}
