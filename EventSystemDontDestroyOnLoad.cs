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

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Scene currentScene= SceneManager.GetActiveScene();
        unitActionMenu=FindObjectOfType<UnitActionMenu>();
        if(unitActionMenu!=null)
        {
            defaultPlayButton=unitActionMenu.GetDefaultButton().gameObject;
            UpdateSelectedButton(defaultPlayButton);
        }
        
    }

   
    public void Menu_MenuCalled(object sender, Menu menuInstance)
    {
        UpdateSelectedButton(menuInstance.GetFirstSelectedButton());
    }

}


