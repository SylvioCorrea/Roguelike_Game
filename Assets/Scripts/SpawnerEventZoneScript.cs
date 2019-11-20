using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerEventZoneScript : MonoBehaviour, IListener, IDeathNotifier
{
    public List<IListener> listeners;
    public Transform[] spawners;
    public bool eventTriggered;

    public int spawnersDestroyed;
    // Start is called before the first frame update
    
    void Awake()
    {
        listeners = new List<IListener>();
        //Subscribe this event zone as listener and then deactivate the spawners.
        //The spawners should activate once the player steps in this event zone.
        
    }


    void Start()
    {
        foreach(Transform spawner in spawners) {
            spawner.GetComponent<IDeathNotifier>().SubscribeListener(this);
        }
    }

    public void Notify()
    {
        spawnersDestroyed++;
        if(spawnersDestroyed >= spawners.Length) { //Die when all spawners have been destroyed
            Die();
        }
    }

    public void Die()
    {
        foreach(IListener l in listeners) {
            l.Notify();
        }
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if(!eventTriggered && col.CompareTag("Player")) { //Player stepped on the event zone.
            eventTriggered = true;
            foreach(Transform spawner in spawners) { //Activate all spawners.
                spawner.GetComponent<SpawnerScript>().SpawnEnemy();
            }
        }
    }

    public void SubscribeListener(IListener l)
    {
        listeners.Add(l);
    }
}
