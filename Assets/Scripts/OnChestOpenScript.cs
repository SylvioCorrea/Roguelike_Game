using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Activates or deactivates transforms upon opening a chest
public class OnChestOpenScript : MonoBehaviour
{
    
    public Transform[] toActivate;
    public Transform[] toDeactivate;

    
    // Start is called before the first frame update
    public void OnChestOpenEvent()
    {
        foreach(Transform t in toActivate) {
            t.gameObject.SetActive(true);
        }

        foreach(Transform t in toDeactivate) {
            t.gameObject.SetActive(false);
        }
    }
}
