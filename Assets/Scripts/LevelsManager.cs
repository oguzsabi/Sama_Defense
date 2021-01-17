using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelsManager : MonoBehaviour
{
    [SerializeField] private int _numberOfLevels = 10;
    [SerializeField] private GameObject _levelsCanvas;

    private Button[] _levelButtons;
    
    /// <summary>
    /// Level buttons are assigned here to prevent buttons from getting disabled after scene is loaded.
    /// </summary>
    private void Awake()
    {
        _levelButtons = _levelsCanvas.GetComponentsInChildren<Button>();
        ArrangeLevelAvailability();
    }
    
    private void Start()
    {
        // PlayerDataManager.LockAllLevels(_numberOfLevels);
        PlayerDataManager.UnlockLevel(1);
        PlayerDataManager.UnlockLevel(11);
    }
    
    /// <summary>
    /// Prevents clicking to levels that are not unlocked yet.
    /// </summary>
    private void ArrangeLevelAvailability()
    {
        for (var i = 0; i < _numberOfLevels; i++)
        {
            if (!PlayerDataManager.IsLevelUnlocked(i + 1))
            {
                _levelButtons[i].interactable = false;
            }
        }
    }
    
    /// <summary>
    /// Loads the corresponding level
    /// </summary>
    /// <param name="levelNumber"></param>
    public void LoadLevel(int levelNumber)
    {
        SceneLoader.LoadScene("Level " + levelNumber);
    }
    
    /// <summary>
    /// Redirects to main menu
    /// </summary>
    public void GoToMainMenu()
    {
        SceneLoader.LoadScene("Main Menu");
    }
}
