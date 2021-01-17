using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int health = 10;
    
    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        
    }
    
    
    private void DecreasePlayerHealth()
    {
        health--;

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

        if (enemyComponent)
        {
            Destroy(other.gameObject);
            DecreasePlayerHealth();
        }
    }
}
