using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootScript : MonoBehaviour
{
    public Weapon weapon;
    public SpriteRenderer spriteRenderer;
    public bool canBePickedUp;

    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    Vector2 hotSpot;
    // Start is called before the first frame update
    void Start()
    {
        hotSpot = new Vector2(cursorTexture.width/2, cursorTexture.height/2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetItem(Weapon i)
    {
        weapon = i;
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        spriteRenderer.sprite = weapon.sprite;
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
        Cursor.SetCursor(null, Vector2.zero, cursorMode);
        GameObject.FindWithTag("Player").GetComponent<PlayerCoreScript>().EquipWeapon(weapon);
        Destroy(gameObject);
    }
}
