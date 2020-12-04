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
        Destroy(gameObject, 15f);
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
}
