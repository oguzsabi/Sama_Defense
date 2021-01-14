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
    private bool _dotRemoved = false;
    private int _counter = 0;
    private float _currentMovementSpeed;
    
    // Start is called before the first frame update
    void Start()
    {
        _gameSession = GameObject.Find("GameSession").GetComponent<GameSession>();
        _currentMovementSpeed = movementSpeed;
    }

    public float GetMoveSpeed()
    {
        return _currentMovementSpeed;
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
            Debug.Log(("Before ApplyElementEffect "));
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
        Debug.Log(("In ApplyElementEffects "));
        switch (projectileType)
        {
            case Projectile.ElementType.Fire:
            {
                Debug.Log("Before Dot() ");
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
        if (!_alreadyDotted)
        {
            StartCoroutine(DotTick());
        }
    }

    private void Slow()
    {
        if (_alreadySlowed) return;
        
        var slowedMovementSpeed = movementSpeed - _slowAmount;
        if (slowedMovementSpeed < 0.1f)
            slowedMovementSpeed = 1f;
        _currentMovementSpeed = slowedMovementSpeed;
        StartCoroutine(SlowTick());

    }

    private void Stun()
    {
        if (_alreadyStunned) return;
        
        _currentMovementSpeed = 0;
        StartCoroutine(StunTick());
    }

    private void KnockBack()
    {
        
    }

    private IEnumerator DotTick()
    {
        // Debug.Log("HP before any ticks");
        while (_dotTicksElapsed <= _dotTickTime)
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
        RemoveDot();
    }

    private IEnumerator SlowTick()
    {
        // var slowedMovementSpeed = movementSpeed - _slowAmount;
        _slowRemoved = false;
        // Debug.Log("Slow before any ticks");
        while (_slowTickElapsed < _slowTickTime)
        {
            Debug.Log("Movement speed before slow " + movementSpeed);
            // movementSpeed = slowedMovementSpeed;
            Debug.Log("Movement speed after slow " + movementSpeed);
            _slowTickElapsed++;
            yield return new WaitForSeconds(1);
        }
        RemoveSlow();
    }

    private IEnumerator StunTick()
    {
        // _movementSpeedStorer = movementSpeed;
        while (_stunTickElapsed < _stunTickTime)
        {
            // movementSpeed = 0;
            _stunTickElapsed++;
            yield return new WaitForSeconds(1);
        }
        RemoveStun();
    }
    
    private void RemoveSlow()
    {
        if (_slowTickElapsed == _slowTickTime || !_slowRemoved)
        {
            _currentMovementSpeed = movementSpeed;
            _slowTickElapsed = 0;
            _slowRemoved = true;
            _alreadySlowed = false;
        }
    }

    private void RemoveStun()
    {
        if (_stunTickElapsed == _stunTickTime || !_stunRemoved)
        {
            _currentMovementSpeed = movementSpeed;
            _stunTickElapsed = 0;
            _stunRemoved = true;
            _alreadyStunned = false;
        }
    }

    private void RemoveDot()
    {
        if (_dotTicksElapsed == _dotTickTime || !_dotRemoved)
        {
            _dotTicksElapsed = 0;
            _dotRemoved = true;
        }
    }
    
    public ElementType Element => element;
}
