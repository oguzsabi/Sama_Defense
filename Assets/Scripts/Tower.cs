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
    [SerializeField] private SphereCollider _towerRange;
    
    public float fireRate = 0.375f;
    public float damage = 25;
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
        // if (other.name.Contains("Terrain") || other.gameObject.layer == 2 || other.gameObject.layer == 11) return;
        // If other is not in the Enemy layer it returns
        if (other.gameObject.layer != 10) return;

        var enemyInRange = other.gameObject;
        // If there is no current target then pick one
        _targets.Add(enemyInRange);
        // print("target in: " + _targets.Count);
        if (!_currentTarget) FindRandomTarget();
    }
    
    /// <summary>
    /// Removes the enemy unit from _targets that lefts the range of tower 
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit(Collider other)
    {
        // If other is not in the Enemy layer it returns
        if (other.gameObject.layer != 10) return;
        
        var enemyOutRange = other.gameObject;
        if (enemyOutRange == _currentTarget) _currentTarget = null;

        _targets.Remove(enemyOutRange);
        if (!_currentTarget) FindRandomTarget();
    }
    /// <summary>
    /// Randomly picks a target that is in range of the tower
    /// </summary>
    private void FindRandomTarget()
    {
        if (_targets.Count <= 0) return;
        
        int randomTargetIndex;
        if (element == ElementType.Earth)
        {
            randomTargetIndex = 0;
        }
        else
        {
            randomTargetIndex = Random.Range(0, _targets.Count - 1);
        }

        try
        {
            _currentTarget = _targets[randomTargetIndex];
            if (_currentTarget) return;
            
            RemoveNullTargets();
            FindRandomTarget();
        }
        catch (Exception)
        {
            FindRandomTarget();
        }
    }

    private void RemoveNullTargets()
    {
        _targets.RemoveAll(target => target == null);
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
        GetTowerData();
        _isReady = true;
    }

    private void GetTowerData()
    {
        switch (element)
        {
            case ElementType.Fire:
                damage = TowerDataManager.GetFireDamage();
                _towerRange.radius = TowerDataManager.GetFireRange();
                fireRate = TowerDataManager.GetFireSpeed();
                
                // projectile accuracy is set in the projectile script for the consistency of the values
                // projectile.GetComponent<Projectile>().accuracy = TowerDataManager.GetFireAccuracy();
                // range visual is set in the tower placement controller script for accurate visual while placing a tower
                // rangeIndicator.GetComponent<Projector>().orthographicSize = TowerDataManager.GetFireRangeVisual();
                break;
            case ElementType.Water:
                damage = TowerDataManager.GetWaterDamage();
                _towerRange.radius = TowerDataManager.GetWaterRange();
                fireRate = TowerDataManager.GetWaterSpeed();
                
                // projectile accuracy is set in the projectile script for the consistency of the values
                // projectile.GetComponent<Projectile>().accuracy = TowerDataManager.GetWaterAccuracy();
                // range visual is set in the tower placement controller script for accurate visual while placing a tower
                // rangeIndicator.GetComponent<Projector>().orthographicSize = TowerDataManager.GetWaterRangeVisual();
                break;
            case ElementType.Earth:
                damage = TowerDataManager.GetEarthDamage();
                _towerRange.radius = TowerDataManager.GetEarthRange();
                fireRate = TowerDataManager.GetEarthSpeed();
                
                // projectile accuracy is set in the projectile script for the consistency of the values
                // projectile.GetComponent<Projectile>().accuracy = TowerDataManager.GetEarthAccuracy();
                // range visual is set in the tower placement controller script for accurate visual while placing a tower
                // rangeIndicator.GetComponent<Projector>().orthographicSize = TowerDataManager.GetEarthRangeVisual();
                break;
            case ElementType.Wood:
                damage = TowerDataManager.GetWoodDamage();
                _towerRange.radius = TowerDataManager.GetWoodRange();
                fireRate = TowerDataManager.GetWoodSpeed();
                
                // projectile accuracy is set in the projectile script for the consistency of the values
                // projectile.GetComponent<Projectile>().accuracy = TowerDataManager.GetWoodAccuracy();
                // range visual is set in the tower placement controller script for accurate visual while placing a tower
                // rangeIndicator.GetComponent<Projector>().orthographicSize = TowerDataManager.GetWoodRangeVisual();
                break;
        }
    }

    public ElementType Element => element;
}
