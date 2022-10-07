using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{

    private bool levelComplete=false;
    // Start is called before the first frame update
    void Start()
    {
        UnitManager.Instance.OnArmyDestroyed+=UnitManager_OnArmyDestroyed;
    }

    // Update is called once per frame
    void Update()
    {
   
    }

    public void UnitManager_OnArmyDestroyed(object sender, String army)
    {
        Debug.Log("End Level event called");
        EndLevel(army);
    }
    private void EndLevel(string armyDestroyed)
    {
        LevelCompleteScreen.Open();
    }

}
