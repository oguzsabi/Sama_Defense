using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float projectileSpeed = 5f;
    [SerializeField] private int accuracy = 100;
    
    private GameObject _target;
    private Vector3 _lastKnownTargetPosition;
    private bool targetLost;
    private float damage;
    private const float forceMultiplier = 50f;

    // Update is called once per frame
    private void Update()
    {
        if (_target)
        {
            MoveToTarget();
        }
        else if (!targetLost)
        {
            MoveToLastKnownPosition();
            targetLost = true;
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

    private IEnumerator DisableColliderAndTerminate()
    {
        yield return new WaitForSeconds(2f);
        GameObject o;
        (o = gameObject).GetComponent<SphereCollider>().enabled = false;
        Destroy(o, 2f);
    }

    private void RollForHit(Enemy enemyComponent)
    {
        var successfulHit = Random.Range(0, 100) < accuracy;

        if (!successfulHit) return;
        
        enemyComponent.GetHit(damage);
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision other)
    {
        var enemyComponent = other.gameObject.GetComponent<Enemy>();
        
        if (enemyComponent)
        {
            RollForHit(enemyComponent);
        }
        else
        {
            StartCoroutine(DisableColliderAndTerminate());
        }
    }
    
    public void SetProjectileTarget(GameObject target)
    {
        _target = target;
    }

    public void SetDamage(float TowDamage)
    {
        damage = TowDamage;
    }
}
