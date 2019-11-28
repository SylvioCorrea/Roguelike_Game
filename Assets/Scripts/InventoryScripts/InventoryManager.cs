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

    public void Awake()
    {
        playerCoreScript = GameObject.FindWithTag("Player").GetComponent<PlayerCoreScript>();
        Transform statsUI = transform.Find("Stats");
        attackValue = statsUI.Find("AttackValue").GetComponent<TextMeshProUGUI>();
        forceValue = statsUI.Find("ForceValue").GetComponent<TextMeshProUGUI>();
        reachValue = statsUI.Find("ReachValue").GetComponent<TextMeshProUGUI>();
        speedValue = statsUI.Find("SpeedValue").GetComponent<TextMeshProUGUI>();
        elementValue = statsUI.Find("ElementValue").GetComponent<TextMeshProUGUI>();
        foreach(InventorySlotScript iss in inventorySlots) {
            iss.inventoryManager = this;
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        if(playerCoreScript.weapon != null) {

        }
    }

    // Update is called once per frame
    void Update()
    {
        
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
    }

    public void ScreamIfNull(Object o, string scream)
    {
        if(!o) {
            Debug.Log(scream);
        }
    }
}
