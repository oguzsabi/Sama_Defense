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

    private void Start()
    {
        _gameSession = GameObject.Find("GameSession").GetComponent<GameSession>();
        feedbackText.enabled = false;
    }
    
    /// <summary>
    /// Increases the damage of tower by 5
    /// </summary>
    /// <param name="tower"></param>
    private void IncreaseDamage(GameObject tower)
    {
        tower.GetComponent<Tower>().damage += 5;
    }
    
    /// <summary>
    /// Increases the accuracy value of projectile by 10
    /// </summary>
    /// <param name="projectile"></param>
    private void IncreaseProjectileAccuracy(GameObject projectile)
    {
        projectile.GetComponent<Projectile>().accuracy += 1;
    }
    
    /// <summary>
    /// Gets the range collider of tower
    /// </summary>
    /// <param name="tower"></param>
    /// <returns>SphereCollider rangeCollider</returns>
    private SphereCollider GetRangeCollider(GameObject tower)
    {
        var rangeCollider = tower.transform.GetChild(0).GetComponent<SphereCollider>();
        return rangeCollider;
    }
    
    /// <summary>
    /// Increases the range of the tower by 0.1
    /// </summary>
    /// <param name="tower"></param>
    private void IncreaseRange(GameObject tower)
    {
        var rangeCollider = GetRangeCollider(tower);
        rangeCollider.radius += 0.1f;
        AdjustRangeVisuals(tower);
    }
    
    /// <summary>
    /// Increase range projections radius
    /// </summary>
    /// <param name="tower"></param>
    private void AdjustRangeVisuals(GameObject tower)
    {
        tower.transform.GetChild(1).GetComponent<Projector>().orthographicSize += 0.735f;
    }
    
    /// <summary>
    /// Increase attack speed of tower by 0.1 per second
    /// </summary>
    /// <param name="tower"></param>
    private void IncreaseAttackSpeed(GameObject tower)
    {
        tower.GetComponent<Tower>().fireRate += 0.1f;
    }
    
    /// <summary>
    /// Upgrades fire tower's damage
    /// Decrements diamonds by 5
    /// </summary>
    public void UpgradeFireTowerDamage()
    {
        if (!CheckForEnoughDiamonds(5)) return;
        
        _gameSession.ChangeDiamondAmountBy(-5);
        IncreaseDamage(fireTower);
        ApplyForExistingTowers(Tower.ElementType.Fire, 1);
        DisplayMessage("Successful purchase", Color.green);
    }
    
    /// <summary>
    /// Upgrades water tower's damage
    /// Decrements diamonds by 5
    /// </summary>
    public void UpgradeWaterTowerDamage()
    {
        if (!CheckForEnoughDiamonds(5)) return;
        
        _gameSession.ChangeDiamondAmountBy(-5);
        IncreaseDamage(waterTower);
        ApplyForExistingTowers(Tower.ElementType.Water, 1);
        DisplayMessage("Successful purchase", Color.green);
    }
    
    /// <summary>
    /// Upgrades earth tower's damage
    /// Decrements diamonds by 5
    /// </summary>
    public void UpgradeEarthTowerDamage()
    {
        if (!CheckForEnoughDiamonds(5)) return;
        
        _gameSession.ChangeDiamondAmountBy(-5);
        IncreaseDamage(earthTower);
        ApplyForExistingTowers(Tower.ElementType.Earth, 1);
        DisplayMessage("Successful purchase", Color.green);
    }
    
    /// <summary>
    /// Upgrades wood tower's damage
    /// Decrements diamonds by 5
    /// </summary>
    public void UpgradeWoodTowerDamage()
    {
        if (!CheckForEnoughDiamonds(5)) return;
        
        _gameSession.ChangeDiamondAmountBy(-5);
        IncreaseDamage(woodTower);
        ApplyForExistingTowers(Tower.ElementType.Wood, 1);
        DisplayMessage("Successful purchase", Color.green);
    }
    
    /// <summary>
    /// Upgrades fire tower's accuracy
    /// Decrements diamonds by 5
    /// </summary>
    public void UpgradeFireTowerAccuracy()
    {
        if (!CheckForEnoughDiamonds(5)) return;
        
        _gameSession.ChangeDiamondAmountBy(-5);
        IncreaseProjectileAccuracy(fireProjectile);
        DisplayMessage("Successful purchase", Color.green);
    }
    
    /// <summary>
    /// Upgrades water tower's accuracy
    /// Decrements diamonds by 5
    /// </summary>
    public void UpgradeWaterTowerAccuracy()
    {
        if (!CheckForEnoughDiamonds(5)) return;
        
        _gameSession.ChangeDiamondAmountBy(-5);
        IncreaseProjectileAccuracy(waterProjectile);
        DisplayMessage("Successful purchase", Color.green);
    }
    /// <summary>
    /// Upgrades earth tower's accuracy
    /// Decrements diamonds by 5
    /// </summary>
    public void UpgradeEarthTowerAccuracy()
    {
        if (!CheckForEnoughDiamonds(5)) return;
        
        _gameSession.ChangeDiamondAmountBy(-5);
        IncreaseProjectileAccuracy(earthProjectile);
        DisplayMessage("Successful purchase", Color.green);
    }
    
    /// <summary>
    /// Upgrades wood tower's accuracy
    /// Decrements diamonds by 5
    /// </summary>
    public void UpgradeWoodTowerAccuracy()
    {
        if (!CheckForEnoughDiamonds(5)) return;
        
        _gameSession.ChangeDiamondAmountBy(-5);
        IncreaseProjectileAccuracy(woodProjectile);
        DisplayMessage("Successful purchase", Color.green);
    }
    
    /// <summary>
    /// Upgrades fire tower's range
    /// Decrements diamonds by 5
    /// </summary>
    public void UpgradeFireTowerRange()
    {
        if (!CheckForEnoughDiamonds(5)) return;
        
        _gameSession.ChangeDiamondAmountBy(-5);
        IncreaseRange(fireTower);
        ApplyForExistingTowers(Tower.ElementType.Fire, 2);
        DisplayMessage("Successful purchase", Color.green);
    }
    
    /// <summary>
    /// Upgrades water tower's range
    /// Decrements diamonds by 5
    /// </summary>
    public void UpgradeWaterTowerRange()
    {
        if (!CheckForEnoughDiamonds(5)) return;
        
        _gameSession.ChangeDiamondAmountBy(-5);
        IncreaseRange(waterTower);
        ApplyForExistingTowers(Tower.ElementType.Water, 2);
        DisplayMessage("Successful purchase", Color.green);
    }
    
    /// <summary>
    /// Upgrades earth tower's range
    /// Decrements diamonds by 5
    /// </summary>
    public void UpgradeEarthTowerRange()
    {
        if (!CheckForEnoughDiamonds(5)) return;
        
        _gameSession.ChangeDiamondAmountBy(-5);
        IncreaseRange(earthTower);
        ApplyForExistingTowers(Tower.ElementType.Earth, 2);
        DisplayMessage("Successful purchase", Color.green);
    }
    
    /// <summary>
    /// Upgrades wood tower's range
    /// Decrements diamonds by 5
    /// </summary>
    public void UpgradeWoodTowerRange()
    {
        if (!CheckForEnoughDiamonds(5)) return;
        
        _gameSession.ChangeDiamondAmountBy(-5);
        IncreaseRange(woodTower);
        ApplyForExistingTowers(Tower.ElementType.Wood, 2);
        DisplayMessage("Successful purchase", Color.green);
    }
    
    /// <summary>
    /// Upgrades fire tower's attack speed
    /// Decrements diamonds by 5
    /// </summary>
    public void UpgradeFireTowerSpeed()
    {
        if (!CheckForEnoughDiamonds(5)) return;
        
        _gameSession.ChangeDiamondAmountBy(-5);
        IncreaseAttackSpeed(fireTower);
        ApplyForExistingTowers(Tower.ElementType.Fire, 3);
        DisplayMessage("Successful purchase", Color.green);
    }
    
    /// <summary>
    /// Upgrades water tower's attack speed
    /// Decrements diamonds by 5
    /// </summary>
    public void UpgradeWaterTowerSpeed()
    {
        if (!CheckForEnoughDiamonds(5)) return;
        
        _gameSession.ChangeDiamondAmountBy(-5);
        IncreaseAttackSpeed(waterTower);
        ApplyForExistingTowers(Tower.ElementType.Water, 3);
        DisplayMessage("Successful purchase", Color.green);
    }
    
    /// <summary>
    /// Upgrades earth tower's attack speed
    /// Decrements diamonds by 5
    /// </summary>
    public void UpgradeEarthTowerSpeed()
    {
        if (!CheckForEnoughDiamonds(5)) return;
        
        _gameSession.ChangeDiamondAmountBy(-5);
        IncreaseAttackSpeed(earthTower);
        ApplyForExistingTowers(Tower.ElementType.Earth, 3);
        DisplayMessage("Successful purchase", Color.green);
    }
    
    /// <summary>
    /// Upgrades wood tower's attack speed
    /// Decrements diamonds by 5
    /// </summary>
    public void UpgradeWoodTowerSpeed()
    {
        if (!CheckForEnoughDiamonds(5)) return;
        
        _gameSession.ChangeDiamondAmountBy(-5);
        IncreaseAttackSpeed(woodTower);
        ApplyForExistingTowers(Tower.ElementType.Wood, 3);
        DisplayMessage("Successful purchase", Color.green);
    }
    
    /// <summary>
    /// Upgrades the number of towers that can be placed,
    /// Decrements diamonds by 20
    /// </summary>
    public void IncrementMaximumTowerCount()
    {
        if (!CheckForEnoughDiamonds(20)) return;
        
        _gameSession.ChangeDiamondAmountBy(-20);
        PlayerDataManager.IncrementMaximumTowerCount();
        _gameSession.ChangeMaxTowerCount();
        DisplayMessage("Successful purchase", Color.green);
    }
    
    /// <summary>
    /// Checks if the transaction can be done with current diamond amount
    /// </summary>
    /// <param name="cost"></param>
    /// <returns></returns>
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
    
    /// <summary>
    /// Prompts user with messages
    /// </summary>
    /// <param name="message"></param>
    /// <param name="color"></param>
    private void DisplayMessage(string message, Color color)
    {
        feedbackText.text = message;
        feedbackText.color = color;
        feedbackText.enabled = true;
        _gameSession.SaveDiamondAmount();
    }

    /// <summary>
    /// Applies upgrades to already placed towers
    /// </summary>
    /// <param name="element"></param>
    /// <param name="upgradeIndex"></param>
    private void ApplyForExistingTowers(Tower.ElementType element, int upgradeIndex)
    {
        foreach (var tower in TowerPlacementController.CurrentTowers)
        {
            var towerElement = tower.GetComponent<Tower>().Element;
            if (towerElement == element)
            {
                switch (upgradeIndex)
                {
                    // Case 1 = Damage, Case 2 = Range, Case 3 = Speed
                    case 1:
                        IncreaseDamage(tower);
                        break;
                    case 2:
                        IncreaseRange(tower);
                        break;
                    case 3:
                        IncreaseAttackSpeed(tower);
                        break;
                }
            }
        }
    }
}
