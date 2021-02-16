using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerDataManager : MonoBehaviour
{
    private const string FIRE_DAMAGE_KEY = "fire_damage";
    private const string FIRE_ACCURACY_KEY = "fire_accuracy";
    private const string FIRE_RANGE_KEY = "fire_range";
    private const string FIRE_RANGE_VISUAL_KEY = "fire_range_visual";
    private const string FIRE_SPEED_KEY = "fire_speed";
    
    private const string WATER_DAMAGE_KEY = "water_damage";
    private const string WATER_ACCURACY_KEY = "water_accuracy";
    private const string WATER_RANGE_KEY = "water_range";
    private const string WATER_RANGE_VISUAL_KEY = "water_range_visual";
    private const string WATER_SPEED_KEY = "water_speed";
    
    private const string EARTH_DAMAGE_KEY = "earth_damage";
    private const string EARTH_ACCURACY_KEY = "earth_accuracy";
    private const string EARTH_RANGE_KEY = "earth_range";
    private const string EARTH_RANGE_VISUAL_KEY = "earth_range_visual";
    private const string EARTH_SPEED_KEY = "earth_speed";
    
    private const string WOOD_DAMAGE_KEY = "wood_damage";
    private const string WOOD_ACCURACY_KEY = "wood_accuracy";
    private const string WOOD_RANGE_KEY = "wood_range";
    private const string WOOD_RANGE_VISUAL_KEY = "wood_range_visual";
    private const string WOOD_SPEED_KEY = "wood_speed";

    private const int damageIncreaseAmount = 5;
    private const int accuracyIncreaseAmount = 1;
    private const float rangeIncreaseAmount = 0.1f;
    private const float rangeVisualIncreaseAmount = 0.735f;
    private const float speedIncreaseAmount = 0.1f;


    public static void IncreaseAndSaveDamage(Tower.ElementType elementType)
    {
        int oldDamage;
        
        switch (elementType)
        {
            case Tower.ElementType.Fire:
                oldDamage = PlayerPrefs.GetInt(FIRE_DAMAGE_KEY, 25);
                PlayerPrefs.SetInt(FIRE_DAMAGE_KEY, oldDamage + damageIncreaseAmount);
                break;
            case Tower.ElementType.Water:
                oldDamage = PlayerPrefs.GetInt(WATER_DAMAGE_KEY, 25);
                PlayerPrefs.SetInt(WATER_DAMAGE_KEY, oldDamage + damageIncreaseAmount);
                break;
            case Tower.ElementType.Earth:
                oldDamage = PlayerPrefs.GetInt(EARTH_DAMAGE_KEY, 25);
                PlayerPrefs.SetInt(EARTH_DAMAGE_KEY, oldDamage + damageIncreaseAmount);
                break;
            case Tower.ElementType.Wood:
                oldDamage = PlayerPrefs.GetInt(WOOD_DAMAGE_KEY, 25);
                PlayerPrefs.SetInt(WOOD_DAMAGE_KEY, oldDamage + damageIncreaseAmount);
                break;
        }
    }
    
    public static void IncreaseAndSaveAccuracy(Tower.ElementType elementType)
    {
        int oldAccuracy;
        
        switch (elementType)
        {
            case Tower.ElementType.Fire:
                oldAccuracy = PlayerPrefs.GetInt(FIRE_ACCURACY_KEY, 90);
                PlayerPrefs.SetInt(FIRE_ACCURACY_KEY, oldAccuracy + accuracyIncreaseAmount);
                break;
            case Tower.ElementType.Water:
                oldAccuracy = PlayerPrefs.GetInt(WATER_ACCURACY_KEY, 90);
                PlayerPrefs.SetInt(WATER_ACCURACY_KEY, oldAccuracy + accuracyIncreaseAmount);
                break;
            case Tower.ElementType.Earth:
                oldAccuracy = PlayerPrefs.GetInt(EARTH_ACCURACY_KEY, 90);
                PlayerPrefs.SetInt(EARTH_ACCURACY_KEY, oldAccuracy + accuracyIncreaseAmount);
                break;
            case Tower.ElementType.Wood:
                oldAccuracy = PlayerPrefs.GetInt(WOOD_ACCURACY_KEY, 90);
                PlayerPrefs.SetInt(WOOD_ACCURACY_KEY, oldAccuracy + accuracyIncreaseAmount);
                break;
        }
    }
    
    public static void IncreaseAndSaveRange(Tower.ElementType elementType)
    {
        float oldRange;
        
        switch (elementType)
        {
            case Tower.ElementType.Fire:
                oldRange = PlayerPrefs.GetFloat(FIRE_RANGE_KEY, 2.15f);
                PlayerPrefs.SetFloat(FIRE_RANGE_KEY, oldRange + rangeIncreaseAmount);
                break;
            case Tower.ElementType.Water:
                oldRange = PlayerPrefs.GetFloat(WATER_RANGE_KEY, 2.15f);
                PlayerPrefs.SetFloat(WATER_RANGE_KEY, oldRange + rangeIncreaseAmount);
                break;
            case Tower.ElementType.Earth:
                oldRange = PlayerPrefs.GetFloat(EARTH_RANGE_KEY, 2.15f);
                PlayerPrefs.SetFloat(EARTH_RANGE_KEY, oldRange + rangeIncreaseAmount);
                break;
            case Tower.ElementType.Wood:
                oldRange = PlayerPrefs.GetFloat(WOOD_RANGE_KEY, 2.15f);
                PlayerPrefs.SetFloat(WOOD_RANGE_KEY, oldRange + rangeIncreaseAmount);
                break;
        }
    }
    
    public static void IncreaseAndSaveRangeVisual(Tower.ElementType elementType)
    {
        float oldRangeVisual;
        
        switch (elementType)
        {
            case Tower.ElementType.Fire:
                oldRangeVisual = PlayerPrefs.GetFloat(FIRE_RANGE_VISUAL_KEY, 15.8f);
                PlayerPrefs.SetFloat(FIRE_RANGE_VISUAL_KEY, oldRangeVisual + rangeVisualIncreaseAmount);
                break;
            case Tower.ElementType.Water:
                oldRangeVisual = PlayerPrefs.GetFloat(WATER_RANGE_VISUAL_KEY, 15.8f);
                PlayerPrefs.SetFloat(WATER_RANGE_VISUAL_KEY, oldRangeVisual + rangeVisualIncreaseAmount);
                break;
            case Tower.ElementType.Earth:
                oldRangeVisual = PlayerPrefs.GetFloat(EARTH_RANGE_VISUAL_KEY, 15.8f);
                PlayerPrefs.SetFloat(EARTH_RANGE_VISUAL_KEY, oldRangeVisual + rangeVisualIncreaseAmount);
                break;
            case Tower.ElementType.Wood:
                oldRangeVisual = PlayerPrefs.GetFloat(WOOD_RANGE_VISUAL_KEY, 15.8f);
                PlayerPrefs.SetFloat(WOOD_RANGE_VISUAL_KEY, oldRangeVisual + rangeVisualIncreaseAmount);
                break;
        }
    }
    
    public static void IncreaseAndSaveSpeed(Tower.ElementType elementType)
    {
        float oldSpeed;
        
        switch (elementType)
        {
            case Tower.ElementType.Fire:
                oldSpeed = PlayerPrefs.GetFloat(FIRE_SPEED_KEY, 0.375f);
                PlayerPrefs.SetFloat(FIRE_SPEED_KEY, oldSpeed + speedIncreaseAmount);
                break;
            case Tower.ElementType.Water:
                oldSpeed = PlayerPrefs.GetFloat(WATER_SPEED_KEY, 0.375f);
                PlayerPrefs.SetFloat(WATER_SPEED_KEY, oldSpeed + speedIncreaseAmount);
                break;
            case Tower.ElementType.Earth:
                oldSpeed = PlayerPrefs.GetFloat(EARTH_SPEED_KEY, 0.375f);
                PlayerPrefs.SetFloat(EARTH_SPEED_KEY, oldSpeed + speedIncreaseAmount);
                break;
            case Tower.ElementType.Wood:
                oldSpeed = PlayerPrefs.GetFloat(WOOD_SPEED_KEY, 0.375f);
                PlayerPrefs.SetFloat(WOOD_SPEED_KEY, oldSpeed + speedIncreaseAmount);
                break;
        }
    }

    public static int GetFireDamage()
    {
        return PlayerPrefs.GetInt(FIRE_DAMAGE_KEY, 25);
    }
    
    public static int GetWaterDamage()
    {
        return PlayerPrefs.GetInt(WATER_DAMAGE_KEY, 25);
    }
    
    public static int GetEarthDamage()
    {
        return PlayerPrefs.GetInt(EARTH_DAMAGE_KEY, 25);
    }
    
    public static int GetWoodDamage()
    {
        return PlayerPrefs.GetInt(WOOD_DAMAGE_KEY, 25);
    }
    
    public static int GetFireAccuracy()
    {
        return PlayerPrefs.GetInt(FIRE_ACCURACY_KEY, 90);
    }
    
    public static int GetWaterAccuracy()
    {
        return PlayerPrefs.GetInt(WATER_ACCURACY_KEY, 90);
    }
    
    public static int GetEarthAccuracy()
    {
        return PlayerPrefs.GetInt(EARTH_ACCURACY_KEY, 90);
    }
    
    public static int GetWoodAccuracy()
    {
        return PlayerPrefs.GetInt(WOOD_ACCURACY_KEY, 90);
    }
    
    public static float GetFireRange()
    {
        return PlayerPrefs.GetFloat(FIRE_RANGE_KEY, 2.15f);
    }
    
    public static float GetWaterRange()
    {
        return PlayerPrefs.GetFloat(WATER_RANGE_KEY, 2.15f);
    }
    
    public static float GetEarthRange()
    {
        return PlayerPrefs.GetFloat(EARTH_RANGE_KEY, 2.15f);
    }
    
    public static float GetWoodRange()
    {
        return PlayerPrefs.GetFloat(WOOD_RANGE_KEY, 2.15f);
    }
    
    public static float GetFireRangeVisual()
    {
        return PlayerPrefs.GetFloat(FIRE_RANGE_VISUAL_KEY, 15.8f);
    }
    
    public static float GetWaterRangeVisual()
    {
        return PlayerPrefs.GetFloat(WATER_RANGE_VISUAL_KEY, 15.8f);
    }
    
    public static float GetEarthRangeVisual()
    {
        return PlayerPrefs.GetFloat(EARTH_RANGE_VISUAL_KEY, 15.8f);
    }
    
    public static float GetWoodRangeVisual()
    {
        return PlayerPrefs.GetFloat(WOOD_RANGE_VISUAL_KEY, 15.8f);
    }
    
    public static float GetFireSpeed()
    {
        return PlayerPrefs.GetFloat(FIRE_SPEED_KEY, 0.375f);
    }
    
    public static float GetWaterSpeed()
    {
        return PlayerPrefs.GetFloat(WATER_SPEED_KEY, 0.375f);
    }
    
    public static float GetEarthSpeed()
    {
        return PlayerPrefs.GetFloat(EARTH_SPEED_KEY, 0.375f);
    }
    
    public static float GetWoodSpeed()
    {
        return PlayerPrefs.GetFloat(WOOD_SPEED_KEY, 0.375f);
    }
    
    public static void ResetAllData()
    {
        PlayerPrefs.SetInt(FIRE_DAMAGE_KEY, 25);
        PlayerPrefs.SetInt(WATER_DAMAGE_KEY, 25);
        PlayerPrefs.SetInt(EARTH_DAMAGE_KEY, 25);
        PlayerPrefs.SetInt(WOOD_DAMAGE_KEY, 25);
        
        PlayerPrefs.SetInt(FIRE_ACCURACY_KEY, 90);
        PlayerPrefs.SetInt(WATER_ACCURACY_KEY, 90);
        PlayerPrefs.SetInt(EARTH_ACCURACY_KEY, 90);
        PlayerPrefs.SetInt(WOOD_ACCURACY_KEY, 90);
        
        PlayerPrefs.SetFloat(FIRE_RANGE_KEY, 2.15f);
        PlayerPrefs.SetFloat(WATER_RANGE_KEY, 2.15f);
        PlayerPrefs.SetFloat(EARTH_RANGE_KEY, 2.15f);
        PlayerPrefs.SetFloat(WOOD_RANGE_KEY, 2.15f);
        
        PlayerPrefs.SetFloat(FIRE_RANGE_VISUAL_KEY, 15.8f);
        PlayerPrefs.SetFloat(WATER_RANGE_VISUAL_KEY, 15.8f);
        PlayerPrefs.SetFloat(EARTH_RANGE_VISUAL_KEY, 15.8f);
        PlayerPrefs.SetFloat(WOOD_RANGE_VISUAL_KEY, 15.8f);
        
        PlayerPrefs.SetFloat(FIRE_SPEED_KEY, 0.375f);
        PlayerPrefs.SetFloat(WATER_SPEED_KEY, 0.375f);
        PlayerPrefs.SetFloat(EARTH_SPEED_KEY, 0.375f);
        PlayerPrefs.SetFloat(WOOD_SPEED_KEY, 0.375f);
    }
}
