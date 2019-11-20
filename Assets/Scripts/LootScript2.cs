using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootScript2 : MonoBehaviour
{
    public Item item;
    public SpriteRenderer spriteRenderer;
    public bool canBePickedUp;

    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    Vector2 hotSpot;

    public InventoryManager inventoryManager;
    public void Awake()
    {
        Transform temp = GameObject.FindWithTag("Canvas").transform;
        temp = temp.Find("PauseMenuEmpty/PauseMenu");
        inventoryManager = temp.GetComponent<InventoryManager>();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        hotSpot = new Vector2(cursorTexture.width/2, cursorTexture.height/2);
        if(item != null) {
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            spriteRenderer.sprite = item.GetSprite();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetItem(Item i)
    {
        item = i;
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        spriteRenderer.sprite = item.GetSprite();
    }

    public void OnMouseEnter()
    {
        canBePickedUp = true;
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
    }

    public void OnMouseExit()
    {
        Cursor.SetCursor(null, Vector2.zero, cursorMode);
        canBePickedUp = false;
    }

    public void OnMouseDown()
    {
        if(inventoryManager.PickUpItem(item)) {
            Cursor.SetCursor(null, Vector2.zero, cursorMode);
            Destroy(gameObject);
        }
    }
}
