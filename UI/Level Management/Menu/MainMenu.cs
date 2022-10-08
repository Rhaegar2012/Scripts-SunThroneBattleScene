using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : Menu<MainMenu>
{
    private int firstLevelIndex=1;
    public void OnPlayPressed()
    {
        LevelLoader.LoadLevel(firstLevelIndex);
    }

    public void OnSettingsPressed()
    {
        //TODO
    }
    public void OnQuitPressed()
    {
        //TODO
    }
}
