using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private GameSession _gameSession;

    private void Start()
    {
        _gameSession = GameObject.Find("GameSession").GetComponent<GameSession>();
    }
    
    /// <summary>
    /// Starts the coroutine for loading next level
    /// </summary>
    public void LoadNextLevel()
    {
        _gameSession.SaveDiamondAmount();
        SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex + 1) % SceneManager.sceneCountInBuildSettings);
        // StartCoroutine(LoadLevel());
    }
    
    // /// <summary>
    // /// Loads next level
    // /// </summary>
    // /// <returns></returns>
    // private IEnumerator LoadLevel()
    // {
    //     _gameSession.SaveDiamondAmount();
    //     yield return new WaitForSeconds(delayTimeInSeconds);
    //     SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex + 1) % SceneManager.sceneCountInBuildSettings);
    // }
    /// <summary>
    /// Gets current scene index
    /// </summary>
    /// <returns></returns>
    public static int GetCurrentSceneIndex()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }
    /// <summary>
    /// Loads a scene
    /// </summary>
    /// <param name="sceneName"></param>
    public static void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
