using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        UnitManager.Instance.OnArmyDestroyed+=UnitManager_OnArmyDestroyed;
    }

    // Update is called once per frame
    void Update()
    {
   
    }

    public void UnitManager_OnArmyDestroyed(object sender, string army)
    {
        Debug.Log("End Level event called");
        EndLevel(army);
    }
    private void EndLevel(string army)
    {
        if(army=="Enemy")
        {
            LevelCompleteScreen.Open();
        }
        if(army=="Player")
        {
            GameOverScreen.Open();
        }

    }

}
