using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCoreScript : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    public bool dead;

    public bool flinched;
    float flinchCooldown;
    
    public float poisonTimer;
    float poisonTimerMax = 5f;
    public float poisonTickTimer;
    float poisonTickTimerMax = 0.5f;
    
    public bool invulnerable;
    float invulnerabilityTimer;

    AnimationScript aniscr;
    Rigidbody2D playerRigidbody;
    SpriteRenderer spriteRenderer;
    public PlayerAttack playerAttackScript;

    public Image healthBar;


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar = GameObject.FindGameObjectWithTag("HealthBar").GetComponent<Image>();
        healthBar.fillAmount = 1;
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
        } else if(currentHealth<=0) {
            dead = true;
            playerRigidbody.velocity = Vector3.zero;
            playerRigidbody.isKinematic = true; //Physics will no longer apply
            aniscr.Die();
        }

        if(invulnerable) {
            invulnerabilityTimer -= Time.deltaTime;
            if(invulnerabilityTimer <= 0) {
                invulnerable = false;
            }
        }

        //Poisoning routine: suffer one tick of damage every half second until poison wears off
        if(poisonTimer > 0) { //Is poisoned
            if(poisonTickTimer <= 0) {
                TakeDamage(maxHealth/20); //Damage tick
                poisonTickTimer = poisonTickTimerMax;
            } else {
                poisonTickTimer -= Time.deltaTime;
            }
            poisonTimer-=Time.deltaTime;
            if(poisonTimer<=0) { //Poison wore off, reset sprite color
                spriteRenderer.color = Color.white;
            }
        }
    }

    public void TakeHit(AttackInfo aInfo)
    {
        if(!invulnerable) {
            TakeDamage(aInfo.attackPower);
            flinched = true;
            flinchCooldown = 0.5f;
            aniscr.Flinch();

            invulnerable = true;
            invulnerabilityTimer = 1f;

            //Stop player movement
            playerRigidbody.velocity = Vector3.zero;
            //Push player
            playerRigidbody.AddForce(aInfo.forceVector, ForceMode2D.Impulse);
            
            if(aInfo.element == Element.POISON) {
                Poison();
            }
        }
    }

    public void TakeDamage(float damage) {
        //Health does not drop below zero
        currentHealth = Mathf.Max(currentHealth - damage, 0);
        //Update healthbar
        healthBar.fillAmount = currentHealth/maxHealth;
    }

    public void Poison() {
        poisonTimer = poisonTimerMax;
        poisonTickTimer = poisonTickTimerMax;
        spriteRenderer.color = new Color32(0x9C, 0xB5, 0x50, 0xFF); //Greenish tint
    }

    public void EquipWeapon(Weapon w) {
        playerAttackScript.attackPower = w.attackPower;
        playerAttackScript.attackForce = w.attackForce;
        playerAttackScript.attackCoolDown = w.attackCoolDown;
        playerAttackScript.hitboxRadius = w.attackRadius;
        playerAttackScript.element = w.element;
    }

    public bool CanMove() {
        return !(flinched || dead);
    }
}
