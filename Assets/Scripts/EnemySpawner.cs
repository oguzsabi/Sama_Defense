using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawner Information")]
    [SerializeField] private Transform spawnerLocation;
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private float[] enemySpawnPeriods;
    [SerializeField] private GameObject[] paths;
    [Header("Wave Information")]
    [SerializeField] private int smallEnemy;
    [SerializeField] private int normalEnemy;
    [SerializeField] private int bigEnemy;
    
    private readonly int[,] waveArray = new int[4,3];
    private float nextSpawnTime;
    private int enemyPrefabsLength;
    private int waveCount = 4; // #of waves in LVL1;
    private int waveIndex;
    
    // Start is called before the first frame update
    private void Start()
    {
        waveIndex = 0;
        waveArray[waveIndex, 0] = smallEnemy; // Small enemy amount in a single wave at LVL1
        waveArray[waveIndex, 1] = normalEnemy; // Normal enemy amount in single wave at LVL1
        waveArray[waveIndex, 2] = bigEnemy; // Big enemy amount in single wave at LVL1

        enemyPrefabsLength = enemyPrefabs.Length;
        print("currently in wave " + waveIndex);
    }

    // Update is called once per frame
    private void Update()
    {
        for (var index = 0; index < enemyPrefabsLength; index++)
        {
            var enemy = enemyPrefabs[index];
            if (waveIndex < waveCount - 1 && waveArray[waveIndex, 0] == 0 && waveArray[waveIndex, 1] == 0 && waveArray[waveIndex, 2] == 0)
            {
                waveIndex++;
                waveArray[waveIndex, 0] = smallEnemy;
                waveArray[waveIndex, 1] = normalEnemy;
                waveArray[waveIndex, 2] = bigEnemy;
                print("currently in wave " + waveIndex);
            }
            else
            {
                if (IsSpawnTime(index) && waveArray[waveIndex, index] > 0)
                {
                    SpawnEnemy(enemy);
                    waveArray[waveIndex, index] -= 1;
                    // print("Enemy Türü: " + index + " Kalan Enemy Sayısı " + waveArray[0, index]);
                }
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
