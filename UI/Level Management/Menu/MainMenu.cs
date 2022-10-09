using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : Menu<MainMenu>
{
    
    public void OnLevelSelectionPressed()
    {
        LevelSelectionMenu.Open();
    }

    public void OnSettingsPressed()
    {
        //TODO
    }
 
}
