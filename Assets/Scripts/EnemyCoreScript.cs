using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public abstract class EnemyCoreScript : MonoBehaviour, IDeathNotifier
{
    public float healthMax;
    public float health;
    public float hurt;

    public EnemyStateEnum state;

    public GameObject damageNumbersPrefab;
    public Transform deathEffect;

    public List<IListener> listeners;
    
    public void Awake()
    {
        listeners = new List<IListener>();
    }

    public abstract void TakeHit(AttackInfo aInfo);
    
    public void TakeDamage(float n)
    {
        health -= n;
        GameObject damageNumbers = Instantiate(damageNumbersPrefab, transform.position, Quaternion.identity);
        damageNumbers.GetComponent<TextMeshPro>().text = ((int)n).ToString();
    }

    public void Die()
    {
        foreach(IListener l in listeners) {
            l.Notify();
        }
        if(deathEffect) {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }

    public void SubscribeListener(IListener l) {
        listeners.Add(l);
    }

}
