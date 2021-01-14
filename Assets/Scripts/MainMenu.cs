using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void PlayButtonClicked()
    {
        SceneLoader.LoadScene("Level 1");
    }

    public void GoToLevelsMenu()
    {
        SceneLoader.LoadScene("Levels");
    }
}
