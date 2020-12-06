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
    [Header("Wave 1 Enemy Count")]
    [Header("Wave Information")]
    [SerializeField] private int[] firstWaveEnemyCounts;
    [Header("Wave 2 Enemy Count")]
    [SerializeField] private int[] secondWaveEnemyCounts;
    [Header("Wave 3 Enemy Count")]
    [SerializeField] private int[] thirdWaveEnemyCounts;
    [Header("Wave 4 Enemy Count")]
    [SerializeField] private int[] fourthWaveEnemyCounts;
    
    
    private int[,] waveArray;
    private int[][] waveEnemyCounts;
    private float nextSpawnTime;
    private int enemyPrefabsLength;
    private int waveCount; // #of waves in LVL1;
    private int currentWaveIndex;

    // Start is called before the first frame update
    private void Start()
    {
        enemyPrefabsLength = enemyPrefabs.Length;
        waveCount = 4;
        currentWaveIndex = 0;
        waveArray = new int[waveCount, enemyPrefabsLength];
        waveEnemyCounts = new int[waveCount][];
        waveEnemyCounts[0] = firstWaveEnemyCounts;
        waveEnemyCounts[1] = secondWaveEnemyCounts;
        waveEnemyCounts[2] = thirdWaveEnemyCounts;
        waveEnemyCounts[3] = fourthWaveEnemyCounts;
        
        ArrangeWaveEnemyCounts();

        // waveArray[waveIndex, 0] = smallEnemy; // Small enemy amount in a single wave at LVL1
        // waveArray[waveIndex, 1] = normalEnemy; // Normal enemy amount in single wave at LVL1
        // waveArray[waveIndex, 2] = bigEnemy; // Big enemy amount in single wave at LVL1
        
        print("currently in wave " + currentWaveIndex);
    }

    // Update is called once per frame
    private void Update()
    {
        for (var enemyIndex = 0; enemyIndex < enemyPrefabsLength; enemyIndex++)
        {
            var enemy = enemyPrefabs[enemyIndex];

            if (!IsSpawnTime(enemyIndex) || IsReadyForNextWave() || !HasSpawnChance(enemyIndex)) continue;
            
            SpawnEnemy(enemy);
            waveArray[currentWaveIndex, enemyIndex] -= 1;
            // print("Enemy Türü: " + index + " Kalan Enemy Sayısı " + waveArray[0, index]);
        }
    }

    private bool IsSpawnTime(int enemyIndex)
    {
        var meanSpawnDelay = enemySpawnPeriods[enemyIndex];
        var spawnsPerSecond = 1 / meanSpawnDelay;
        var threshold = spawnsPerSecond * Time.deltaTime;

        return Random.value < threshold;
    }

    private bool HasSpawnChance(int enemyIndex)
    {
        return waveArray[currentWaveIndex, enemyIndex] > 0;
    }

    private void SpawnEnemy(GameObject enemy)
    {
        var newEnemy = Instantiate(enemy, spawnerLocation.position, Quaternion.identity);
        var enemyComponent = newEnemy.GetComponent<Enemy>();

        enemyComponent.GetComponent<PathFinder>().SetEnemyPaths(paths);
    }

    private void ArrangeWaveEnemyCounts()
    {
        for (var waveNumber = 0; waveNumber < waveCount; waveNumber++)
        {
            for (var enemyIndex = 0; enemyIndex < enemyPrefabsLength; enemyIndex++)
            {
                waveArray[waveNumber, enemyIndex] = waveEnemyCounts[waveNumber][enemyIndex];
            }
        }
    }

    private bool IsReadyForNextWave()
    {
        if (AreAllWavesFinished() || !AreAllEnemiesSpawnedInCurrentWave())
        {
            return false;
        }
        
        currentWaveIndex++;
        print("currently in wave " + currentWaveIndex);
        return true;
    }

    private bool AreAllEnemiesSpawnedInCurrentWave()
    {
        for (var enemyIndex = 0; enemyIndex < enemyPrefabsLength; enemyIndex++)
        {
            if (waveArray[currentWaveIndex, enemyIndex] > 0)
            {
                return false;
            }
        }

        return true;
    }

    private bool AreAllWavesFinished()
    {
        return currentWaveIndex >= waveCount - 1;
    }
}
