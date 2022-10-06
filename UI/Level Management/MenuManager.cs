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
    [SerializeField] private BattleWonScreen battleWonScreenPrefab;
    [SerializeField] private BattleLostScreen battleLostScreenPrefab;
    private Transform menuParent;
    private Stack<Menu> menuStack;
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

    }
    void Start()
    {
        
    }
    private void InitializeMenus()
    {
        if(menuParent==null)
        {
            GameObject menuParentObject=new GameObject("Menus");
            menuParent=menuParentObject.transform;
        }
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
            }
            //TODO: Open main menu prefab here
            
        }


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
    
}
