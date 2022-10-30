using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;


public class LevelLoader : MonoBehaviour
{
    private static EventSystem eventSystem;
    public static LevelLoader Instance{get;private set;}
    private void Awake()
    {
        if(Instance!=null)
        {
            Debug.Log("LevelLoader Instance already exists");
            Destroy(gameObject);
            return;
        }
        Instance=this;
       
        

    }
    public static void LoadLevel(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex);
    }
    public static void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }
  

    public static void ReloadLevel()
    {
        Scene currentScene=SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }

    
}
