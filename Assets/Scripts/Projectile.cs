using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float projectileSpeed = 5f;

    private GameObject _target;
    
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
        Destroy(gameObject);
    }
}
