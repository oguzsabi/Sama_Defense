using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePause : MonoBehaviour
{
    [SerializeField] private GameObject gameSessionCanvas;
    [SerializeField] private GameObject pauseMenuCanvas;
    [SerializeField] private GameObject upgradesMenuCanvas;
    private bool isGamePaused = false;

    private void Awake()
    {
        pauseMenuCanvas.SetActive(false);
        upgradesMenuCanvas.SetActive(false);
    }

    // Update is called once per frame
    private void Update()
    {
        if (!Input.GetKeyDown(KeyCode.Escape)) return;
        isGamePaused = !isGamePaused;
        
        if (upgradesMenuCanvas.activeSelf)
        {
            GoBackToPauseMenu();
            return;
        }

        if (isGamePaused)
        {
            PauseGame();
        }
        else
        {
            UnpauseGame();
        }
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
        gameSessionCanvas.SetActive(false);
        pauseMenuCanvas.SetActive(true);
    }

    public void UnpauseGame()
    {
        Time.timeScale = 1;
        gameSessionCanvas.SetActive(true);
        pauseMenuCanvas.SetActive(false);
    }

    public void GoToMainMenu()
    {
        LevelLoader.LoadScene("Main Menu");
        Time.timeScale = 1;
    }

    public void GoToUpgrades()
    {
        pauseMenuCanvas.SetActive(false);
        upgradesMenuCanvas.SetActive(true);
    }

    public void GoBackToPauseMenu()
    {
        upgradesMenuCanvas.SetActive(false);
        pauseMenuCanvas.SetActive(true);
    }
}
