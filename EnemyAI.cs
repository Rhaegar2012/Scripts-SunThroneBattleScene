using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    //Fields
    private List<Unit> unitList;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        unitList=UnitManager.Instance.GetEnemyUnitList();
        TurnSystem.Instance.OnTurnChanged+=TurnSystem_OnTurnChanged;
    }

    // Update is called once per frame
    void Update()
    {
        timer-=Time.deltaTime;
        if(timer>0)
        {
            return;
        }
        if(!TurnSystem.Instance.IsPlayerTurn())
        {
            Debug.Log("Enemy Turn Finishes");
            TurnSystem.Instance.NextTurn();
        }
        
    }
    public void TurnSystem_OnTurnChanged(object sender, EventArgs empty)
    {
        Debug.Log("Enemy Turn Starts");
        timer=2f;
    }
}
