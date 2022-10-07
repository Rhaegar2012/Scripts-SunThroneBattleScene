using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        Debug.Log($"MenuManager {MenuManager.Instance}");
        Debug.Log($"Menu Instance {Instance}");
        if(MenuManager.Instance!=null && Instance!=null)
        {
            Debug.Log("MENU:Open accessed");
            MenuManager.Instance.OpenMenu(Instance);
        }
    }


}
public abstract class Menu:MonoBehaviour
{

}

