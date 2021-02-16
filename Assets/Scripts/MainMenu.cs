using UnityEngine;

public class MainMenu : MonoBehaviour
{
    /// <summary>
    /// Starts Level 1
    /// </summary>
    public void PlayButtonClicked()
    {
        SceneLoader.LoadScene("Level 1");
    }
    /// <summary>
    /// Redirects to levels screen
    /// </summary>
    public void GoToLevelsMenu()
    {
        SceneLoader.LoadScene("Levels");
    }

    public void GoToUpgradesMenu()
    {
        SceneLoader.LoadScene("Upgrades");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
