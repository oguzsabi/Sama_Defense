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
    /// <summary>
    /// Pauses the game by setting time scale to 0
    /// </summary>
    private void PauseGame()
    {
        Time.timeScale = 0;
        gameSessionCanvas.SetActive(false);
        pauseMenuCanvas.SetActive(true);
    }
    
    /// <summary>
    /// Resumes the game by setting time scale to 1
    /// </summary>
    public void UnpauseGame()
    {
        Time.timeScale = 1;
        gameSessionCanvas.SetActive(true);
        pauseMenuCanvas.SetActive(false);
    }
    
    /// <summary>
    /// Redirects to the main menu 
    /// </summary>
    public void GoToMainMenu()
    {
        SceneLoader.LoadScene("Main Menu");
        Time.timeScale = 1;
    }
    /// <summary>
    /// Redirects to the upgrades menu 
    /// </summary>
    public void GoToUpgrades()
    {
        pauseMenuCanvas.SetActive(false);
        upgradesMenuCanvas.SetActive(true);
    }
    /// <summary>
    /// Redirects to pause menu
    /// </summary>
    public void GoBackToPauseMenu()
    {
        upgradesMenuCanvas.SetActive(false);
        pauseMenuCanvas.SetActive(true);
    }
}
