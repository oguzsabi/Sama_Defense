using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NextLevelCountdown : MonoBehaviour
{
    [SerializeField] private SceneLoader _sceneLoader;
    [SerializeField] private TextMeshProUGUI countdownText;

    private int countdown;
    private float oneSecondCounter;

    private void Start()
    {
        countdown = 5;
    }

    // Update is called once per frame
    private void Update()
    {
        oneSecondCounter += Time.deltaTime;
        
        if (oneSecondCounter < 1) return;
        
        oneSecondCounter = 0;
        countdown--;
        countdownText.text = countdown.ToString();

        if (countdown <= 0)
        {
            _sceneLoader.LoadNextLevel();
        }
    }
}
