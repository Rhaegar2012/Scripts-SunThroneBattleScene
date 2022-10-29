using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : Menu<MainMenu>
{
    [SerializeField] private GameObject selectedButton;
    public void OnLevelSelectionPressed()
    {
        LevelSelectionMenu.Instance.SetFirstSelectedButton();
        OnMenuCalled(LevelSelectionMenu.Instance);
        LevelSelectionMenu.Open();
    }
    protected override void OnMenuCalled(Menu menuInstance)
    {
        base.OnMenuCalled(menuInstance);
    }

    public void OnSettingsPressed()
    {
        //TODO
    }
    public void SetFirstSelectedButton()
    {
        base.firstSelectedButton=selectedButton;
    }
 
}
