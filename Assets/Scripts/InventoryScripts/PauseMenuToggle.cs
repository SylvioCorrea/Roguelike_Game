using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuToggle : MonoBehaviour
{
    public bool menuIsOpen;
    public Transform pauseMenu;
    
    public void Awake()
    {
        pauseMenu = transform.Find("PauseMenu");
    }

    // Start is called before the first frame update
    void Start()
    {
        menuIsOpen = false;
        pauseMenu.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) {
            if(!menuIsOpen) { //Open menu
                pauseMenu.gameObject.SetActive(true);
                menuIsOpen = true;
            } else { //Close menu
                pauseMenu.gameObject.SetActive(false);
                menuIsOpen = false;
            }
        }
    }
}
