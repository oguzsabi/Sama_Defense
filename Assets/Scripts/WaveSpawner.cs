using System.Collections;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] private float secondsBetweenWaves = 5f;
    [SerializeField] private float secondsBetweenSpawns = 0.5f;
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private float[] enemySpawnPeriods;
    [SerializeField] private int[] firstWaveEnemyCounts;
    [SerializeField] private int[] secondWaveEnemyCounts;
    [SerializeField] private int[] thirdWaveEnemyCounts;
    [SerializeField] private int[] fourthWaveEnemyCounts;
    [SerializeField] private GameObject[] paths;

    private Vector3 _spawnerLocation;
    private int[][] _waveEnemyCounts;
    private float _nextSpawnTime;
    private int _enemyPrefabsLength;
    private int _waveCount;
    private int _currentWaveIndex;
    private bool _inWaveBreak = false;
    private bool _inSpawnBreak = false;
    private bool _loadingNextLevel = false;
    private static int _enemiesAlive = 0;
    private GameSession _gameSession;
    
    private void Start()
    {
        _enemiesAlive = 0;
        _spawnerLocation = transform.position;
        _enemyPrefabsLength = enemyPrefabs.Length;
        _waveCount = 4;
        _currentWaveIndex = 0;
        _waveEnemyCounts = new int[_waveCount][];
        _waveEnemyCounts[0] = firstWaveEnemyCounts;
        _waveEnemyCounts[1] = secondWaveEnemyCounts;
        _waveEnemyCounts[2] = thirdWaveEnemyCounts;
        _waveEnemyCounts[3] = fourthWaveEnemyCounts;
        _gameSession = GameObject.Find("GameSession").GetComponent<GameSession>();
    }
    
    private void Update()
    {
        if (_inWaveBreak || _inSpawnBreak) return;

        if (IsTheLevelOver() && !_loadingNextLevel)
        {
            _loadingNextLevel = true;
            _gameSession.LoadNextLevel();
        }
        
        for (var enemyIndex = 0; enemyIndex < _enemyPrefabsLength; enemyIndex++)
        {
            var enemy = enemyPrefabs[enemyIndex];

            if (!IsSpawnTime(enemyIndex) || IsReadyForNextWave() || !HasSpawnChance(enemyIndex)) continue;
            
            SpawnEnemy(enemy);
            DecreaseEnemyCount(enemyIndex);
        }
    }
    /// <summary>
    /// Checks if next spawn time is reached or not
    /// </summary>
    /// <param name="enemyIndex"></param>
    /// <returns></returns>
    private bool IsSpawnTime(int enemyIndex)
    {
        var meanSpawnDelay = enemySpawnPeriods[enemyIndex];
        var spawnsPerSecond = 1 / meanSpawnDelay;
        var threshold = spawnsPerSecond * Time.deltaTime;

        return Random.value < threshold;
    }
    /// <summary>
    /// Decreases the enemy unit count
    /// </summary>
    /// <param name="enemyIndex"></param>
    private void DecreaseEnemyCount(int enemyIndex)
    {
        _waveEnemyCounts[_currentWaveIndex][enemyIndex] -= 1;
    }
    /// <summary>
    /// Checks if an enemy unit has a spawn chance
    /// </summary>
    /// <param name="enemyIndex"></param>
    /// <returns></returns>
    private bool HasSpawnChance(int enemyIndex)
    {
        return _waveEnemyCounts[_currentWaveIndex][enemyIndex] > 0;
    }
    /// <summary>
    /// Spawns an enemy unit
    /// Increases the number of enemy units
    /// </summary>
    /// <param name="enemy"></param>
    private void SpawnEnemy(GameObject enemy)
    {
        StartCoroutine(TakeASpawnBreak());
        
        var newEnemy = Instantiate(enemy, _spawnerLocation, Quaternion.identity);
        var enemyComponent = newEnemy.GetComponent<Enemy>();
        _enemiesAlive++;

        enemyComponent.GetComponent<PathFinder>().SetEnemyPaths(paths);
    }
    /// <summary>
    /// Checks if it is okay to start next wave
    /// </summary>
    /// <returns></returns>
    private bool IsReadyForNextWave()
    {
        if (AreAllWavesFinished() || !AreAllEnemiesSpawnedInCurrentWave())
        {
            return false;
        }

        StartCoroutine(TakeAWaveBreak());
        _currentWaveIndex++;
        return true;
    }
    /// <summary>
    /// Gives a break between waves
    /// </summary>
    /// <returns></returns>
    private IEnumerator TakeAWaveBreak()
    {
        _inWaveBreak = true;
        yield return new WaitForSeconds(secondsBetweenWaves);
        _gameSession.IncrementWaveNumber();
        _inWaveBreak = false;
    }
    /// <summary>
    /// Gives a break between spawns
    /// </summary>
    /// <returns></returns>
    private IEnumerator TakeASpawnBreak()
    {
        _inSpawnBreak = true;
        yield return new WaitForSeconds(secondsBetweenSpawns);
        _inSpawnBreak = false;
    }
    /// <summary>
    /// Checks if all enemies in a wave is spawned
    /// </summary>
    /// <returns></returns>
    private bool AreAllEnemiesSpawnedInCurrentWave()
    {
        for (var enemyIndex = 0; enemyIndex < _enemyPrefabsLength; enemyIndex++)
        {
            if (_waveEnemyCounts[_currentWaveIndex][enemyIndex] > 0)
            {
                return false;
            }
        }
        return true;
    }
    /// <summary>
    /// Checks if all waves are done
    /// </summary>
    /// <returns></returns>
    private bool AreAllWavesFinished()
    {
        return _currentWaveIndex >= _waveCount - 1;
    }
    /// <summary>
    /// Checks if level is over
    /// </summary>
    /// <returns></returns>
    private bool IsTheLevelOver()
    {
        // If not in the last wave don't check for level over.
        if (_currentWaveIndex != _waveCount - 1) return false;
        
        // This checks whether there are still enemies to be spawned.
        if (_waveEnemyCounts[_waveCount - 1].Any(enemyCount => enemyCount > 0))
        {
            return false;
        }
        
        // If all enemies are spawned and all of them are dead, it is time to load the new level.
        return _enemiesAlive < 1;
    }
    /// <summary>
    /// Decreases the number of enemies that are alive
    /// </summary>
    public static void DecreaseAliveEnemyCount()
    {
        _enemiesAlive--;
    }
}
