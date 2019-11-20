using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour, IListener
{
    
    public GameObject[] enemyPrefabs;
    public int currIndex;

    public float alertDistanceReset;

    public void Start()
    {
        currIndex = 0;
        SpawnEnemy();
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
            Destroy(gameObject);
        }
    }

    public void Notify()
    {
        SpawnEnemy();
    }

}
