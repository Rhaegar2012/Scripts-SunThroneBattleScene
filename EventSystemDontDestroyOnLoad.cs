using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class EventSystemDontDestroyOnLoad : MonoBehaviour
{
    private EventSystem eventSystem;
    private UnitActionMenu unitActionMenu;
    private GameObject defaultPlayButton;
    private void Awake()
    {
        EventSystem[] eventSystemList=FindObjectsOfType<EventSystem>();
        if(eventSystemList.Length>1)
        {
            for(int i=1;i<eventSystemList.Length;i++)
            {
                Destroy(eventSystemList[i].gameObject);
            }
            return;
        }
        UnityEngine.Object.DontDestroyOnLoad(gameObject);
        eventSystem=EventSystem.current;
        SceneManager.sceneLoaded+=OnSceneLoaded;

        
    }

    private void Start()
    {
        LevelSelectionMenu.Instance.OnPlayLevelCalled+=LevelSelectionMenu_OnPlayLevelCalled;
        Menu.MenuCalled+=Menu_MenuCalled;
        SetFirstSelectedButton();
    }

    private void SetFirstSelectedButton()
    {
        MainMenu.Instance.SetFirstSelectedButton();
        eventSystem.firstSelectedGameObject=MainMenu.Instance.GetFirstSelectedButton();

    }

    private void UpdateSelectedButton(GameObject selectedButton)
    {
        eventSystem.SetSelectedGameObject(selectedButton);
    }

    public void LevelSelectionMenu_OnPlayLevelCalled(object sender, EventArgs empty)
    {
        /*unitActionMenu=FindObjectOfType<MenuManager>();
        if(unitActionMenu==null)
        {
            Debug.Log("Action Menu is null");
        }
        else
        {
            Debug.Log("Object found");
        }
        //defaultPlayButton=unitActionMenu.GetDefaultButton().gameObject;
        //eventSystem.SetSelectedGameObject(defaultPlayButton);*/

    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Scene currentScene= SceneManager.GetActiveScene();
        Debug.Log($"CurrentScene is {currentScene.name}");
        unitActionMenu=FindObjectOfType<UnitActionMenu>();
        defaultPlayButton=unitActionMenu.GetDefaultButton().gameObject;
        eventSystem.SetSelectedGameObject(defaultPlayButton);
    }

   
    public void Menu_MenuCalled(object sender, Menu menuInstance)
    {
        UpdateSelectedButton(menuInstance.GetFirstSelectedButton());
    }

}


