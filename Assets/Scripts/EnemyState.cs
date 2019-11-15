using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public abstract class EnemyState : MonoBehaviour
{
    public float health;
    public float hurt;

    public GameObject damageNumbersPrefab;
    
    public abstract void TakeHit(AttackInfo aInfo);
    
    public void TakeDamage(float n)
    {
        health -= n;
        GameObject damageNumbers = Instantiate(damageNumbersPrefab, transform.position, Quaternion.identity);
        damageNumbers.GetComponent<TextMeshPro>().text = ((int)n).ToString();
    }

}
