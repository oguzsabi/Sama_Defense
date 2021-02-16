using System;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class Projectile : MonoBehaviour
{
    public enum ElementType { Fire, Water, Earth, Wood }
    
    [SerializeField] private float projectileSpeed = 5f;
    [SerializeField] public int accuracy;
    [SerializeField] private ElementType element;
    [SerializeField] private TextMeshPro missText;
    
    private GameObject _target;
    private Vector3 _lastKnownTargetPosition;
    private bool _targetLost;
    private float _damage;
    private Camera _mainCamera;
    private const float FORCE_MULTIPLIER = 50f;
    private string _projectileType;

    private void Start()
    {
        GetAccuracyData();
        _mainCamera = Camera.main;
    }
    
    private void Update()
    {
        if (missText.gameObject.activeSelf)
        {
            missText.transform.rotation = Quaternion.LookRotation(missText.transform.position - _mainCamera.transform.position);
        }
        
        if (_target)
        {
            MoveToTarget();
        }
        else if (!_targetLost)
        {
            MoveToLastKnownPosition();
            _targetLost = true;
        }
    }

    private void GetAccuracyData()
    {
        switch (element)
        {
            case ElementType.Fire:
                accuracy = TowerDataManager.GetFireAccuracy();
                break;
            case ElementType.Water:
                accuracy = TowerDataManager.GetWaterAccuracy();
                break;
            case ElementType.Earth:
                accuracy = TowerDataManager.GetEarthAccuracy();
                break;
            case ElementType.Wood:
                accuracy = TowerDataManager.GetWoodAccuracy();
                break;
        }
    }

    /// <summary>
    /// Moves the projectile to target enemy unit
    /// </summary>
    private void MoveToTarget()
    {
        _lastKnownTargetPosition = _target.transform.position;
        var step = projectileSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, _lastKnownTargetPosition, step);
    }
    
    /// <summary>
    /// In the case of target enemy unit dies and projectile is in mid air projectiles directed to last know position of enemy unit
    /// </summary>
    private void MoveToLastKnownPosition()
    {
        var offset = _lastKnownTargetPosition - transform.position;
        GetComponent<Rigidbody>().AddForce(offset * FORCE_MULTIPLIER);
    }
    
    /// <summary>
    /// Checks if projectile will hit the target or not if it is not a hit then destroys the projectile
    /// </summary>
    /// <param name="enemy"></param>
    private void RollForHit(Enemy enemy)
    {
        var successfulHit = Random.Range(1, 100) <= accuracy;

        if (!successfulHit)
        {
            MissAndDestroyProjectile();
            return;
        }

        var actualDamage = CalculateActualDamage(enemy);
        enemy.GetHit(actualDamage, this.Element);
        missText.fontSize = 0;
        transform.localScale = new Vector3(4, 4, 4);
        Destroy(gameObject, 0.1f);
    }
    
    /// <summary>
    /// Disables the mesh renderer
    /// Destroys the game object
    /// </summary>
    private void MissAndDestroyProjectile()
    {
        var _projectile = gameObject;
        missText.gameObject.SetActive(true);
        _projectile.GetComponent<MeshRenderer>().enabled = false;
        Destroy(_projectile, 0.3f);
    }
    
    /// <summary>
    /// Calculates actual damage with respect to damage relation between element relations
    /// </summary>
    /// <param name="enemyComponent"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    private float CalculateActualDamage(Enemy enemyComponent)
    {
        var actualDamage = _damage;
        switch (enemyComponent.Element)
        {
            case Enemy.ElementType.Fire:
                if (element == ElementType.Wood) actualDamage *= 0.75f;
                else if (element == ElementType.Water) actualDamage *= 1.25f;
                break;
            case Enemy.ElementType.Water:
                if (element == ElementType.Fire) actualDamage *= 0.75f;
                else if (element == ElementType.Earth) actualDamage *= 1.25f;
                break;
            case Enemy.ElementType.Earth:
                if (element == ElementType.Water) actualDamage *= 0.75f;
                else if (element == ElementType.Wood) actualDamage *= 1.25f;
                break;
            case Enemy.ElementType.Wood:
                if (element == ElementType.Earth) actualDamage *= 0.75f;
                else if (element == ElementType.Fire) actualDamage *= 1.25f;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        return actualDamage;
    }
    
    /// <summary>
    /// Trigger event between projectile and enemy unit
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        var enemyComponent = other.gameObject.GetComponent<Enemy>();
        
        if (enemyComponent)
        {
            RollForHit(enemyComponent);
        }
        else
        {
            Destroy(gameObject, 0.5f);
        }
    }
    
    /// <summary>
    /// Sets a enemy unit for the projectile
    /// </summary>
    /// <param name="target"></param>
    public void SetProjectileTarget(GameObject target)
    {
        _target = target;
    }
    /// <summary>
    /// Sets a damage value to projectile 
    /// </summary>
    /// <param name="TowDamage"></param>
    public void SetDamage(float TowDamage)
    {
        _damage = TowDamage;
    }

    public ElementType Element => element;
}
