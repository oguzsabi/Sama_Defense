using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private float delayTimeInSeconds = 5f;

    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel());
    }

    private IEnumerator LoadLevel()
    {
        yield return new WaitForSeconds(delayTimeInSeconds);
        SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex + 1) % SceneManager.sceneCountInBuildSettings);
    }
}
