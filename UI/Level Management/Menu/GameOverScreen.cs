using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScreen : Menu<GameOverScreen>
{
   public void OnRetryPressed()
   {
        LevelLoader.ReloadLevel();
   }
}
