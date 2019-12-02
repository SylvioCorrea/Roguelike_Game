using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestScript : MonoBehaviour
{
    public bool canBeOpened;
    public bool isOpen;
    public Animator animator;

    public Transform lootEmpty;
    
    public GameObject[] generalContents; //General contents of a chest should have a RigidBody
    public Item[] itemContents; //Contents of the Item class. Will be inserted into a lootEmpty upon opening
    //A better class design would avoid having 2 different loot lists in each chest, but this will do for now.
    
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
            int totalContents = generalContents.Length + itemContents.Length;
            float radius = totalContents==0 ? 0 : 360/totalContents; //avoid division by zero if empty chest
            
            int n = 0;
            foreach(Item item in itemContents) { //Instantiate lootEmpty and add item to it
                Vector3 forceVector = Quaternion.Euler(0 , 0, 270 + n*radius) * Vector3.right;
                Transform loot = Instantiate(lootEmpty, transform.position + forceVector*0.5f, Quaternion.identity);
                loot.GetComponent<LootScript>().SetItem(item);
                loot.GetComponent<Rigidbody2D>().AddForce(forceVector*3, ForceMode2D.Impulse);
                n++;
            }
            
            foreach(GameObject gObj in generalContents) { //Instantiate all loot on the list
                Vector3 forceVector = Quaternion.Euler(0 , 0, 270 + n*radius) * Vector3.right;
                GameObject loot = Instantiate(gObj, transform.position + forceVector*0.5f, Quaternion.identity);
                loot.GetComponent<Rigidbody2D>().AddForce(forceVector*3, ForceMode2D.Impulse);
                n++;
            }


            //Trigger events tied to opening this chest if any
            OnChestOpenScript ocos = GetComponent<OnChestOpenScript>();
            if(ocos != null) {
                ocos.OnChestOpenEvent();
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
