using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityStandardAssets.Effects;
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
    private const float forceMultiplier = 50f;
    private string _projectileType;
    

    private void Start()
    {
        _mainCamera = Camera.main;
    }

    // Update is called once per frame
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

    private void MoveToTarget()
    {
        _lastKnownTargetPosition = _target.transform.position;
        var step = projectileSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, _lastKnownTargetPosition, step);
    }

    private void MoveToLastKnownPosition()
    {
        var offset = _lastKnownTargetPosition - transform.position;
        GetComponent<Rigidbody>().AddForce(offset * forceMultiplier);
    }
    
    private void RollForHit(Enemy enemyComponent)
    {
        var successfulHit = Random.Range(0, 100) < accuracy;

        if (!successfulHit)
        {
            MissAndDestroyProjectile();
            return;
        }

        var actualDamage = CalculateActualDamage(enemyComponent);
        enemyComponent.GetHit(actualDamage, this.Element);
        Destroy(gameObject);
    }

    private void MissAndDestroyProjectile()
    {
        var _projectile = gameObject;
        missText.gameObject.SetActive(true);
        _projectile.GetComponent<MeshRenderer>().enabled = false;
        Destroy(_projectile, 0.3f);
    }

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
    
    public void SetProjectileTarget(GameObject target)
    {
        _target = target;
    }

    public void SetDamage(float TowDamage)
    {
        _damage = TowDamage;
    }

    public ElementType Element => element;
}
