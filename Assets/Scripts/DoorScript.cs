using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public bool playerInsideTriggerCollider;
    public InventoryManager inventoryManager;
    public GameManagerScript gameManager;

    void Awake()
    {
        inventoryManager = GameObject.FindWithTag("PauseMenu").GetComponent<InventoryManager>();
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManagerScript>();
    }
    void Update()
    {
        if(playerInsideTriggerCollider && Input.GetKeyDown(KeyCode.E)) {
            if(inventoryManager.UseKey()) {
                gameManager.StageClear();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        playerInsideTriggerCollider = true;
    }

    void OnTriggerExit2D(Collider2D col)
    {
        playerInsideTriggerCollider = false;
    }
}
