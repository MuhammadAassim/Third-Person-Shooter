using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Prefab")]
    [SerializeField] private GameObject enemyPrefab;

    [Header("SpawnSettings")]
    [SerializeField] private float minSpawnTime;
    [SerializeField] private float maxSpawnTime;

    [Header("PoolSettings")]
    [SerializeField] private int poolSize;

    private List <GameObject> enemyPool;
    private float spawnDelay;

    private void Start()
    {
        
        enemyPool = new List<GameObject>();

        for(int i = 0; i< poolSize; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab);
            enemy.SetActive(false);
            enemyPool.Add(enemy);
        }

        ResetSpawnTimer();
    }

    private void Update()
    {
        spawnDelay -= Time.deltaTime;
        if(spawnDelay <= 0f)
        {
            SpawnEnemies();
            ResetSpawnTimer();
        }
    }


    private void SpawnEnemies()
    {
        foreach(GameObject enemy in enemyPool)
        {
            if (!enemy.activeInHierarchy)
            {
                enemy.transform.position = transform.position;
                enemy.SetActive (true);
                return;
            }
        } 
    }

    private void ResetSpawnTimer()
    {
        spawnDelay = Random.Range(minSpawnTime, maxSpawnTime);
    }
}
