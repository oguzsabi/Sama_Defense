using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Tower : MonoBehaviour
{
    [SerializeField] private float shootingPeriod = 3f;
    [SerializeField] private float projectileSpeed = 3f;
    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform projectileStart;


    private bool shootAvailable = true;
    private List<GameObject> targets = new List<GameObject>();
    private GameObject currentTarget;

    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        if (!shootAvailable) return;
        
        shootAvailable = false;
        if (currentTarget) StartCoroutine(Shoot());
        else shootAvailable = true;
    }

    private IEnumerator Shoot()
    {
        var newProjectile = Instantiate(projectile, projectileStart.position, Quaternion.identity);
        newProjectile.GetComponent<Projectile>().SetProjectileTarget(currentTarget);

        yield return new WaitForSeconds(shootingPeriod);
        
        shootAvailable = true;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Equals("Terrain")) return;

        var enemyInRange = other.gameObject;
        if (!currentTarget) currentTarget = enemyInRange;
        
        targets.Add(enemyInRange);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name.Equals("Terrain")) return;

        var enemyOutRange = other.gameObject;
        if (enemyOutRange == currentTarget) currentTarget = null;

        targets.Remove(enemyOutRange);

        if (targets.Count > 0)
        {
            FindRandomTarget();
        }
    }

    private void FindRandomTarget()
    {
        var randomTargetIndex = Random.Range(0, targets.Count - 1);

        try
        {
            currentTarget = targets[randomTargetIndex];
        }
        catch (Exception e)
        {
            if (targets.Count > 0) FindRandomTarget();
        }
    }
}
