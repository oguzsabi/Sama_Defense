using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    /// <summary>
    /// Starts Level 1
    /// </summary>
    public void PlayButtonClicked()
    {
        SceneLoader.LoadScene("Level 11");
    }
    /// <summary>
    /// Redirects to levels screen
    /// </summary>
    public void GoToLevelsMenu()
    {
        SceneLoader.LoadScene("Levels");
    }
}
