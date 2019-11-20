using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour, IListener, IDeathNotifier
{
    public bool shouldActivateOnStart;
    public List<IListener> listeners;
    public GameObject[] enemyPrefabs;
    public int currIndex;

    public float alertDistanceReset;

    public void Awake()
    {
        listeners = new List<IListener>();
    }
    public void Start()
    {
        currIndex = 0;
        if(shouldActivateOnStart) {
            SpawnEnemy();
        }
    }
    public void SpawnEnemy()
    {
        if(currIndex < enemyPrefabs.Length) {
            GameObject enemySpawned = Instantiate(enemyPrefabs[currIndex]);
            enemySpawned.transform.position = transform.position;
            enemySpawned.GetComponent<IEnemyPursuit>().SetAlertDistance(alertDistanceReset);
            enemySpawned.GetComponent<EnemyCoreScript>().SubscribeListener(this);
            currIndex++;
        } else {
            Die();
        }
    }

    public void Notify()
    {
        SpawnEnemy();
    }

    public void SubscribeListener(IListener l)
    {
        listeners.Add(l);
    }

    public void Die()
    {
        foreach(IListener l in listeners) {
            l.Notify();
        }
        Destroy(gameObject);
    }

}
