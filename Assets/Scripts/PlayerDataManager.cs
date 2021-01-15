using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDataManager : MonoBehaviour
{
    private const string MASTER_VOLUME_KEY = "master_volume";
    private const string LEVEL_KEY = "level_unlocked_";
    private const string DIAMOND_AMOUNT_KEY = "diamond_amount";
    private const string MAX_TOWER_COUNT_KEY = "max_tower_count";
    
    public static void SetMasterVolume(float volume)
    {
        if (volume >= 0f && volume <= 1f)
        {
            PlayerPrefs.SetFloat(MASTER_VOLUME_KEY, volume);
        }
        else
        {
            Debug.LogError("Master volume out of range");
        }
    }

    public static float GetMasterVolume()
    {
        return PlayerPrefs.GetFloat(MASTER_VOLUME_KEY);
    }
    
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

    public static void LockAllLevels(int numberOfLevels)
    {
        for (var i = 0; i < numberOfLevels; i++)
        {
            PlayerPrefs.SetInt(LEVEL_KEY + i, 0); // 1 is true
        }
    }

    public static void SetDiamondAmount(int amount)
    {
        PlayerPrefs.SetInt(DIAMOND_AMOUNT_KEY, amount);
    }

    public static int GetDiamondAmount()
    {
        return PlayerPrefs.GetInt(DIAMOND_AMOUNT_KEY);
    }

    public static void ResetDiamondAmount()
    {
        SetDiamondAmount(0);
    }

    public static void IncrementMaximumTowerCount()
    {
        PlayerPrefs.SetInt(MAX_TOWER_COUNT_KEY, PlayerPrefs.GetInt(MAX_TOWER_COUNT_KEY) + 1);
    }

    public static int GetMaximumTowerCount()
    {
        return PlayerPrefs.GetInt(MAX_TOWER_COUNT_KEY);
    }

    public static void SetDefaultMaxTowerCount()
    {
        if (PlayerPrefs.GetInt(MAX_TOWER_COUNT_KEY) < 5)
        {
            PlayerPrefs.SetInt(MAX_TOWER_COUNT_KEY, 5);
        }
    }
}
