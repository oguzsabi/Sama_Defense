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
    [SerializeField] private int _dotTickTime;
    [SerializeField] private int _ticksElapsed;
    [SerializeField] private int _dotDamage;
    
    private GameSession _gameSession;
    private bool _diedBefore = false;
    private bool _alreadyDotted = false;
    private int _counter = 0;
    
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

    public void GetHit(float damage, string projectileType)
    {
        
        //print("Damage Dealt = " + damage);
        switch (projectileType)
        {
            case "fire":
            {
                health -= damage;
                DoT();
                _alreadyDotted = true;
                if (health <= 0)
                {
                    _gameSession.ChangeCoinAmountBy(worth);
                    Die();
                }
                return;
            }
            case "water":
            {
                Slow();
                health -= damage;
                if (health <= 0)
                {
                    _gameSession.ChangeCoinAmountBy(worth);
                    Die();
                }
                return;
            }
            case "earth":
            {
                Stun();
                health -= damage;
                if (health <= 0)
                {
                    _gameSession.ChangeCoinAmountBy(worth);
                    Die();
                }
                return;
            }
            case "wood":
            {
                KnockBack();
                health -= damage;
                if (health <= 0)
                {
                    _gameSession.ChangeCoinAmountBy(worth);
                    Die();
                }
                return;
            }
            default:
            {
                health -= damage;
                if (health <= 0)
                {
                    _gameSession.ChangeCoinAmountBy(worth);
                    Die();
                }
                return;
            }
            
        }
        
    }

    private void Die()
    {
        if (_diedBefore) return;
        _diedBefore = true;
        WaveSpawner.DecreaseAliveEnemyCount();
        _gameSession.IncrementDiamondAmount();
        Destroy(gameObject);
    }
    

    public void DoT()
    {   
        Debug.Log("Enemy: " + element);
        if(!_alreadyDotted) StartCoroutine(DotTick());
        
    }

    public void Slow()
    {
        
    }

    public void Stun()
    {
        
    }

    public void KnockBack()
    {
        
    }

    IEnumerator DotTick()
    {
        Debug.Log("HP before any ticks");
        while (_ticksElapsed < _dotTickTime)
        {
            
            Debug.Log("Tick number " + _counter + "HP(Before tick):" + health);
            health -= _dotDamage;
            _counter++;
            _ticksElapsed++;
            Debug.Log("Tick number " + _counter + "HP(After tick):" + health);
            
            
            if (health <= 0)
            {
                _gameSession.ChangeCoinAmountBy(worth);
                Die();
                break;
            }

            yield return new WaitForSeconds(1);
        }    
    }
    public ElementType Element => element;
}
