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
                inventoryManager.InventoryEquip((Weapon)item);
            }
        };
    }

    //Should be called when storing an item in this slot
    public void StoreItem(Item item)
    {
        this.item = item;
        
        
        // Transform imgTransform = transform.Find("ButtonImage");
        // if(!imgTransform) {Debug.Log("ButtonImage is null");}
        // //Get Image component from child
        // itemImage = imgTransform.GetComponent<Image>();
        // if(!imgTransform) {Debug.Log("itemImage null");}

        itemImage.sprite = item.GetSprite();
        itemImage.preserveAspect = true;
        itemImage.color = Color.white;
        itemImage.gameObject.SetActive(true);
    }

    public void PrintMessage() {
        Debug.Log("Button pressed");
    }
}
