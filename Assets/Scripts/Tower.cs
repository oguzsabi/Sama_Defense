using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Tower : MonoBehaviour
{
    public enum ElementType { Fire, Water, Earth, Wood }
    
    [SerializeField] private ElementType element;
    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform projectileStart;
    
    public float fireRate = 0.35f;
    public float damage = 50;
    public bool isPlaceable;
    public GameObject adjacencyDetector;
    public GameObject rangeIndicator;

    private bool _shootAvailable = true;
    private readonly List<GameObject> _targets = new List<GameObject>();
    private GameObject _currentTarget;
    private bool _isReady;

    // default rangeIndicator size = 15.8, default range radius = 2.15
    
    private void Start()
    {
        _isReady = false;
        isPlaceable = true;
    }

    
    private void Update()
    {
        if (!_shootAvailable || !_isReady) return;
        
        _shootAvailable = false;
        if (_currentTarget)
        {
            StartCoroutine(Shoot());
        }
        else
        {
            _shootAvailable = true;
            FindRandomTarget();
        }
    }
    /// <summary>
    /// Creates a new instance of a projectile and sets projectile's values
    /// </summary>
    /// <returns></returns>
    private IEnumerator Shoot()
    {
        var newProjectile = Instantiate(projectile, projectileStart.position, Quaternion.identity);
        var projectileComponent = newProjectile.GetComponent<Projectile>();

        projectileComponent.SetProjectileTarget(_currentTarget);
        projectileComponent.SetDamage(damage);

        yield return new WaitForSeconds(1/fireRate);
        
        _shootAvailable = true;
    }
    
    /// <summary>
    /// Adds enemy units to _target array which enters the range of tower
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        // If it is not terrain or IgnoreRaycast or Placement then it is an enemy unit
        if (other.name.Equals("Terrain") || other.gameObject.layer == 2 || other.gameObject.layer == 11) return;

        var enemyInRange = other.gameObject;
        // If there is no current target then pick one
        if (!_currentTarget) _currentTarget = enemyInRange;
        _targets.Add(enemyInRange);
    }
    
    /// <summary>
    /// Removes the enemy unit from _targets that lefts the range of tower 
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit(Collider other)
    {
        // If it is not terrain or IgnoreRaycast or Placement then it is an enemy unit
        if (other.name.Equals("Terrain") || other.gameObject.layer == 2 || other.gameObject.layer == 11) return;

        var enemyOutRange = other.gameObject;
        if (enemyOutRange == _currentTarget) _currentTarget = null;

        _targets.Remove(enemyOutRange);
        FindRandomTarget();
    }
    /// <summary>
    /// Randomly picks a target that is in range of the tower
    /// </summary>
    private void FindRandomTarget()
    {
        if (_targets.Count <= 0) return;
        
        var randomTargetIndex = Random.Range(0, _targets.Count - 1);

        try
        {
            _currentTarget = _targets[randomTargetIndex];
            if (_currentTarget) return;
            
            _targets.RemoveAll(target => target == null);
            FindRandomTarget();
        }
        catch (Exception)
        {
            FindRandomTarget();
        }
    }
    
    /// <summary>
    /// Activates green range indicator on mouse enter
    /// </summary>
    private void OnMouseEnter()
    {
        rangeIndicator.SetActive(true);
    }
    
    /// <summary>
    /// Deactivates green range indicator on mouse enter
    /// </summary>
    private void OnMouseExit()
    {
        rangeIndicator.SetActive(false);
    }
    
    /// <summary>
    /// Makes tower ready for shoot
    /// </summary>
    public void MakeTowerReady()
    {
        _isReady = true;
    }
    
    public ElementType Element => element;
}
