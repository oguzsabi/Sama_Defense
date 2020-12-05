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
    [SerializeField] private int smallEnemy;
    [SerializeField] private int normalEnemy;
    [SerializeField] private int bigEnemy;
    
    private int[,] waveArray = new int[1,3];
    private float nextSpawnTime;
    private int enemyPrefabsLength;
    private int waveCounter = 4; // #of waves in LVL1;


    // Start is called before the first frame update
    private void Start()
    {
        
        waveArray[0,0] = 5; // Small enemy amount in a single wave at LVL1
        waveArray[0,1] = 5; // Normal enemy amount in single wave at LVL1
        waveArray[0,2] = 5; // Big enemy amount in single wave at LVL1

        smallEnemy = waveArray[0,0];
        normalEnemy = waveArray[0,1];
        bigEnemy = waveArray[0,2];
      
        
        
        enemyPrefabsLength = enemyPrefabs.Length;
    }

    // Update is called once per frame
    private void Update()
    {
        for (var index = 0; index < enemyPrefabsLength; index++)
        {
            var enemy = enemyPrefabs[index];
            if (waveArray[0, 0] == 0 && waveArray[0, 1] == 0 && waveArray[0, 2] == 0)
            {
                waveCounter--;
                waveArray[0, 0] = 5;
                waveArray[0, 1] = 5;
                waveArray[0, 2] = 5;
            }
            else
            {
                if (IsSpawnTime(index) && waveArray[0,index] > 0)
                {
                    SpawnEnemy(enemy);
                    waveArray[0,index] -= 1;
                    print("Enemy Türü: " + index + " Kalan Enemy Sayısı " + waveArray[0,index]);
                
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
