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
        MenuManager.Instance.UpdateSelectedButton(LevelSelectionMenu.Instance.GetFirstSelectedButton());
        LevelSelectionMenu.Open();
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
