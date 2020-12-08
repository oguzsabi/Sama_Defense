using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] private float secondsBetweenWaves = 5f;
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private float[] enemySpawnPeriods;
    [SerializeField] private int[] firstWaveEnemyCounts;
    [SerializeField] private int[] secondWaveEnemyCounts;
    [SerializeField] private int[] thirdWaveEnemyCounts;
    [SerializeField] private int[] fourthWaveEnemyCounts;
    [SerializeField] private GameObject[] paths;

    private Vector3 spawnerLocation;
    private int[][] waveEnemyCounts;
    private float nextSpawnTime;
    private int enemyPrefabsLength;
    private int waveCount; // #of waves in LVL1;
    private int currentWaveIndex;
    private bool inWaveBreak = false;
    
    // Start is called before the first frame update
    private void Start()
    {
        spawnerLocation = transform.position;
        enemyPrefabsLength = enemyPrefabs.Length;
        waveCount = 4;
        currentWaveIndex = 0;
        waveEnemyCounts = new int[waveCount][];
        waveEnemyCounts[0] = firstWaveEnemyCounts;
        waveEnemyCounts[1] = secondWaveEnemyCounts;
        waveEnemyCounts[2] = thirdWaveEnemyCounts;
        waveEnemyCounts[3] = fourthWaveEnemyCounts;

        print("currently in wave " + currentWaveIndex);
    }

    // Update is called once per frame
    private void Update()
    {
        if (inWaveBreak) return;
        
        for (var enemyIndex = 0; enemyIndex < enemyPrefabsLength; enemyIndex++)
        {
            var enemy = enemyPrefabs[enemyIndex];

            if (!IsSpawnTime(enemyIndex) || IsReadyForNextWave() || !HasSpawnChance(enemyIndex)) continue;
            
            SpawnEnemy(enemy);
            DecreaseEnemyCount(enemyIndex);
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

    private void DecreaseEnemyCount(int enemyIndex)
    {
        waveEnemyCounts[currentWaveIndex][enemyIndex] -= 1;
    }

    private bool HasSpawnChance(int enemyIndex)
    {
        return waveEnemyCounts[currentWaveIndex][enemyIndex] > 0;
    }

    private void SpawnEnemy(GameObject enemy)
    {
        var newEnemy = Instantiate(enemy, spawnerLocation, Quaternion.identity);
        var enemyComponent = newEnemy.GetComponent<Enemy>();

        enemyComponent.GetComponent<PathFinder>().SetEnemyPaths(paths);
    }

    private bool IsReadyForNextWave()
    {
        if (AreAllWavesFinished() || !AreAllEnemiesSpawnedInCurrentWave())
        {
            return false;
        }

        StartCoroutine(TakeAWaveBreak());
        currentWaveIndex++;
        return true;
    }

    private IEnumerator TakeAWaveBreak()
    {
        inWaveBreak = true;
        yield return new WaitForSeconds(secondsBetweenWaves);
        print("currently in wave " + currentWaveIndex);
        inWaveBreak = false;
    }

    private bool AreAllEnemiesSpawnedInCurrentWave()
    {
        for (var enemyIndex = 0; enemyIndex < enemyPrefabsLength; enemyIndex++)
        {
            if (waveEnemyCounts[currentWaveIndex][enemyIndex] > 0)
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
