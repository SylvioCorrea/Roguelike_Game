using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    public InventorySlotScript[] inventorySlots;

    public Image equipedItemImage;

    public PlayerCoreScript playerCoreScript;
    
    public TextMeshProUGUI attackValue;
    public TextMeshProUGUI forceValue;
    public TextMeshProUGUI reachValue;
    public TextMeshProUGUI speedValue;
    public TextMeshProUGUI elementValue;

    int keysHeld; //Keys held by the player
    public TextMeshProUGUI keysHeldValue;

    public void Awake()
    {
        playerCoreScript = GameObject.FindWithTag("Player").GetComponent<PlayerCoreScript>();
        Transform statsUI = transform.Find("Stats");
        attackValue = statsUI.Find("AttackValue").GetComponent<TextMeshProUGUI>();
        forceValue = statsUI.Find("ForceValue").GetComponent<TextMeshProUGUI>();
        reachValue = statsUI.Find("ReachValue").GetComponent<TextMeshProUGUI>();
        speedValue = statsUI.Find("SpeedValue").GetComponent<TextMeshProUGUI>();
        elementValue = statsUI.Find("ElementValue").GetComponent<TextMeshProUGUI>();
        keysHeldValue = statsUI.Find("KeysHeldValue").GetComponent<TextMeshProUGUI>();
        foreach(InventorySlotScript iss in inventorySlots) {
            iss.inventoryManager = this;
        }
    }

    //Called by inventory buttons
    public void InventoryEquip(Weapon w, InventorySlotScript iss)
    {
        //Return currently equipped weapon to the inventory.
        //Can be a null value.
        iss.StoreItem(playerCoreScript.weapon);
        
        //Equip weapon in the player
        playerCoreScript.EquipWeapon(w);

        UpdateStatsUI();
    }

    public void UpdateStatsUI() {
        Weapon w = playerCoreScript.weapon;
        //Change equipped weapon image
        equipedItemImage.sprite = w.GetSprite();
        equipedItemImage.color = Color.white;
        equipedItemImage.preserveAspect = true;

        //Change stats
        attackValue.text = w.attackPower.ToString();
        forceValue.text = w.attackForce.ToString("0.00");
        speedValue.text = (1 / w.attackCoolDown).ToString("0.00");
        reachValue.text = w.attackRadius.ToString();
        elementValue.text = w.element.ToString();
    }

    public bool PickUpItem(Item item)
    {
        if(item is Weapon) {
            //Search for empty inventory slot to store item
            for(int i=0; i<inventorySlots.Length; i++) {
                if(inventorySlots[i].item == null) {
                    inventorySlots[i].StoreItem(item);
                    Debug.Log("item stored");
                    return true;
                }
            }
            //There are no more empty slots
            Debug.Log("inventory full");
            return false;
        } else if (item is Key) {
            keysHeld++;
            keysHeldValue.text = keysHeld.ToString("00");
            return true;
        }
        Debug.Log("No code for this class of item yet.");
        return false;
    }

    public bool UseKey()
    {
        if(keysHeld>0) {
            keysHeld--;
            keysHeldValue.text = keysHeld.ToString("00");
            return true;
        } else {
            return false;
        }
    }
}
