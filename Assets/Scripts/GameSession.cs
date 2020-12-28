using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI diamondAmountText;
    [SerializeField] private TextMeshProUGUI coinAmountText;
    [SerializeField] private TextMeshProUGUI towerCountText;
    [SerializeField] private TextMeshProUGUI levelNumberText;
    [SerializeField] private TextMeshProUGUI waveNumberText;

    public void ChangeCoinAmountBy(int amount)
    {
        var oldCoinAmount = int.Parse(coinAmountText.text);
        var newCoinAmount = oldCoinAmount + amount;

        coinAmountText.text = newCoinAmount.ToString();
    }

    public void IncrementTowerCount()
    {
        var towerCounts = towerCountText.text.Split('/');
        var oldTowerCount = int.Parse(towerCounts[0]);
        var newTowerCount = oldTowerCount + 1;
        var maxTowerCount = int.Parse(towerCounts[1]);

        towerCountText.text = newTowerCount.ToString() + "/" + maxTowerCount;
    }

    public void IncrementWaveNumber()
    {
        var waveNumbers = waveNumberText.text.Split('/');
        var oldWaveNumber = int.Parse(waveNumbers[0]);
        var newWaveNumber = oldWaveNumber + 1;
        var maxWaveNumber = int.Parse(waveNumbers[1]);

        waveNumberText.text = newWaveNumber.ToString() + "/" + maxWaveNumber;
    }

    public bool AreThereEnoughCoins(int cost)
    {
        var currentCoinAmount = int.Parse(coinAmountText.text);
        return currentCoinAmount - cost >= 0;
    }

    public bool IsTowerLimitReached()
    {
        var towerCounts = towerCountText.text.Split('/');
        var oldTowerCount = int.Parse(towerCounts[0]);
        var newTowerCount = oldTowerCount + 1;
        var maxTowerCount = int.Parse(towerCounts[1]);

        return newTowerCount > maxTowerCount;
    }
}
