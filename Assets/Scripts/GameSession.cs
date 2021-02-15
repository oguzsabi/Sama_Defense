using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    [SerializeField] private GameObject _gameSessionCanvas;
    [SerializeField] private GameObject _successCanvas;
    [SerializeField] private TextMeshProUGUI _healthAmountText;
    [SerializeField] private TextMeshProUGUI _diamondAmountText;
    [SerializeField] private TextMeshProUGUI _coinAmountText;
    [SerializeField] private TextMeshProUGUI _towerCountText;
    [SerializeField] private TextMeshProUGUI _levelNumberText;
    [SerializeField] private TextMeshProUGUI _waveNumberText;
    [SerializeField] private bool _isDemoLevel = false;
    
    private const int LEVEL_NUMBER_OFFSET = -3;

    private void Awake()
    {
        CheckDefaultMaxTowerCount();
        _gameSessionCanvas.SetActive(true);
    }

    private void Start()
    {
        if (_isDemoLevel)
        {
            PlayerDataManager.GiveMaxDiamond();
            PlayerDataManager.GiveMaxTowerCount();
            ChangeCoinAmountBy(1000);
        }
        
        // PlayerDataManager.ResetDiamondAmount();
        // PlayerDataManager.ResetMaxTowerCount();
        
        _diamondAmountText.text = PlayerDataManager.GetDiamondAmount().ToString();
        _levelNumberText.text = (SceneLoader.GetCurrentSceneIndex() + LEVEL_NUMBER_OFFSET).ToString();
        _towerCountText.text = 0 + "/" + PlayerDataManager.GetMaximumTowerCount();
    }
    
    /// <summary>
    /// Changes the amount of coin by given amount
    /// </summary>
    /// <param name="amount"></param>
    public void ChangeCoinAmountBy(int amount)
    {
        var oldCoinAmount = int.Parse(_coinAmountText.text);
        var newCoinAmount = oldCoinAmount + amount;

        _coinAmountText.text = newCoinAmount.ToString();
    }
    
    /// <summary>
    /// Changes the amount of diamond by given amount
    /// </summary>
    /// <param name="amount"></param>
    public void ChangeDiamondAmountBy(int amount)
    {
        var oldDiamondAmount = int.Parse(_diamondAmountText.text);
        var newDiamondAmount = oldDiamondAmount + amount;

        _diamondAmountText.text = newDiamondAmount.ToString();
    }
    
    /// <summary>
    /// Increases the placed tower count
    /// </summary>
    public void IncrementTowerCount()
    {
        var towerCounts = _towerCountText.text.Split('/');
        var oldTowerCount = int.Parse(towerCounts[0]);
        var newTowerCount = oldTowerCount + 1;
        var maxTowerCount = int.Parse(towerCounts[1]);

        _towerCountText.text = newTowerCount.ToString() + "/" + maxTowerCount;
    }
    
    /// <summary>
    /// Increases the amount of tower that can be placed
    /// </summary>
    public void ChangeMaxTowerCount()
    {
        var towerCounts = _towerCountText.text.Split('/');
        var oldTowerCount = int.Parse(towerCounts[0]);
        var maxTowerCount = int.Parse(towerCounts[1]) + 1;

        _towerCountText.text = oldTowerCount.ToString() + "/" + maxTowerCount;
    }
    
    /// <summary>
    /// Increases the wave number
    /// </summary>
    public void IncrementWaveNumber()
    {
        var waveNumbers = _waveNumberText.text.Split('/');
        var oldWaveNumber = int.Parse(waveNumbers[0]);
        var newWaveNumber = oldWaveNumber + 1;
        var maxWaveNumber = int.Parse(waveNumbers[1]);

        _waveNumberText.text = newWaveNumber.ToString() + "/" + maxWaveNumber;
    }
    
    /// <summary>
    /// Checks if there are enough coins to make the transaction
    /// </summary>
    /// <param name="cost"></param>
    /// <returns>bool</returns>
    public bool AreThereEnoughCoins(int cost)
    {
        var currentCoinAmount = int.Parse(_coinAmountText.text);
        return currentCoinAmount - cost >= 0;
    }
    
    /// <summary>
    /// Checks if there are enough diamonds to make the transaction
    /// </summary>
    /// <param name="cost"></param>
    /// <returns>bool</returns>
    public bool AreThereEnoughDiamonds(int cost)
    {
        var currentDiamondAmount = int.Parse(_diamondAmountText.text);
        return currentDiamondAmount - cost >= 0;
    }
    
    /// <summary>
    /// Checks if tower placement limit is reached.
    /// </summary>
    /// <returns>bool</returns>
    public bool IsTowerLimitReached()
    {
        var towerCounts = _towerCountText.text.Split('/');
        var oldTowerCount = int.Parse(towerCounts[0]);
        var newTowerCount = oldTowerCount + 1;
        var maxTowerCount = int.Parse(towerCounts[1]);

        return newTowerCount > maxTowerCount;
    }
    
    /// <summary>
    /// Increases the amount of diamond
    /// </summary>
    public void IncrementDiamondAmount()
    {
        var newDiamondAmount = int.Parse(_diamondAmountText.text) + 1;
        _diamondAmountText.text = newDiamondAmount.ToString();
    }
    
    /// <summary>
    /// Saves the diamond amount
    /// </summary>
    public void SaveDiamondAmount()
    {
        PlayerDataManager.SetDiamondAmount(int.Parse(_diamondAmountText.text));
    }

    public void DecreasePlayerHealthBy(int amount)
    {
        var oldHealthAmount = int.Parse(_healthAmountText.text);
        var newHealthAmount = oldHealthAmount - amount;

        _healthAmountText.text = newHealthAmount.ToString();
    }
    
    /// <summary>
    /// Unlocks the next level and loads it
    /// </summary>
    public void LoadNextLevel()
    {
        UnlockNextLevel();
        _successCanvas.SetActive(true);
    }
    
    /// <summary>
    /// Unlocking for next level is handled
    /// </summary>
    private void UnlockNextLevel()
    {
        var currentSceneName = SceneManager.GetActiveScene().name;
        var levelNumber = int.Parse(currentSceneName.Substring(6, currentSceneName.Length - 6)) + 1;
        PlayerDataManager.UnlockLevel(levelNumber);
    }
    
    /// <summary>
    /// Checks the default value of maximum tower placeable tower count
    /// </summary>
    private void CheckDefaultMaxTowerCount()
    {
        PlayerDataManager.SetDefaultMaxTowerCount();
    }
}
