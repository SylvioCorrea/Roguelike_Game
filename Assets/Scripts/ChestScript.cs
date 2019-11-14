﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestScript : MonoBehaviour
{
    public bool canBeOpened;
    public bool isOpen;
    public Animator animator;

    public Transform lootEmpty;
    public Weapon[] contents;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(canBeOpened && !isOpen && Input.GetKeyDown(KeyCode.E)) {
            isOpen = true;
            animator.Play("ChestOpen");
            //Release all loot
            int n = 1;
            foreach(Weapon item in contents) {
                Vector3 forceVector = Quaternion.Euler(0 , 0, n*80) * Vector3.right;
                Transform loot = Instantiate(lootEmpty, transform.position+forceVector*0.5f, Quaternion.identity);
                loot.GetComponent<LootScript>().SetItem(item);
                loot.GetComponent<Rigidbody2D>().AddForce(forceVector*5, ForceMode2D.Impulse);
                n++;
                Debug.Log(forceVector);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Player")) {
            Debug.Log("enter");
            canBeOpened = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if(col.CompareTag("Player")) {
            Debug.Log("exit");
            canBeOpened = false;
        }
    }

}
