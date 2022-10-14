using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    //Singleton
    public static MenuManager Instance{get;private set;}
    //Fields
    [SerializeField] private LevelCompleteScreen levelCompleteScreenPrefab;
    [SerializeField] private GameOverScreen gameOverScreenPrefab;
    [SerializeField] private MainMenu mainMenuPrefab;
    [SerializeField] private LevelSelectionMenu levelSelectionPrefab;
    private Transform menuParent;
    private Stack<Menu> menuStack;
    private int mainMenuIndex=0;
    // Start is called before the first frame update
    void Awake()
    {
        if(Instance!=null)
        {
            Debug.Log("MenuManager Instance already exists");
            Destroy(gameObject);
            return;
        }
        Instance=this;
        UnityEngine.Object.DontDestroyOnLoad(gameObject);
        menuStack=new Stack<Menu>();
       


    }
    void Start()
    {
        InitializeMenus();
    }
    private void InitializeMenus()
    {
        if(menuParent==null)
        {
            GameObject menuParentObject=new GameObject("Menus");
            menuParent=menuParentObject.transform;
        }
        //Menu dontdestroy on load 
        UnityEngine.Object.DontDestroyOnLoad(menuParent.gameObject);
        //Reflection obtains all the menu serialized fields and collects them into
        Type myType=this.GetType();
        BindingFlags myFlags=BindingFlags.Instance| BindingFlags.NonPublic| BindingFlags.DeclaredOnly;
        FieldInfo[] fields=myType.GetFields(myFlags);
        foreach(FieldInfo field in fields)
        {
            Menu menuPrefab=field.GetValue(this) as Menu;
            if(menuPrefab!=null)
            {
                Menu menuInstance=Instantiate(menuPrefab,menuParent);
                menuInstance.gameObject.SetActive(false);
                menuInstance.SubscribeToEvents();
            }
            
            
        }
        MainMenu.Open();


    }
    public void OpenMenu(Menu menuInstance)
    {

        if(menuInstance==null)
        {
            Debug.LogWarning("MENUMANAGER: OpenMenu Invalid menuInstance");
            return;
        }
        if(menuStack.Count>0)
        {
            foreach(Menu menu in menuStack)
            {
                menu.gameObject.SetActive(false);
            }
            menuInstance.gameObject.SetActive(true);
            menuStack.Push(menuInstance);

        }
        else
        {
            menuInstance.gameObject.SetActive(true);
            menuStack.Push(menuInstance);
        }

    }
    public void CloseMenu()
    {
        Menu menuInstance=menuStack.Pop();
        menuInstance.gameObject.SetActive(false);
        if(menuStack.Count>0)
        {
            Menu nextMenu=menuStack.Peek();
            nextMenu.gameObject.SetActive(true);
        }

    }

    public void CloseAllMenus()
    {
        foreach(Menu menu in menuStack)
        {
            menu.gameObject.SetActive(false);
        }
    }
    
}
