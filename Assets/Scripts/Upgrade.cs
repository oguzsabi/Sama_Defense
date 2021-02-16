using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Upgrade : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI feedbackText;
    [SerializeField] private TextMeshProUGUI towerCountCostText;

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
        // UpgradeCostManager.ResetAllCosts();
        // TowerDataManager.ResetAllData();
        // PlayerDataManager.ResetMaxTowerCount();
        // PlayerDataManager.GiveMaxDiamond();
        // PlayerDataManager.ResetDiamondAmount();
        // PlayerDataManager.ResetMaxTowerCount();
        CheckTextDiamondValues();
    }

    private void Start()
    {
        _gameSession = GameObject.Find("GameSession").GetComponent<GameSession>();
        feedbackText.enabled = false;
    }

    private void CheckTextDiamondValues()
    {
        upgradeTexts[0].text = "Fire Tower ↑ +5\n(" + UpgradeCostManager.GetFireCosts()[0] + " Diamonds)";
        upgradeTexts[1].text = "Fire Tower ↑ +5\n(" + UpgradeCostManager.GetFireCosts()[1] + " Diamonds)";
        upgradeTexts[2].text = "Fire Tower ↑ +5\n(" + UpgradeCostManager.GetFireCosts()[2] + " Diamonds)";
        upgradeTexts[3].text = "Fire Tower ↑ +5\n(" + UpgradeCostManager.GetFireCosts()[3] + " Diamonds)";
        
        upgradeTexts[4].text = "Water Tower ↑ +5\n(" + UpgradeCostManager.GetWaterCosts()[0] + " Diamonds)";
        upgradeTexts[5].text = "Water Tower ↑ +5\n(" + UpgradeCostManager.GetWaterCosts()[1] + " Diamonds)";
        upgradeTexts[6].text = "Water Tower ↑ +5\n(" + UpgradeCostManager.GetWaterCosts()[2] + " Diamonds)";
        upgradeTexts[7].text = "Water Tower ↑ +5\n(" + UpgradeCostManager.GetWaterCosts()[3] + " Diamonds)";
        
        upgradeTexts[8].text = "Earth Tower ↑ +5\n(" + UpgradeCostManager.GetEarthCosts()[0] + " Diamonds)";
        upgradeTexts[9].text = "Earth Tower ↑ +5\n(" + UpgradeCostManager.GetEarthCosts()[1] + " Diamonds)";
        upgradeTexts[10].text = "Earth Tower ↑ +5\n(" + UpgradeCostManager.GetEarthCosts()[2] + " Diamonds)";
        upgradeTexts[11].text = "Earth Tower ↑ +5\n(" + UpgradeCostManager.GetEarthCosts()[3] + " Diamonds)";
        
        upgradeTexts[12].text = "Wood Tower ↑ +5\n(" + UpgradeCostManager.GetWoodCosts()[0] + " Diamonds)";
        upgradeTexts[13].text = "Wood Tower ↑ +5\n(" + UpgradeCostManager.GetWoodCosts()[1] + " Diamonds)";
        upgradeTexts[14].text = "Wood Tower ↑ +5\n(" + UpgradeCostManager.GetWoodCosts()[2] + " Diamonds)";
        upgradeTexts[15].text = "Wood Tower ↑ +5\n(" + UpgradeCostManager.GetWoodCosts()[3] + " Diamonds)";
        
        towerCountCostText.text = "+1 Max. Tower Count\n(" + UpgradeCostManager.GetMaxTowerCost() +" Diamonds)";
    }
    
    /// <summary>
    /// Increases the damage of tower by 5
    /// </summary>
    /// <param name="tower"></param>
    private void IncreaseDamageForExistingTowers(GameObject tower)
    {
        tower.GetComponent<Tower>().damage += 5;
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
    private void IncreaseRangeForExistingTowers(GameObject tower)
    {
        var rangeCollider = GetRangeCollider(tower);
        rangeCollider.radius += 0.1f;
        AdjustRangeVisualsForExistingTowers(tower);
    }
    
    /// <summary>
    /// Increase range projections radius
    /// </summary>
    /// <param name="tower"></param>
    private void AdjustRangeVisualsForExistingTowers(GameObject tower)
    {
        tower.transform.GetChild(1).GetComponent<Projector>().orthographicSize += 0.735f;
    }
    
    /// <summary>
    /// Increase attack speed of tower by 0.1 per second
    /// </summary>
    /// <param name="tower"></param>
    private void IncreaseAttackSpeedForExistingTowers(GameObject tower)
    {
        tower.GetComponent<Tower>().fireRate += 0.1f;
    }
    
    /// <summary>
    /// Upgrades fire tower's damage
    /// Decrements diamonds by 5
    /// </summary>
    public void UpgradeFireTowerDamage()
    {
        var cost = UpgradeCostManager.GetFireCosts()[0];
        if (!CheckForEnoughDiamonds(cost)) return;
        
        UpgradeCostManager.IncreaseAndSaveDamageCost(Tower.ElementType.Fire);
        CheckTextDiamondValues();
        _gameSession.ChangeDiamondAmountBy(-cost);
        TowerDataManager.IncreaseAndSaveDamage(Tower.ElementType.Fire);
        // IncreaseDamageForExistingTowers(fireTower);
        ApplyForExistingTowers(Tower.ElementType.Fire, 1);
        DisplayMessage("Successful purchase", Color.green);
    }
    
    /// <summary>
    /// Upgrades water tower's damage
    /// Decrements diamonds by 5
    /// </summary>
    public void UpgradeWaterTowerDamage()
    {
        var cost = UpgradeCostManager.GetWaterCosts()[0];
        if (!CheckForEnoughDiamonds(cost)) return;
        
        UpgradeCostManager.IncreaseAndSaveDamageCost(Tower.ElementType.Water);
        CheckTextDiamondValues();
        _gameSession.ChangeDiamondAmountBy(-cost);
        TowerDataManager.IncreaseAndSaveDamage(Tower.ElementType.Water);
        // IncreaseDamageForExistingTowers(waterTower);
        ApplyForExistingTowers(Tower.ElementType.Water, 1);
        DisplayMessage("Successful purchase", Color.green);
    }
    
    /// <summary>
    /// Upgrades earth tower's damage
    /// Decrements diamonds by 5
    /// </summary>
    public void UpgradeEarthTowerDamage()
    {
        var cost = UpgradeCostManager.GetEarthCosts()[0];
        if (!CheckForEnoughDiamonds(cost)) return;
        
        UpgradeCostManager.IncreaseAndSaveDamageCost(Tower.ElementType.Earth);
        CheckTextDiamondValues();
        _gameSession.ChangeDiamondAmountBy(-cost);
        TowerDataManager.IncreaseAndSaveDamage(Tower.ElementType.Earth);
        // IncreaseDamageForExistingTowers(earthTower);
        ApplyForExistingTowers(Tower.ElementType.Earth, 1);
        DisplayMessage("Successful purchase", Color.green);
    }
    
    /// <summary>
    /// Upgrades wood tower's damage
    /// Decrements diamonds by 5
    /// </summary>
    public void UpgradeWoodTowerDamage()
    {
        var cost = UpgradeCostManager.GetWoodCosts()[0];
        if (!CheckForEnoughDiamonds(cost)) return;
        
        UpgradeCostManager.IncreaseAndSaveDamageCost(Tower.ElementType.Wood);
        CheckTextDiamondValues();
        _gameSession.ChangeDiamondAmountBy(-cost);
        TowerDataManager.IncreaseAndSaveDamage(Tower.ElementType.Wood);
        // IncreaseDamageForExistingTowers(woodTower);
        ApplyForExistingTowers(Tower.ElementType.Wood, 1);
        DisplayMessage("Successful purchase", Color.green);
    }
    
    /// <summary>
    /// Upgrades fire tower's accuracy
    /// Decrements diamonds by 5
    /// </summary>
    public void UpgradeFireTowerAccuracy()
    {
        var cost = UpgradeCostManager.GetFireCosts()[1];
        if (!CheckForEnoughDiamonds(cost)) return;
        
        UpgradeCostManager.IncreaseAndSaveAccuracyCost(Tower.ElementType.Fire);
        CheckTextDiamondValues();
        _gameSession.ChangeDiamondAmountBy(-cost);
        TowerDataManager.IncreaseAndSaveAccuracy(Tower.ElementType.Fire);
        // IncreaseProjectileAccuracyForExistingTowers(fireProjectile);
        DisplayMessage("Successful purchase", Color.green);
    }
    
    /// <summary>
    /// Upgrades water tower's accuracy
    /// Decrements diamonds by 5
    /// </summary>
    public void UpgradeWaterTowerAccuracy()
    {
        var cost = UpgradeCostManager.GetWaterCosts()[1];
        if (!CheckForEnoughDiamonds(cost)) return;
        
        UpgradeCostManager.IncreaseAndSaveAccuracyCost(Tower.ElementType.Water);
        CheckTextDiamondValues();
        _gameSession.ChangeDiamondAmountBy(-cost);
        TowerDataManager.IncreaseAndSaveAccuracy(Tower.ElementType.Water);
        // IncreaseProjectileAccuracyForExistingTowers(waterProjectile);
        DisplayMessage("Successful purchase", Color.green);
    }
    /// <summary>
    /// Upgrades earth tower's accuracy
    /// Decrements diamonds by 5
    /// </summary>
    public void UpgradeEarthTowerAccuracy()
    {
        var cost = UpgradeCostManager.GetEarthCosts()[1];
        if (!CheckForEnoughDiamonds(cost)) return;
        
        UpgradeCostManager.IncreaseAndSaveAccuracyCost(Tower.ElementType.Earth);
        CheckTextDiamondValues();
        _gameSession.ChangeDiamondAmountBy(-cost);
        TowerDataManager.IncreaseAndSaveAccuracy(Tower.ElementType.Earth);
        // IncreaseProjectileAccuracyForExistingTowers(earthProjectile);
        DisplayMessage("Successful purchase", Color.green);
    }
    
    /// <summary>
    /// Upgrades wood tower's accuracy
    /// Decrements diamonds by 5
    /// </summary>
    public void UpgradeWoodTowerAccuracy()
    {
        var cost = UpgradeCostManager.GetWoodCosts()[1];
        if (!CheckForEnoughDiamonds(cost)) return;
        
        UpgradeCostManager.IncreaseAndSaveAccuracyCost(Tower.ElementType.Wood);
        CheckTextDiamondValues();
        _gameSession.ChangeDiamondAmountBy(-cost);
        TowerDataManager.IncreaseAndSaveAccuracy(Tower.ElementType.Wood);
        // IncreaseProjectileAccuracyForExistingTowers(woodProjectile);
        DisplayMessage("Successful purchase", Color.green);
    }
    
    /// <summary>
    /// Upgrades fire tower's range
    /// Decrements diamonds by 5
    /// </summary>
    public void UpgradeFireTowerRange()
    {
        var cost = UpgradeCostManager.GetFireCosts()[2];
        if (!CheckForEnoughDiamonds(cost)) return;
        
        UpgradeCostManager.IncreaseAndSaveRangeCost(Tower.ElementType.Fire);
        CheckTextDiamondValues();
        _gameSession.ChangeDiamondAmountBy(-cost);
        TowerDataManager.IncreaseAndSaveRange(Tower.ElementType.Fire);
        TowerDataManager.IncreaseAndSaveRangeVisual(Tower.ElementType.Fire);
        // IncreaseRangeForExistingTowers(fireTower);
        ApplyForExistingTowers(Tower.ElementType.Fire, 2);
        DisplayMessage("Successful purchase", Color.green);
    }
    
    /// <summary>
    /// Upgrades water tower's range
    /// Decrements diamonds by 5
    /// </summary>
    public void UpgradeWaterTowerRange()
    {
        var cost = UpgradeCostManager.GetWaterCosts()[2];
        if (!CheckForEnoughDiamonds(cost)) return;
        
        UpgradeCostManager.IncreaseAndSaveRangeCost(Tower.ElementType.Water);
        CheckTextDiamondValues();
        _gameSession.ChangeDiamondAmountBy(-cost);
        TowerDataManager.IncreaseAndSaveRange(Tower.ElementType.Water);
        TowerDataManager.IncreaseAndSaveRangeVisual(Tower.ElementType.Water);
        // IncreaseRangeForExistingTowers(waterTower);
        ApplyForExistingTowers(Tower.ElementType.Water, 2);
        DisplayMessage("Successful purchase", Color.green);
    }
    
    /// <summary>
    /// Upgrades earth tower's range
    /// Decrements diamonds by 5
    /// </summary>
    public void UpgradeEarthTowerRange()
    {
        var cost = UpgradeCostManager.GetEarthCosts()[2];
        if (!CheckForEnoughDiamonds(cost)) return;
        
        UpgradeCostManager.IncreaseAndSaveRangeCost(Tower.ElementType.Earth);
        CheckTextDiamondValues();
        _gameSession.ChangeDiamondAmountBy(-cost);
        TowerDataManager.IncreaseAndSaveRange(Tower.ElementType.Earth);
        TowerDataManager.IncreaseAndSaveRangeVisual(Tower.ElementType.Earth);
        // IncreaseRangeForExistingTowers(earthTower);
        ApplyForExistingTowers(Tower.ElementType.Earth, 2);
        DisplayMessage("Successful purchase", Color.green);
    }
    
    /// <summary>
    /// Upgrades wood tower's range
    /// Decrements diamonds by 5
    /// </summary>
    public void UpgradeWoodTowerRange()
    {
        var cost = UpgradeCostManager.GetWoodCosts()[2];
        if (!CheckForEnoughDiamonds(cost)) return;
        
        UpgradeCostManager.IncreaseAndSaveRangeCost(Tower.ElementType.Wood);
        CheckTextDiamondValues();
        // _gameSession.ChangeDiamondAmountBy(-cost);
        TowerDataManager.IncreaseAndSaveRange(Tower.ElementType.Wood);
        TowerDataManager.IncreaseAndSaveRangeVisual(Tower.ElementType.Wood);
        IncreaseRangeForExistingTowers(woodTower);
        ApplyForExistingTowers(Tower.ElementType.Wood, 2);
        DisplayMessage("Successful purchase", Color.green);
    }
    
    /// <summary>
    /// Upgrades fire tower's attack speed
    /// Decrements diamonds by 5
    /// </summary>
    public void UpgradeFireTowerSpeed()
    {
        var cost = UpgradeCostManager.GetFireCosts()[3];
        if (!CheckForEnoughDiamonds(cost)) return;
        
        UpgradeCostManager.IncreaseAndSaveSpeedCost(Tower.ElementType.Fire);
        CheckTextDiamondValues();
        _gameSession.ChangeDiamondAmountBy(-cost);
        TowerDataManager.IncreaseAndSaveSpeed(Tower.ElementType.Fire);
        // IncreaseAttackSpeedForExistingTowers(fireTower);
        ApplyForExistingTowers(Tower.ElementType.Fire, 3);
        DisplayMessage("Successful purchase", Color.green);
    }
    
    /// <summary>
    /// Upgrades water tower's attack speed
    /// Decrements diamonds by 5
    /// </summary>
    public void UpgradeWaterTowerSpeed()
    {
        var cost = UpgradeCostManager.GetWaterCosts()[3];
        if (!CheckForEnoughDiamonds(cost)) return;
        
        UpgradeCostManager.IncreaseAndSaveSpeedCost(Tower.ElementType.Water);
        CheckTextDiamondValues();
        _gameSession.ChangeDiamondAmountBy(-cost);
        TowerDataManager.IncreaseAndSaveSpeed(Tower.ElementType.Water);
        // IncreaseAttackSpeedForExistingTowers(waterTower);
        ApplyForExistingTowers(Tower.ElementType.Water, 3);
        DisplayMessage("Successful purchase", Color.green);
    }
    
    /// <summary>
    /// Upgrades earth tower's attack speed
    /// Decrements diamonds by 5
    /// </summary>
    public void UpgradeEarthTowerSpeed()
    {
        var cost = UpgradeCostManager.GetEarthCosts()[3];
        if (!CheckForEnoughDiamonds(cost)) return;
        
        UpgradeCostManager.IncreaseAndSaveSpeedCost(Tower.ElementType.Earth);
        CheckTextDiamondValues();
        _gameSession.ChangeDiamondAmountBy(-cost);
        TowerDataManager.IncreaseAndSaveSpeed(Tower.ElementType.Earth);
        // IncreaseAttackSpeedForExistingTowers(earthTower);
        ApplyForExistingTowers(Tower.ElementType.Earth, 3);
        DisplayMessage("Successful purchase", Color.green);
    }
    
    /// <summary>
    /// Upgrades wood tower's attack speed
    /// Decrements diamonds by 5
    /// </summary>
    public void UpgradeWoodTowerSpeed()
    {
        var cost = UpgradeCostManager.GetWoodCosts()[3];
        if (!CheckForEnoughDiamonds(cost)) return;
        
        UpgradeCostManager.IncreaseAndSaveSpeedCost(Tower.ElementType.Wood);
        CheckTextDiamondValues();
        _gameSession.ChangeDiamondAmountBy(-cost);
        TowerDataManager.IncreaseAndSaveSpeed(Tower.ElementType.Wood);
        // IncreaseAttackSpeedForExistingTowers(woodTower);
        ApplyForExistingTowers(Tower.ElementType.Wood, 3);
        DisplayMessage("Successful purchase", Color.green);
    }
    
    /// <summary>
    /// Upgrades the number of towers that can be placed,
    /// Decrements diamonds by 20
    /// </summary>
    public void IncrementMaximumTowerCount()
    {
        var cost = UpgradeCostManager.GetMaxTowerCost();
        if (!CheckForEnoughDiamonds(cost)) return;
        
        UpgradeCostManager.IncreaseAndSaveTowerCountCost();
        towerCountCostText.text = "+1 Max. Tower Count\n(" + UpgradeCostManager.GetMaxTowerCost() +" Diamonds)";
        _gameSession.ChangeDiamondAmountBy(-cost);
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

        DisplayMessage("Insufficient amount of diamonds", Color.red);
        return false;
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
                        IncreaseDamageForExistingTowers(tower);
                        break;
                    case 2:
                        IncreaseRangeForExistingTowers(tower);
                        break;
                    case 3:
                        IncreaseAttackSpeedForExistingTowers(tower);
                        break;
                }
            }
        }
    }
}
