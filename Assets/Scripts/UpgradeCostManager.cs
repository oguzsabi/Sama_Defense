using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeCostManager : MonoBehaviour
{
    private const int costIncreaseAmount = 5;
    private const int towerCountCostIncreaseAmount = 20;

    private const string FIRE_DAMAGE_KEY = "fire_damage_cost";
    private const string FIRE_ACCURACY_KEY = "fire_accuracy_cost";
    private const string FIRE_RANGE_KEY = "fire_range_cost";
    private const string FIRE_SPEED_KEY = "fire_speed_cost";
    
    private const string WATER_DAMAGE_KEY = "water_damage_cost";
    private const string WATER_ACCURACY_KEY = "water_accuracy_cost";
    private const string WATER_RANGE_KEY = "water_range_cost";
    private const string WATER_SPEED_KEY = "water_speed_cost";
    
    private const string EARTH_DAMAGE_KEY = "earth_damage_cost";
    private const string EARTH_ACCURACY_KEY = "earth_accuracy_cost";
    private const string EARTH_RANGE_KEY = "earth_range_cost";
    private const string EARTH_SPEED_KEY = "earth_speed_cost";
    
    private const string WOOD_DAMAGE_KEY = "wood_damage_cost";
    private const string WOOD_ACCURACY_KEY = "wood_accuracy_cost";
    private const string WOOD_RANGE_KEY = "wood_range_cost";
    private const string WOOD_SPEED_KEY = "wood_speed_cost";

    private const string TOWER_COUNT_KEY = "tower_count_cost";

    
    public static void IncreaseAndSaveDamageCost(Tower.ElementType elementType)
    {
        int oldCost;
        
        switch (elementType)
        {
            case Tower.ElementType.Fire:
                oldCost = PlayerPrefs.GetInt(FIRE_DAMAGE_KEY, 5);
                PlayerPrefs.SetInt(FIRE_DAMAGE_KEY, oldCost + costIncreaseAmount);
                break;
            case Tower.ElementType.Water:
                oldCost = PlayerPrefs.GetInt(WATER_DAMAGE_KEY, 5);
                PlayerPrefs.SetInt(WATER_DAMAGE_KEY, oldCost + costIncreaseAmount);
                break;
            case Tower.ElementType.Earth:
                oldCost = PlayerPrefs.GetInt(EARTH_DAMAGE_KEY, 5);
                PlayerPrefs.SetInt(EARTH_DAMAGE_KEY, oldCost + costIncreaseAmount);
                break;
            case Tower.ElementType.Wood:
                oldCost = PlayerPrefs.GetInt(WOOD_DAMAGE_KEY, 5);
                PlayerPrefs.SetInt(WOOD_DAMAGE_KEY, oldCost + costIncreaseAmount);
                break;
        }
    }
    
    public static void IncreaseAndSaveAccuracyCost(Tower.ElementType elementType)
    {
        int oldCost;
        
        switch (elementType)
        {
            case Tower.ElementType.Fire:
                oldCost = PlayerPrefs.GetInt(FIRE_ACCURACY_KEY, 5);
                PlayerPrefs.SetInt(FIRE_ACCURACY_KEY, oldCost + costIncreaseAmount);
                break;
            case Tower.ElementType.Water:
                oldCost = PlayerPrefs.GetInt(WATER_ACCURACY_KEY, 5);
                PlayerPrefs.SetInt(WATER_ACCURACY_KEY, oldCost + costIncreaseAmount);
                break;
            case Tower.ElementType.Earth:
                oldCost = PlayerPrefs.GetInt(EARTH_ACCURACY_KEY, 5);
                PlayerPrefs.SetInt(EARTH_ACCURACY_KEY, oldCost + costIncreaseAmount);
                break;
            case Tower.ElementType.Wood:
                oldCost = PlayerPrefs.GetInt(WOOD_ACCURACY_KEY, 5);
                PlayerPrefs.SetInt(WOOD_ACCURACY_KEY, oldCost + costIncreaseAmount);
                break;
        }
    }
    
    public static void IncreaseAndSaveRangeCost(Tower.ElementType elementType)
    {
        int oldCost;
        
        switch (elementType)
        {
            case Tower.ElementType.Fire:
                oldCost = PlayerPrefs.GetInt(FIRE_RANGE_KEY, 5);
                PlayerPrefs.SetInt(FIRE_RANGE_KEY, oldCost + costIncreaseAmount);
                break;
            case Tower.ElementType.Water:
                oldCost = PlayerPrefs.GetInt(WATER_RANGE_KEY, 5);
                PlayerPrefs.SetInt(WATER_RANGE_KEY, oldCost + costIncreaseAmount);
                break;
            case Tower.ElementType.Earth:
                oldCost = PlayerPrefs.GetInt(EARTH_RANGE_KEY, 5);
                PlayerPrefs.SetInt(EARTH_RANGE_KEY, oldCost + costIncreaseAmount);
                break;
            case Tower.ElementType.Wood:
                oldCost = PlayerPrefs.GetInt(WOOD_RANGE_KEY, 5);
                PlayerPrefs.SetInt(WOOD_RANGE_KEY, oldCost + costIncreaseAmount);
                break;
        }
    }
    
    public static void IncreaseAndSaveSpeedCost(Tower.ElementType elementType)
    {
        int oldCost;
        
        switch (elementType)
        {
            case Tower.ElementType.Fire:
                oldCost = PlayerPrefs.GetInt(FIRE_SPEED_KEY, 5);
                PlayerPrefs.SetInt(FIRE_SPEED_KEY, oldCost + costIncreaseAmount);
                break;
            case Tower.ElementType.Water:
                oldCost = PlayerPrefs.GetInt(WATER_SPEED_KEY, 5);
                PlayerPrefs.SetInt(WATER_SPEED_KEY, oldCost + costIncreaseAmount);
                break;
            case Tower.ElementType.Earth:
                oldCost = PlayerPrefs.GetInt(EARTH_SPEED_KEY, 5);
                PlayerPrefs.SetInt(EARTH_SPEED_KEY, oldCost + costIncreaseAmount);
                break;
            case Tower.ElementType.Wood:
                oldCost = PlayerPrefs.GetInt(WOOD_SPEED_KEY, 5);
                PlayerPrefs.SetInt(WOOD_SPEED_KEY, oldCost + costIncreaseAmount);
                break;
        }
    }

    public static void IncreaseAndSaveTowerCountCost()
    {
        var oldCost = PlayerPrefs.GetInt(TOWER_COUNT_KEY, 20);
        PlayerPrefs.SetInt(TOWER_COUNT_KEY, oldCost + towerCountCostIncreaseAmount);
    }

    public static int[] GetFireCosts()
    {
        return new[]
        {
            PlayerPrefs.GetInt(FIRE_DAMAGE_KEY, 5), PlayerPrefs.GetInt(FIRE_ACCURACY_KEY, 5),
            PlayerPrefs.GetInt(FIRE_RANGE_KEY, 5), PlayerPrefs.GetInt(FIRE_SPEED_KEY, 5)
        };
    }
    
    public static int[] GetWaterCosts()
    {
        return new[]
        {
            PlayerPrefs.GetInt(WATER_DAMAGE_KEY, 5), PlayerPrefs.GetInt(WATER_ACCURACY_KEY, 5),
            PlayerPrefs.GetInt(WATER_RANGE_KEY, 5), PlayerPrefs.GetInt(WATER_SPEED_KEY, 5)
        };
    }
    
    public static int[] GetEarthCosts()
    {
        return new[]
        {
            PlayerPrefs.GetInt(EARTH_DAMAGE_KEY, 5), PlayerPrefs.GetInt(EARTH_ACCURACY_KEY, 5),
            PlayerPrefs.GetInt(EARTH_RANGE_KEY, 5), PlayerPrefs.GetInt(EARTH_SPEED_KEY, 5)
        };
    }
    
    public static int[] GetWoodCosts()
    {
        return new[]
        {
            PlayerPrefs.GetInt(WOOD_DAMAGE_KEY, 5), PlayerPrefs.GetInt(WOOD_ACCURACY_KEY, 5),
            PlayerPrefs.GetInt(WOOD_RANGE_KEY, 5), PlayerPrefs.GetInt(WOOD_SPEED_KEY, 5)
        };
    }

    public static int GetMaxTowerCost()
    {
        return PlayerPrefs.GetInt(TOWER_COUNT_KEY, 20);
    }

    public static void ResetAllCosts()
    {
        PlayerPrefs.SetInt(FIRE_DAMAGE_KEY, 5);
        PlayerPrefs.SetInt(WATER_DAMAGE_KEY, 5);
        PlayerPrefs.SetInt(EARTH_DAMAGE_KEY, 5);
        PlayerPrefs.SetInt(WOOD_DAMAGE_KEY, 5);
        
        PlayerPrefs.SetInt(FIRE_ACCURACY_KEY, 5);
        PlayerPrefs.SetInt(WATER_ACCURACY_KEY, 5);
        PlayerPrefs.SetInt(EARTH_ACCURACY_KEY, 5);
        PlayerPrefs.SetInt(WOOD_ACCURACY_KEY, 5);
        
        PlayerPrefs.SetInt(FIRE_RANGE_KEY, 5);
        PlayerPrefs.SetInt(WATER_RANGE_KEY, 5);
        PlayerPrefs.SetInt(EARTH_RANGE_KEY, 5);
        PlayerPrefs.SetInt(WOOD_RANGE_KEY, 5);
        
        PlayerPrefs.SetInt(FIRE_SPEED_KEY, 5);
        PlayerPrefs.SetInt(WATER_SPEED_KEY, 5);
        PlayerPrefs.SetInt(EARTH_SPEED_KEY, 5);
        PlayerPrefs.SetInt(WOOD_SPEED_KEY, 5);
        
        PlayerPrefs.SetInt(TOWER_COUNT_KEY, 20);
    }
}
