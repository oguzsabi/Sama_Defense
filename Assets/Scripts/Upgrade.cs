using System;
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

    // Order is: fire damage, fire accuracy, fire range, fire speed, water..., earth, wood
    [SerializeField] private TextMeshProUGUI[] upgradeTexts;

    private GameSession _gameSession;

    private void Awake()
    {
        TowerDataManager.ResetAllCosts();
        CheckTextDiamondValues();
    }

    private void Start()
    {
        _gameSession = GameObject.Find("GameSession").GetComponent<GameSession>();
        feedbackText.enabled = false;
    }

    private void CheckTextDiamondValues()
    {
        upgradeTexts[0].text = "Fire Tower ↑ +5\n(" + TowerDataManager.GetFireCosts()[0] + " Diamonds)";
        upgradeTexts[1].text = "Fire Tower ↑ +5\n(" + TowerDataManager.GetFireCosts()[1] + " Diamonds)";
        upgradeTexts[2].text = "Fire Tower ↑ +5\n(" + TowerDataManager.GetFireCosts()[2] + " Diamonds)";
        upgradeTexts[3].text = "Fire Tower ↑ +5\n(" + TowerDataManager.GetFireCosts()[3] + " Diamonds)";
        
        upgradeTexts[4].text = "Water Tower ↑ +5\n(" + TowerDataManager.GetWaterCosts()[0] + " Diamonds)";
        upgradeTexts[5].text = "Water Tower ↑ +5\n(" + TowerDataManager.GetWaterCosts()[1] + " Diamonds)";
        upgradeTexts[6].text = "Water Tower ↑ +5\n(" + TowerDataManager.GetWaterCosts()[2] + " Diamonds)";
        upgradeTexts[7].text = "Water Tower ↑ +5\n(" + TowerDataManager.GetWaterCosts()[3] + " Diamonds)";
        
        upgradeTexts[8].text = "Earth Tower ↑ +5\n(" + TowerDataManager.GetEarthCosts()[0] + " Diamonds)";
        upgradeTexts[9].text = "Earth Tower ↑ +5\n(" + TowerDataManager.GetEarthCosts()[1] + " Diamonds)";
        upgradeTexts[10].text = "Earth Tower ↑ +5\n(" + TowerDataManager.GetEarthCosts()[2] + " Diamonds)";
        upgradeTexts[11].text = "Earth Tower ↑ +5\n(" + TowerDataManager.GetEarthCosts()[3] + " Diamonds)";
        
        upgradeTexts[12].text = "Wood Tower ↑ +5\n(" + TowerDataManager.GetWoodCosts()[0] + " Diamonds)";
        upgradeTexts[13].text = "Wood Tower ↑ +5\n(" + TowerDataManager.GetWoodCosts()[1] + " Diamonds)";
        upgradeTexts[14].text = "Wood Tower ↑ +5\n(" + TowerDataManager.GetWoodCosts()[2] + " Diamonds)";
        upgradeTexts[15].text = "Wood Tower ↑ +5\n(" + TowerDataManager.GetWoodCosts()[3] + " Diamonds)";
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
        var cost = TowerDataManager.GetFireCosts()[0];
        if (!CheckForEnoughDiamonds(cost)) return;
        
        TowerDataManager.IncreaseAndSaveDamageCost(Tower.ElementType.Fire);
        CheckTextDiamondValues();
        _gameSession.ChangeDiamondAmountBy(-cost);
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
        var cost = TowerDataManager.GetWaterCosts()[0];
        if (!CheckForEnoughDiamonds(cost)) return;
        
        TowerDataManager.IncreaseAndSaveDamageCost(Tower.ElementType.Water);
        CheckTextDiamondValues();
        _gameSession.ChangeDiamondAmountBy(-cost);
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
        var cost = TowerDataManager.GetEarthCosts()[0];
        if (!CheckForEnoughDiamonds(cost)) return;
        
        TowerDataManager.IncreaseAndSaveDamageCost(Tower.ElementType.Earth);
        CheckTextDiamondValues();
        _gameSession.ChangeDiamondAmountBy(-cost);
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
        var cost = TowerDataManager.GetWoodCosts()[0];
        if (!CheckForEnoughDiamonds(cost)) return;
        
        TowerDataManager.IncreaseAndSaveDamageCost(Tower.ElementType.Wood);
        CheckTextDiamondValues();
        _gameSession.ChangeDiamondAmountBy(-cost);
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
        var cost = TowerDataManager.GetFireCosts()[1];
        if (!CheckForEnoughDiamonds(cost)) return;
        
        TowerDataManager.IncreaseAndSaveAccuracyCost(Tower.ElementType.Fire);
        CheckTextDiamondValues();
        _gameSession.ChangeDiamondAmountBy(-cost);
        IncreaseProjectileAccuracy(fireProjectile);
        DisplayMessage("Successful purchase", Color.green);
    }
    
    /// <summary>
    /// Upgrades water tower's accuracy
    /// Decrements diamonds by 5
    /// </summary>
    public void UpgradeWaterTowerAccuracy()
    {
        var cost = TowerDataManager.GetWaterCosts()[1];
        if (!CheckForEnoughDiamonds(cost)) return;
        
        TowerDataManager.IncreaseAndSaveAccuracyCost(Tower.ElementType.Water);
        CheckTextDiamondValues();
        _gameSession.ChangeDiamondAmountBy(-cost);
        IncreaseProjectileAccuracy(waterProjectile);
        DisplayMessage("Successful purchase", Color.green);
    }
    /// <summary>
    /// Upgrades earth tower's accuracy
    /// Decrements diamonds by 5
    /// </summary>
    public void UpgradeEarthTowerAccuracy()
    {
        var cost = TowerDataManager.GetEarthCosts()[1];
        if (!CheckForEnoughDiamonds(cost)) return;
        
        TowerDataManager.IncreaseAndSaveAccuracyCost(Tower.ElementType.Earth);
        CheckTextDiamondValues();
        _gameSession.ChangeDiamondAmountBy(-cost);
        IncreaseProjectileAccuracy(earthProjectile);
        DisplayMessage("Successful purchase", Color.green);
    }
    
    /// <summary>
    /// Upgrades wood tower's accuracy
    /// Decrements diamonds by 5
    /// </summary>
    public void UpgradeWoodTowerAccuracy()
    {
        var cost = TowerDataManager.GetWoodCosts()[1];
        if (!CheckForEnoughDiamonds(cost)) return;
        
        TowerDataManager.IncreaseAndSaveAccuracyCost(Tower.ElementType.Wood);
        CheckTextDiamondValues();
        _gameSession.ChangeDiamondAmountBy(-cost);
        IncreaseProjectileAccuracy(woodProjectile);
        DisplayMessage("Successful purchase", Color.green);
    }
    
    /// <summary>
    /// Upgrades fire tower's range
    /// Decrements diamonds by 5
    /// </summary>
    public void UpgradeFireTowerRange()
    {
        var cost = TowerDataManager.GetFireCosts()[2];
        if (!CheckForEnoughDiamonds(cost)) return;
        
        TowerDataManager.IncreaseAndSaveRangeCost(Tower.ElementType.Fire);
        CheckTextDiamondValues();
        _gameSession.ChangeDiamondAmountBy(-cost);
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
        var cost = TowerDataManager.GetWaterCosts()[2];
        if (!CheckForEnoughDiamonds(cost)) return;
        
        TowerDataManager.IncreaseAndSaveRangeCost(Tower.ElementType.Water);
        CheckTextDiamondValues();
        _gameSession.ChangeDiamondAmountBy(-cost);
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
        var cost = TowerDataManager.GetEarthCosts()[2];
        if (!CheckForEnoughDiamonds(cost)) return;
        
        TowerDataManager.IncreaseAndSaveRangeCost(Tower.ElementType.Earth);
        CheckTextDiamondValues();
        _gameSession.ChangeDiamondAmountBy(-cost);
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
        var cost = TowerDataManager.GetWoodCosts()[2];
        if (!CheckForEnoughDiamonds(cost)) return;
        
        TowerDataManager.IncreaseAndSaveRangeCost(Tower.ElementType.Wood);
        CheckTextDiamondValues();
        _gameSession.ChangeDiamondAmountBy(-cost);
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
        var cost = TowerDataManager.GetFireCosts()[3];
        if (!CheckForEnoughDiamonds(cost)) return;
        
        TowerDataManager.IncreaseAndSaveSpeedCost(Tower.ElementType.Fire);
        CheckTextDiamondValues();
        _gameSession.ChangeDiamondAmountBy(-cost);
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
        var cost = TowerDataManager.GetWaterCosts()[3];
        if (!CheckForEnoughDiamonds(cost)) return;
        
        TowerDataManager.IncreaseAndSaveSpeedCost(Tower.ElementType.Water);
        CheckTextDiamondValues();
        _gameSession.ChangeDiamondAmountBy(-cost);
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
        var cost = TowerDataManager.GetEarthCosts()[3];
        if (!CheckForEnoughDiamonds(cost)) return;
        
        TowerDataManager.IncreaseAndSaveSpeedCost(Tower.ElementType.Earth);
        CheckTextDiamondValues();
        _gameSession.ChangeDiamondAmountBy(-cost);
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
        var cost = TowerDataManager.GetWoodCosts()[3];
        if (!CheckForEnoughDiamonds(cost)) return;
        
        TowerDataManager.IncreaseAndSaveSpeedCost(Tower.ElementType.Wood);
        CheckTextDiamondValues();
        _gameSession.ChangeDiamondAmountBy(-cost);
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
