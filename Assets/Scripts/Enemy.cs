using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float health = 100f;
    [SerializeField] private float moveSpeed = 10f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float GetMoveSpeed()
    {
        return moveSpeed;
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
