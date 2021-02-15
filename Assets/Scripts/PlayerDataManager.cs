using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDataManager : MonoBehaviour
{
    private const string LEVEL_KEY = "level_unlocked_";
    private const string DIAMOND_AMOUNT_KEY = "diamond_amount";
    private const string MAX_TOWER_COUNT_KEY = "max_tower_count";
    
    /// <summary>
    /// Unlocks next level
    /// </summary>
    /// <param name="level"></param>
    public static void UnlockLevel(int level)
    {
        if (level < SceneManager.sceneCountInBuildSettings)
        {
            PlayerPrefs.SetInt(LEVEL_KEY + level.ToString(), 1); // 1 is true
        }
        else
        {
            Debug.LogError("Level index out of range");
        }
    }
    /// <summary>
    /// Checks if the next level is unlocked
    /// </summary>
    /// <param name="level"></param>
    /// <returns></returns>
    public static bool IsLevelUnlocked(int level)
    {
        if (level < SceneManager.sceneCountInBuildSettings)
        {
            var result = PlayerPrefs.GetInt(LEVEL_KEY + level.ToString());
            return result == 1;
        }
        Debug.LogError("Level index out of range");
        return false;
    }
    /// <summary>
    /// Locks all levels
    /// </summary>
    /// <param name="numberOfLevels"></param>
    public static void LockAllLevels(int numberOfLevels)
    {
        for (var i = 0; i < numberOfLevels; i++)
        {
            PlayerPrefs.SetInt(LEVEL_KEY + i, 0); // 1 is true
        }
    }
    /// <summary>
    /// Sets diamond amount to given value
    /// </summary>
    /// <param name="amount"></param>
    public static void SetDiamondAmount(int amount)
    {
        PlayerPrefs.SetInt(DIAMOND_AMOUNT_KEY, amount);
    }
    /// <summary>
    /// Gets diamond amount
    /// </summary>
    /// <returns></returns>
    public static int GetDiamondAmount()
    {
        return PlayerPrefs.GetInt(DIAMOND_AMOUNT_KEY);
    }
    /// <summary>
    /// Resets diamond amount
    /// </summary>
    public static void ResetDiamondAmount()
    {
        SetDiamondAmount(0);
    }
    /// <summary>
    /// Increases the maximum tower that can be placed
    /// </summary>
    public static void IncrementMaximumTowerCount()
    {
        PlayerPrefs.SetInt(MAX_TOWER_COUNT_KEY, PlayerPrefs.GetInt(MAX_TOWER_COUNT_KEY) + 1);
    }
    /// <summary>
    /// Gets the maximum towers that can be placed
    /// </summary>
    /// <returns></returns>
    public static int GetMaximumTowerCount()
    {
        return PlayerPrefs.GetInt(MAX_TOWER_COUNT_KEY);
    }
    /// <summary>
    /// Sets a default value to maximum towers that can be placed
    /// </summary>
    public static void SetDefaultMaxTowerCount()
    {
        if (PlayerPrefs.GetInt(MAX_TOWER_COUNT_KEY) < 5)
        {
            PlayerPrefs.SetInt(MAX_TOWER_COUNT_KEY, 5);
        }
    }

    public static void GiveMaxDiamond()
    {
        SetDiamondAmount(999);
    }
    
    public static void GiveMaxTowerCount()
    {
        PlayerPrefs.SetInt(MAX_TOWER_COUNT_KEY, 10);
    }

    public static void ResetMaxTowerCount()
    {
        PlayerPrefs.SetInt(MAX_TOWER_COUNT_KEY, 5);
    }
}
