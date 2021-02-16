using UnityEngine;

public class Death : MonoBehaviour
{
    public void GoToMainMenu()
    {
        SceneLoader.LoadScene("Main Menu");
    }
}
