using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int health = 100;
    [SerializeField] private GameSession _gameSession;
    
    private void DecreasePlayerHealth()
    {
        health -= 10;
        _gameSession.DecreasePlayerHealthBy(10);

        if (health <= 0)
        {
            EndGame();
        }
    }

    private void EndGame()
    {
        SceneLoader.LoadScene("Death");
    }

    private void OnTriggerEnter(Collider other)
    {
        var enemyComponent = other.GetComponent<Enemy>();

        if (!enemyComponent) return;
        
        Destroy(other.gameObject);
        WaveSpawner.DecreaseAliveEnemyCount();
        DecreasePlayerHealth();
    }
}
