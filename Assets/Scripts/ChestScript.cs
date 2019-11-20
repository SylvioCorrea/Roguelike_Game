using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestScript : MonoBehaviour
{
    public bool canBeOpened;
    public bool isOpen;
    public Animator animator;

    public Transform lootEmpty;
    
    public GameObject[] contents; //Contents of a chest should have a RigidBody
    
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
            //Release all loot in a circular fashion around the chest
            float radius = 360/contents.Length;
            int n = 1;
            foreach(GameObject item in contents) {
                Vector3 forceVector = Quaternion.Euler(0 , 0, n*radius) * Vector3.right;
                GameObject loot = Instantiate(item, transform.position+forceVector*0.5f, Quaternion.identity);
                loot.GetComponent<Rigidbody2D>().AddForce(forceVector*3, ForceMode2D.Impulse);
                n++;
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
