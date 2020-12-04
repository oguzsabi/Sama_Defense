using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Tower : MonoBehaviour
{
    [SerializeField] private float shootingPeriod = 3f;
    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform projectileStart;
    [SerializeField] private GameObject rangeIndicator;
    [SerializeField] private float damage = 50;
    
    private bool shootAvailable = true;
    private readonly List<GameObject> targets = new List<GameObject>();
    private GameObject currentTarget;
    private bool isReady;
    
    public bool isPlaceable;
    public GameObject adjacencyDetector;

    // Start is called before the first frame update
    private void Start()
    {
        isReady = false;
        isPlaceable = true;
    }

    // Update is called once per frame
    private void Update()
    {
        if (!shootAvailable || !isReady) return;
        
        shootAvailable = false;
        if (currentTarget)
        {
            StartCoroutine(Shoot());
        }
        else
        {
            shootAvailable = true;
            FindRandomTarget();
        }
    }

    private IEnumerator Shoot()
    {
        var newProjectile = Instantiate(projectile, projectileStart.position, Quaternion.identity);
        var projectileComponent = newProjectile.GetComponent<Projectile>();

        projectileComponent.SetProjectileTarget(currentTarget);
        projectileComponent.SetDamage(damage);

        yield return new WaitForSeconds(shootingPeriod);
        
        shootAvailable = true;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Equals("Terrain") || other.gameObject.layer == 2 || other.gameObject.layer == 11) return;

        var enemyInRange = other.gameObject;
        // If there is no current target then pick one
        if (!currentTarget) currentTarget = enemyInRange;
        targets.Add(enemyInRange);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name.Equals("Terrain") || other.gameObject.layer == 2 || other.gameObject.layer == 11) return;

        var enemyOutRange = other.gameObject;
        if (enemyOutRange == currentTarget) currentTarget = null;

        targets.Remove(enemyOutRange);
        FindRandomTarget();
    }

    private void FindRandomTarget()
    {
        if (targets.Count <= 0) return;
        
        var randomTargetIndex = Random.Range(0, targets.Count - 1);

        try
        {
            currentTarget = targets[randomTargetIndex];
            if (currentTarget) return;
            
            targets.RemoveAll(target => target == null);
            FindRandomTarget();
        }
        catch (Exception e)
        {
            FindRandomTarget();
            print("in catch");
        }
    }

    private void OnMouseEnter()
    {
        rangeIndicator.SetActive(true);
    }

    private void OnMouseExit()
    {
        rangeIndicator.SetActive(false);
    }

    public void MakeTowerReady()
    {
        isReady = true;
    }
}
