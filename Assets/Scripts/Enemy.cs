using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float health = 100f;
    [SerializeField] private float movementSpeed = 10f;
    
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        //health = 100f;
        //movementSpeed = 10f;
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
        health -= damage;
        
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void AddToTowers(GameObject tower)
    {
        
    }
    
   
    public void SetEnemyHp(float hp)
    {
        health = hp;
    }

    public void SetEnemyMovementSpeed(float speed)
    {
        movementSpeed = speed;
    }
    
    
}
