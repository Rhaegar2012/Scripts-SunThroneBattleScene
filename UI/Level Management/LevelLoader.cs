using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    
    public static void LoadLevel(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex);
    }

    public static void ReloadLevel()
    {
        Scene currentScene=SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);

    }
}