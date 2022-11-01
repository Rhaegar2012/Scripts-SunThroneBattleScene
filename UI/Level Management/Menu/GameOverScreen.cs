using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScreen : Menu<GameOverScreen>
{
   [SerializeField] private GameObject selectedButton;
   public void OnRetryPressed()
   {
        LevelLoader.ReloadLevel();
   }
   public  void SetFirstSelectedButton()
   {
      base.firstSelectedButton=selectedButton;
      OnMenuCalled(GameOverScreen.Instance);
   }
}
