using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform spawnerLocation;
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private float[] enemySpawnPeriods;
    // [SerializeField] private GameObject enemy;
    [SerializeField] private GameObject[] paths;

    private float nextSpawnTime;
    private int enemyPrefabsLength;
    
    // Start is called before the first frame update
    private void Start()
    {
        enemyPrefabsLength = enemyPrefabs.Length;
    }

    // Update is called once per frame
    private void Update()
    {
        for (var index = 0; index < enemyPrefabsLength; index++)
        {
            var enemy = enemyPrefabs[index];
            if (IsSpawnTime(index))
            {
                SpawnEnemy(enemy);
            }
        }
    }

    private bool IsSpawnTime(int index)
    {
        var meanSpawnDelay = enemySpawnPeriods[index];
        var spawnsPerSecond = 1 / meanSpawnDelay;
        var threshold = spawnsPerSecond * Time.deltaTime / 5;

        return Random.value < threshold;
    }

    private void SpawnEnemy(GameObject enemy)
    {
        var newEnemy = Instantiate(enemy, spawnerLocation.position, Quaternion.identity);
        var enemyComponent = newEnemy.GetComponent<Enemy>();

        enemyComponent.GetComponent<PathFinder>().SetEnemyPaths(paths);
    }
}
