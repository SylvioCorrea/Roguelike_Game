using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : MonoBehaviour
{
    public int health;
    float hurt;
    // Start is called before the first frame update
    void Start()
    {
        health = 100;
        hurt = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0) {
            Destroy(gameObject);
        }
        if(hurt > 0) {
            hurt -= Time.deltaTime;
        } else {
            GetComponent<SpriteRenderer>().color = Color.white;
        }

    }

    public void TakeDamage(int damage) {
        health -= damage;
        Debug.Log("Damage taken");
        GetComponent<SpriteRenderer>().color = Color.red;
        hurt = 0.5f;
    }
}
