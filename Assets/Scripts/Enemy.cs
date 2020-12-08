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

    public Currency currencyScript;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 45f);
        currencyScript = GameObject.FindWithTag("GameController").GetComponent<Currency>();
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
            currencyScript.coin += worth;
            Destroy(gameObject);
            
        }
    }

    public ElementType Element => element;
}
