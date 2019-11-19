using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    public InventorySlotScript inventorySlot;
    
    public TextMeshProUGUI attackValue;
    public TextMeshProUGUI forceValue;
    public TextMeshProUGUI reachValue;
    public TextMeshProUGUI speedValue;
    public TextMeshProUGUI elementValue;

    public void Awake()
    {
        Transform statsUI = transform.Find("Stats");
        attackValue = statsUI.Find("AttackValue").GetComponent<TextMeshProUGUI>();
        ScreamIfNull(attackValue, "AttackValue");
        forceValue = statsUI.Find("ForceValue").GetComponent<TextMeshProUGUI>();
        reachValue = statsUI.Find("ReachValue").GetComponent<TextMeshProUGUI>();
        speedValue = statsUI.Find("SpeedValue").GetComponent<TextMeshProUGUI>();
        elementValue = statsUI.Find("ElementValue").GetComponent<TextMeshProUGUI>();
        inventorySlot.inventoryManager = this;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Called by inventory buttons
    public void InventoryEquip(Weapon w)
    {
        GameObject.FindWithTag("Player").GetComponent<PlayerCoreScript>().EquipWeapon(w);
        attackValue.text = w.attackPower.ToString();
        forceValue.text = w.attackForce.ToString();
        speedValue.text = (1 / w.attackCoolDown).ToString();
        reachValue.text = w.attackRadius.ToString();
        elementValue.text = w.element.ToString();
    }

    public void PickUpItem(Item item)
    {
        inventorySlot.StoreItem(item);
    }

    public void ScreamIfNull(Object o, string scream)
    {
        if(!o) {
            Debug.Log(scream);
        }
    }
}
