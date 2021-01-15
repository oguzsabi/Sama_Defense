using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelsManager : MonoBehaviour
{
    [SerializeField] private int numberOfLevels = 10;
    [SerializeField] private GameObject levelsCanvas;

    private Button[] levelButtons;

    private void Awake()
    {
        // this is done here to prevent buttons from getting disabled after scene is loaded.
        levelButtons = levelsCanvas.GetComponentsInChildren<Button>();
        ArrangeLevelAvailability();
    }

    // Start is called before the first frame update
    private void Start()
    {
        // PlayerDataManager.LockAllLevels(numberOfLevels);
        PlayerDataManager.UnlockLevel(1);
    }

    private void ArrangeLevelAvailability()
    {
        for (var i = 0; i < numberOfLevels; i++)
        {
            if (!PlayerDataManager.IsLevelUnlocked(i + 1))
            {
                levelButtons[i].interactable = false;
            }
        }
    }

    public void LoadLevel(int levelNumber)
    {
        SceneLoader.LoadScene("Level " + levelNumber);
    }

    public void GoToMainMenu()
    {
        SceneLoader.LoadScene("Main Menu");
    }
}
