using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelsManager : MonoBehaviour
{
    [SerializeField] private int numberOfLevels = 10;
    [SerializeField] private GameObject levelsCanvas;
    [SerializeField] private Color lockedColor;
    [SerializeField] private Color unlockedColor;

    private Button[] levelButtons;

    // Start is called before the first frame update
    private void Start()
    {
        // PlayerDataManager.LockAllLevels(numberOfLevels);
        PlayerDataManager.UnlockLevel(1);
        levelButtons = levelsCanvas.GetComponentsInChildren<Button>();
        ArrangeLevelAvailability();
    }

    private void ArrangeLevelAvailability()
    {
        for (var i = 0; i < 10; i++)
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
