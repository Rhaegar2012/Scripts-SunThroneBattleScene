using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public abstract class Menu<T>:Menu where T:Menu<T>
{
    public static T Instance{get;private set;}
    private void Awake()
    {
        if(Instance!=null)
        {
            Debug.Log($"Menu Instance {Instance.ToString()} already exists");
            Destroy(gameObject);
            return;
        }
        Instance=(T)this;
    }
    public static void Open()
    {
        Debug.Log("MENU: Open called");
        if(MenuManager.Instance!=null && Instance!=null)
        {
            Debug.Log("MENU:Open accessed");
            MenuManager.Instance.OpenMenu(Instance);
        }
    }


}
public abstract class Menu:MonoBehaviour
{
    private int mainLevelIndex=0;
    public void OnQuitPressed()
    {
        Application.Quit();
    }
    public void OnMainMenuPressed()
    {
        LevelLoader.LoadLevel(mainLevelIndex);
        MainMenu.Open();
    }
    public void OnLevelSelectionPressed()
    {
        LevelSelectionMenu.Open();
    }
}
