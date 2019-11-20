using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlotScript : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public Image itemImage;
    public Item item;
    
    public void Awake()
    {
        //Get child
        Transform imgTransform = transform.Find("SlotButton/ButtonImage");
        //Get Image component from child
        itemImage = imgTransform.GetComponent<Image>();
    }
    
    public void OnButtonClicked()
    {
        if(item) {
            if(item is Weapon) {
                Weapon w = (Weapon)item;
                item = null;
                inventoryManager.InventoryEquip(w, this);
            }
        };
    }

    //Should be called when storing an item in this slot.
    //Item can be null for the purposes of freeing the slot.
    public void StoreItem(Item item)
    {
        this.item = item;
        UpdateSlotUI();
    }

    public void UpdateSlotUI()
    {
        if(item != null) {
            itemImage.sprite = item.GetSprite();
            itemImage.preserveAspect = true;
            itemImage.color = Color.white;

            //Possibly useless code. Think it's always active now.
            itemImage.gameObject.SetActive(true);

        } else {
            itemImage.sprite = null;
            itemImage.color = new Color(1, 1, 1, 0); //transparent image
        }
    }

    public void PrintMessage() {
        Debug.Log("Button pressed");
    }
}
