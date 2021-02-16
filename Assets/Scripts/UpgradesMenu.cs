using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradesMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] towerDataTexts;

    private void Awake()
    {
        CheckTowerData();
    }

    private void CheckTowerData()
    {
        towerDataTexts[0].text = "Damage: " + TowerDataManager.GetFireDamage();
        towerDataTexts[1].text = "Accuracy: " + TowerDataManager.GetFireAccuracy();
        towerDataTexts[2].text = "Range: " + TowerDataManager.GetFireRange();
        towerDataTexts[3].text = "Speed: " + TowerDataManager.GetFireSpeed();
        
        towerDataTexts[4].text = "Damage: " + TowerDataManager.GetWaterDamage();
        towerDataTexts[5].text = "Accuracy: " + TowerDataManager.GetWaterAccuracy();
        towerDataTexts[6].text = "Range: " + TowerDataManager.GetWaterRange();
        towerDataTexts[7].text = "Speed: " + TowerDataManager.GetWaterSpeed();
        
        towerDataTexts[8].text = "Damage: " + TowerDataManager.GetEarthDamage();
        towerDataTexts[9].text = "Accuracy: " + TowerDataManager.GetEarthAccuracy();
        towerDataTexts[10].text = "Range: " + TowerDataManager.GetEarthRange();
        towerDataTexts[11].text = "Speed: " + TowerDataManager.GetEarthSpeed();
        
        towerDataTexts[12].text = "Damage: " + TowerDataManager.GetWoodDamage();
        towerDataTexts[13].text = "Accuracy: " + TowerDataManager.GetWoodAccuracy();
        towerDataTexts[14].text = "Range: " + TowerDataManager.GetWoodRange();
        towerDataTexts[15].text = "Speed: " + TowerDataManager.GetWoodSpeed();
    }

    public void GoBack()
    {
        SceneLoader.LoadScene("Main Menu");
    }
}
