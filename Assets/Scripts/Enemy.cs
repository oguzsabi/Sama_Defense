using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum ElementType { Fire, Water, Earth, Wood }
    
    [SerializeField] private float health = 100f;
    [SerializeField] private float movementSpeed = 10f;
    [SerializeField] private ElementType element;
    [SerializeField] private int worth;

    private GameSession _gameSession;
    private bool diedBefore = false;
    // Start is called before the first frame update
    void Start()
    {
        _gameSession = GameObject.Find("GameSession").GetComponent<GameSession>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float GetMoveSpeed()
    {
        return movementSpeed;
    }

    public void GetHit(float damage)
    {
        //print("Damage Dealt = " + damage);
        health -= damage;
        
        if (health <= 0)
        {
            _gameSession.ChangeCoinAmountBy(worth);
            Die();
        }
    }

    private void Die()
    {
        if (diedBefore) return;
        diedBefore = true;
        WaveSpawner.DecreaseAliveEnemyCount();
        _gameSession.IncrementDiamondAmount();
        Destroy(gameObject);
    }

    public ElementType Element => element;
}
