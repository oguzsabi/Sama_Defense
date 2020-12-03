using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform spawnerLocation;
    [SerializeField] private GameObject enemy;
    [SerializeField] private GameObject[] paths;
    
    private float spawningPeriod = 3f;
    private float HP;
    private float movementSpeed;
    private float nextSpawnTime;
    
    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        if (Time.time >  nextSpawnTime)
        {
            nextSpawnTime = Time.time + spawningPeriod;
            StartCoroutine(SpawnEnemy());
        }
    }
    
    private IEnumerator SpawnEnemy()
    {
        var newEnemy = Instantiate(enemy, spawnerLocation.position, Quaternion.identity);
        var enemyComponent = newEnemy.GetComponent<Enemy>();

        enemyComponent.GetComponent<PathFinder>().SetEnemyPaths(paths);
        
        yield return new WaitForSeconds(spawningPeriod);
    }
    
}
