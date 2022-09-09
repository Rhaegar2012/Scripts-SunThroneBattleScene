using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    //State enumerator
    private enum State
    {
        WaitingForEnemyTurn,
        TakingTurn,
        Busy
    }
    //Fields
    private List<Unit> unitList;
    private float timer;
    private State state;
    void Awake()
    {
        state=State.WaitingForEnemyTurn;
    }
    // Start is called before the first frame update
    void Start()
    {
        unitList=UnitManager.Instance.GetEnemyUnitList();
        TurnSystem.Instance.OnTurnChanged+=TurnSystem_OnTurnChanged;
    }

    // Update is called once per frame
    void Update()
    {
        if(TurnSystem.Instance.IsPlayerTurn())
        {
            return;
        }
        switch(state)
        {
            case State.WaitingForEnemyTurn:
                break;
            case State.TakingTurn:
                timer-=Time.deltaTime;
                if(timer<=0f)
                {
                    if(TryTakeEnemyAIAction(SetTakingTurn))
                    {
                        state=State.Busy;
                    }
                    else
                    {
                        TurnSystem.Instance.NextTurn();
                    }
                }
                break;
            case State.Busy:
                break;
        }
        
        
    }
    private void SetTakingTurn()
    {
        timer=0.5f;
        state=State.TakingTurn;
    }
    private bool TryTakeEnemyAIAction(Action onEnemyAIActionComplete)
    {
        foreach(Unit unit in unitList)
        {
            if(TryTakeEnemyAIAction(unit,onEnemyAIActionComplete))
            {
                return true;
            }
        }
        return false;
    }
    private bool TryTakeEnemyAIAction(Unit enemyUnit, Action onEnemyAIActionComplete)
    {
        return true;

    }
    public void TurnSystem_OnTurnChanged(object sender, EventArgs empty)
    {
        Debug.Log("Enemy Turn Starts");
        timer=2f;
    }
}
