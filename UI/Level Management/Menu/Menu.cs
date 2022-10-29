using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public abstract class Menu<T>:Menu where T:Menu<T>
{
    public static T Instance{get;private set;}
    
    protected virtual void Awake()
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

        if(MenuManager.Instance!=null && Instance!=null)
        {
            MenuManager.Instance.OpenMenu(Instance);
        }
    }


}
public abstract class Menu:MonoBehaviour
{
    private int mainLevelIndex=0;
    protected GameObject firstSelectedButton;
    public static event EventHandler<Menu> MenuCalled;
    public void OnQuitPressed()
    {
        Application.Quit();
    }
    public void OnMainMenuPressed()
    {
        MainMenu.Open();
        MainMenu.Instance.SetFirstSelectedButton();
        OnMenuCalled(MainMenu.Instance);
        //LevelLoader.LoadLevel(mainLevelIndex);
        
    }
    protected virtual void OnMenuCalled(Menu menuInstance)
    {
        MenuCalled?.Invoke(this,menuInstance);
    }
    public void OnLevelSelectionPressed()
    {
        LevelSelectionMenu.Open();
    }
    public  virtual void SubscribeToEvents()
    {
        //Placeholder method for event subscription

    }
    public GameObject GetFirstSelectedButton()
    {
        return firstSelectedButton;
    }
}

