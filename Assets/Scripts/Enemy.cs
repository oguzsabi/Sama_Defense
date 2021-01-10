using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum ElementType { Fire, Water, Earth, Wood }
    
    [SerializeField] private float health;
    [SerializeField] private float movementSpeed;
    [SerializeField] private ElementType element;
    [SerializeField] private int worth;
    [SerializeField] private int _dotTickTime;
    [SerializeField] private int _dotTicksElapsed;
    [SerializeField] private int _dotDamage;
    [SerializeField] private int _slowTickTime;
    [SerializeField] private int _slowTickElapsed;
    [SerializeField] private int _slowAmount;
    [SerializeField] private int _stunTickTime;
    [SerializeField] private int _stunTickElapsed;
    
    private GameSession _gameSession;
    private bool _diedBefore = false;
    private bool _alreadyDotted = false;
    private bool _alreadySlowed = false;
    private bool _alreadyStunned = false;
    private bool _slowRemoved = false;
    private bool _stunRemoved = false;
    private int _counter = 0;
    private float _movementSpeedStorer;
    
    // Start is called before the first frame update
    void Start()
    {
        _gameSession = GameObject.Find("GameSession").GetComponent<GameSession>();
    }

    public float GetMoveSpeed()
    {
        return movementSpeed;
    }

    public void GetHit(float damage, Projectile.ElementType projectileType)
    {
        health -= damage;
        if (health <= 0)
        {
            _gameSession.ChangeCoinAmountBy(worth);
            Die();
        }
        else
        {
            ApplyElementEffect(projectileType);
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

    private void ApplyElementEffect(Projectile.ElementType projectileType)
    {
        switch (projectileType)
        {
            case Projectile.ElementType.Fire:
            {
                DoT();
                _alreadyDotted = true;
                return;
            }
            case Projectile.ElementType.Water:
            {
                Slow();
                _alreadySlowed = true;
                return;
            }
            case Projectile.ElementType.Earth:
            {
                Stun();
                _alreadyStunned = true;
                return;
            }
            case Projectile.ElementType.Wood:
            {
                KnockBack();
                return;
            }
            default:
                throw new ArgumentOutOfRangeException(nameof(projectileType), projectileType, "Wrong Element Type");
        }
    }
    
    private void DoT()
    {   
        // Debug.Log("Enemy: " + element);
        if(!_alreadyDotted) StartCoroutine(DotTick());
    }

    private void Slow()
    {
        if (!_alreadySlowed) StartCoroutine(SlowTick());
        RemoveSlow();
    }

    private void Stun()
    {
        if (!_alreadyStunned) StartCoroutine(StunTick());
        RemoveStun();
    }

    private void KnockBack()
    {
        
    }

    private IEnumerator DotTick()
    {
        // Debug.Log("HP before any ticks");
        while (_dotTicksElapsed < _dotTickTime)
        {
            // Debug.Log("Tick number " + _counter + "HP(Before tick):" + health);
            health -= _dotDamage;
            _counter++;
            _dotTicksElapsed++;
            // Debug.Log("Tick number " + _counter + "HP(After tick):" + health);

            if (health <= 0)
            {
                _gameSession.ChangeCoinAmountBy(worth);
                Die();
                break;
            }

            yield return new WaitForSeconds(1);
        }    
    }

    private IEnumerator SlowTick()
    {
        var slowedMovementSpeed = movementSpeed - _slowAmount;
        _slowRemoved = false;
        // Debug.Log("Slow before any ticks");
        while (_slowTickElapsed < _slowTickTime)
        {
            Debug.Log("Movement speed before slow " + movementSpeed);
            movementSpeed = slowedMovementSpeed;
            Debug.Log("Movement speed after slow " + movementSpeed);
            _slowTickElapsed++;
            yield return new WaitForSeconds(1);
        }
    }

    private IEnumerator StunTick()
    {
        _movementSpeedStorer = movementSpeed;
        while (_stunTickElapsed < _stunTickTime)
        {
            movementSpeed = 0;
            _stunTickElapsed++;
            yield return new WaitForSeconds(1);
        }
    }
    
    private void RemoveSlow()
    {
        if (_slowTickElapsed == _slowTickTime || !_slowRemoved)
        {
            movementSpeed += _slowAmount;
            _slowRemoved = true;
            _alreadySlowed = false;
        }
    }

    private void RemoveStun()
    {
        if (_stunTickElapsed == _stunTickTime || !_stunRemoved)
        {
            movementSpeed += _movementSpeedStorer;
            _stunRemoved = true;
            _alreadyStunned = false;
        }
    }
    
    public ElementType Element => element;
}
