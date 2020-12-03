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
    private float damage;

    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        if (!_target) return;
        
        MoveToTarget();
    }

    private void MoveToTarget()
    {
        var step = projectileSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, _target.transform.position, step);
    }

    public void SetProjectileTarget(GameObject target)
    {
        _target = target;
    }

    private void OnCollisionEnter(Collision other)
    {
        var hit = Random.Range(0, 100);
        
        if (hit < accuracy)
        {
            other.gameObject.GetComponent<Enemy>().GetHit(damage);
        }

        Destroy(gameObject);
    }

    public void SetDamage(float TowDamage)
    {
        damage = TowDamage;
    }
}
