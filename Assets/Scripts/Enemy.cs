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
    private Rigidbody _rigidbody;
    private bool _diedBefore = false;
    private bool _alreadyDotted = false;
    private bool _alreadySlowed = false;
    private bool _alreadyStunned = false;
    private bool _alreadyKnockedBack = false;
    private bool _slowRemoved = false;
    private bool _stunRemoved = false;
    private bool _dotRemoved = false;
    private float _currentMovementSpeed;


    void Start()
    {
        _gameSession = GameObject.Find("GameSession").GetComponent<GameSession>();
        _currentMovementSpeed = movementSpeed;
        _rigidbody = GetComponent<Rigidbody>();
    }
    /// <summary>
    /// Gets the movement speed of enemy unit
    /// </summary>
    /// <returns>float currentMovementSpeed</returns>
    public float GetMoveSpeed()
    {
        return _currentMovementSpeed;
    }
    
    /// <summary>
    /// Deals damage to enemy unit and either kills unit or apply elemental effects to unit
    /// </summary>
    /// <param name="damage"></param>
    /// <param name="projectileType"></param>
    public void GetHit(float damage, Projectile.ElementType projectileType)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
        else
        {
            Debug.Log(("Before ApplyElementEffect "));
            ApplyElementEffect(projectileType);
        }
    }
    
    /// <summary>
    /// Destroys enemy unit
    /// Increases currencies and decreases alive enemy count
    /// </summary>
    private void Die()
    {
        if (_diedBefore) return;
        
        _diedBefore = true;
        WaveSpawner.DecreaseAliveEnemyCount();
        _gameSession.ChangeCoinAmountBy(worth);
        _gameSession.IncrementDiamondAmount();
        Destroy(gameObject);
    }
    
    /// <summary>
    /// Applies elemental effects to enemy unit
    /// Fire: Deals damage over time
    /// Water: Slows enemy units
    /// Earth: Stuns enemy units
    /// Wood: Knocks enemy units back
    /// </summary>
    /// <param name="projectileType"></param>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
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
                _alreadyKnockedBack = true;
                return;
            }
            default:
                throw new ArgumentOutOfRangeException(nameof(projectileType), projectileType, "Wrong Element Type");
        }
    }
    
    /// <summary>
    /// Starts a coroutine for damage over time(DoT) effect on enemy unit
    /// </summary>
    private void DoT()
    {
        // Only apply DoT effect if unit does not have a DoT already
        if (_alreadyDotted) return;

        StartCoroutine(DotTick());

    }
    
    /// <summary>
    /// Starts a coroutine for slow effect on enemy unit and slows the enemy for _slowAmount
    /// </summary>
    private void Slow()
    {
        // Only apply slow effect if unit does not have a slow already
        if (_alreadySlowed) return;
        
        var slowedMovementSpeed = movementSpeed - _slowAmount;
        if (slowedMovementSpeed < 0.1f)
            slowedMovementSpeed = 1f;
        _currentMovementSpeed = slowedMovementSpeed;
        StartCoroutine(SlowTick());
    }
    
    /// <summary>
    /// Starts a coroutine for stun effect on enemy unit
    /// Reduces the movement speed to 0
    /// </summary>
    private void Stun()
    {
        // Only apply stun effect if unit does not stunned already
        if (_alreadyStunned) return;
        
        _currentMovementSpeed = 0;
        StartCoroutine(StunTick());
    }

    private void KnockBack()
    {
        if (_alreadyKnockedBack) return;
        
        _rigidbody.AddForce(0, 0, 800);
        StartCoroutine(KnockBackTick());
    }
    
    /// <summary>
    /// Loops for _dotTickTime times and deals _dotDamage in every iteration
    /// Removes the DoT effect once the loop is over
    /// </summary>
    /// <returns>IEnumerator WaitForSeconds</returns>
    private IEnumerator DotTick()
    {
        while (_dotTicksElapsed <= _dotTickTime)
        {
            health -= _dotDamage;
            _dotTicksElapsed++;
            
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
    
    /// <summary>
    /// Loops for _slowTickTime times
    /// Removes the slow effect once the loop is over
    /// </summary>
    /// <returns>IEnumerator WaitForSeconds</returns>
    private IEnumerator SlowTick()
    {
        _slowRemoved = false;
        while (_slowTickElapsed < _slowTickTime)
        {
            _slowTickElapsed++;
            yield return new WaitForSeconds(1);
        }
        RemoveSlow();
    }
    
    /// <summary>
    /// Loops for _stunTickTime times
    /// Removes the stun effect once the loop is over
    /// </summary>
    /// <returns>IEnumerator WaitForSeconds</returns>
    private IEnumerator StunTick()
    {
        while (_stunTickElapsed < _stunTickTime)
        {
            _stunTickElapsed++;
            yield return new WaitForSeconds(1);
        }
        RemoveStun();
    }
    
    private IEnumerator KnockBackTick()
    {
        yield return new WaitForSeconds(3f);
        _alreadyKnockedBack = false;
    }
    
    /// <summary>
    /// Resets _dotTickElapsed for future DoT effects
    /// </summary>
    private void RemoveDot()
    {
        if (_dotTicksElapsed == _dotTickTime || !_dotRemoved)
        {
            _dotTicksElapsed = 0;
            _dotRemoved = true;
        }
    }
    
    /// <summary>
    /// Returns movement speed to its normal value
    /// Resets _slowTickElapsed for future slow effects
    /// </summary>
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
    
    /// <summary>
    /// Returns movement speed to its normal value
    /// Resets _stunTickElapsed for future stun effects
    /// </summary>
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

    public ElementType Element => element;
}
