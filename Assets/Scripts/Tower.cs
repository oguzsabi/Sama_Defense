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

    private bool shootAvailable = true;
    private readonly List<GameObject> targets = new List<GameObject>();
    private GameObject currentTarget;
    private bool isReady;

    // default rangeIndicator size = 15.8, default range radius = 2.15

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

        yield return new WaitForSeconds(1/fireRate);
        
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
        catch (Exception)
        {
            FindRandomTarget();
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
    
    public ElementType Element => element;
}
