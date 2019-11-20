using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Should be placed in a child containing the trigger collider
//After pickup, the child will also desstroy it's parent
public class HealthPotionScript : MonoBehaviour
{
    public float healthRecoveryRatio;

    public void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Player")) {
            col.GetComponent<PlayerCoreScript>().RecoverHealthRatio(healthRecoveryRatio);
            Debug.Log("health recovered");
            Destroy(transform.parent.gameObject);
        }
    }
}
