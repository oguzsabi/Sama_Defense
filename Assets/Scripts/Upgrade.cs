using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Upgrade : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI feedbackText;
    
    [SerializeField] private GameObject waterTower;
    [SerializeField] private GameObject fireTower;
    [SerializeField] private GameObject earthTower;
    [SerializeField] private GameObject woodTower;
    
    [SerializeField] private GameObject waterProjectile;
    [SerializeField] private GameObject fireProjectile;
    [SerializeField] private GameObject earthProjectile;
    [SerializeField] private GameObject woodProjectile;

    private GameSession _gameSession;
    // private Transform _waterTColliderTransform;
    // private Transform _fireTColliderTransform;
    // private Transform _earthTColliderTransform;
    // private Transform _woodTColliderTransform;

    private void Start()
    {
        _gameSession = GameObject.Find("GameSession").GetComponent<GameSession>();

        // _waterTColliderTransform = waterTower.transform;
        // _fireTColliderTransform = fireTower.transform;
        // _earthTColliderTransform = earthTower.transform;
        // _woodTColliderTransform = woodTower.transform;

        feedbackText.enabled = false;
    }

    private SphereCollider GetRangeCollider(GameObject tower)
    {
        var rangeCollider = tower.transform.GetChild(0).GetComponent<SphereCollider>();
        return rangeCollider;
    }

    private void IncreaseRange(GameObject tower)
    {
        var rangeCollider = GetRangeCollider(tower);
        print("before range up " + rangeCollider.radius);
        rangeCollider.radius += 1f;
        print("after range up " + rangeCollider.radius);
        AdjustRangeVisuals(tower);
    }

    private void AdjustRangeVisuals(GameObject tower)
    {
        tower.transform.GetChild(1).GetComponent<Projector>().orthographicSize += 7.35f;
    }

    private void IncreaseDamage(GameObject tower)
    {
        print(tower.name + " damage before upgrade " + tower.GetComponent<Tower>().damage);
        tower.GetComponent<Tower>().damage += 5;
        print(tower.name + " damage after upgrade " + tower.GetComponent<Tower>().damage);
    }
    
    private void IncreaseProjectileAccuracy(GameObject projectile)
    {
        print(projectile.name + " accuracy before upgrade " + projectile.GetComponent<Projectile>().accuracy );
        projectile.GetComponent<Projectile>().accuracy += 10;
        print(projectile.name + " accuracy after upgrade " + projectile.GetComponent<Projectile>().accuracy);
    }

    public void UpgradeFireTowerDamage()
    {
        if (!CheckForEnoughDiamonds(5)) return;
        
        _gameSession.ChangeDiamondAmountBy(-5);
        IncreaseDamage(fireTower);
        DisplayMessage("Successful purchase", Color.green);
    }
    
    public void UpgradeWaterTowerDamage()
    {
        if (!CheckForEnoughDiamonds(5)) return;
        
        _gameSession.ChangeDiamondAmountBy(-5);
        IncreaseDamage(waterTower);
        DisplayMessage("Successful purchase", Color.green);
    }
    
    public void UpgradeEarthTowerDamage()
    {
        if (!CheckForEnoughDiamonds(5)) return;
        
        _gameSession.ChangeDiamondAmountBy(-5);
        IncreaseDamage(earthTower);
        DisplayMessage("Successful purchase", Color.green);
    }
    
    public void UpgradeWoodTowerDamage()
    {
        if (!CheckForEnoughDiamonds(5)) return;
        
        _gameSession.ChangeDiamondAmountBy(-5);
        IncreaseDamage(woodTower);
        DisplayMessage("Successful purchase", Color.green);
    }

    public void UpgradeFireTowerAccuracy()
    {
        if (!CheckForEnoughDiamonds(5)) return;
        
        _gameSession.ChangeDiamondAmountBy(-5);
        IncreaseProjectileAccuracy(fireProjectile);
        DisplayMessage("Successful purchase", Color.green);
    }
    
    public void UpgradeWaterTowerAccuracy()
    {
        if (!CheckForEnoughDiamonds(5)) return;
        
        _gameSession.ChangeDiamondAmountBy(-5);
        IncreaseProjectileAccuracy(waterProjectile);
        DisplayMessage("Successful purchase", Color.green);
    }
    
    public void UpgradeEarthTowerAccuracy()
    {
        if (!CheckForEnoughDiamonds(5)) return;
        
        _gameSession.ChangeDiamondAmountBy(-5);
        IncreaseProjectileAccuracy(earthProjectile);
        DisplayMessage("Successful purchase", Color.green);
    }
    
    public void UpgradeWoodTowerAccuracy()
    {
        if (!CheckForEnoughDiamonds(5)) return;
        
        _gameSession.ChangeDiamondAmountBy(-5);
        IncreaseProjectileAccuracy(woodProjectile);
        DisplayMessage("Successful purchase", Color.green);
    }

    public void UpgradeFireTowerRange()
    {
        if (!CheckForEnoughDiamonds(5)) return;
        
        _gameSession.ChangeDiamondAmountBy(-5);
        IncreaseRange(fireTower);
        DisplayMessage("Successful purchase", Color.green);
    }
    
    public void UpgradeWaterTowerRange()
    {
        if (!CheckForEnoughDiamonds(5)) return;
        
        _gameSession.ChangeDiamondAmountBy(-5);
        IncreaseRange(waterTower);
        DisplayMessage("Successful purchase", Color.green);
    }
    
    public void UpgradeEarthTowerRange()
    {
        if (!CheckForEnoughDiamonds(5)) return;
        
        _gameSession.ChangeDiamondAmountBy(-5);
        IncreaseRange(earthTower);
        DisplayMessage("Successful purchase", Color.green);
    }
    
    public void UpgradeWoodTowerRange()
    {
        if (!CheckForEnoughDiamonds(5)) return;
        
        _gameSession.ChangeDiamondAmountBy(-5);
        IncreaseRange(woodTower);
        DisplayMessage("Successful purchase", Color.green);
    }

    public void UpgradeFireTowerSpeed()
    {
        
    }
    
    public void UpgradeWaterTowerSpeed()
    {
        
    }
    
    public void UpgradeEarthTowerSpeed()
    {
        
    }
    
    public void UpgradeWoodTowerSpeed()
    {
        
    }

    private bool CheckForEnoughDiamonds(int cost)
    {
        if (_gameSession.AreThereEnoughDiamonds(cost))
        {
            return true;
        }
        else
        {
            DisplayMessage("Insufficient amount of diamonds", Color.red);
            return false;
        }
    }

    private void DisplayMessage(string message, Color color)
    {
        feedbackText.text = message;
        feedbackText.color = color;
        feedbackText.enabled = true;
    }
}
