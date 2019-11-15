using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerKnightAttack : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Player")) {
            Debug.Log("hammered");
        }
    }


    public void OnTriggerStay2D(Collider2D col)
    {
        if(col.CompareTag("Player")) {
            Debug.Log("hammered");
        }
    }
}
