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
            if(!unit.UnitCompletedAction() && TryTakeEnemyAIAction(unit,onEnemyAIActionComplete))
            {
                CameraController.Instance.UpdateFollowingUnit(unit.transform);
                return true;
            }
            else
            {
                continue;
            }
        }
        return false;
    }
    private bool TryTakeEnemyAIAction(Unit enemyUnit, Action onEnemyAIActionComplete)
    {
       BaseAction bestBaseAction=null;
       EnemyAIAction bestEnemyAIAction=null;
       foreach(BaseAction baseAction in enemyUnit.GetActionArray())
       {
            if(bestEnemyAIAction==null)
            {
                bestBaseAction=baseAction;
                bestEnemyAIAction=baseAction.GetBestEnemyAIAction();
            }
            else
            {
                EnemyAIAction testEnemyAIAction =baseAction.GetBestEnemyAIAction();
                if(testEnemyAIAction!=null&&testEnemyAIAction.actionValue>bestEnemyAIAction.actionValue)
                {
                    bestEnemyAIAction=testEnemyAIAction;
                    bestBaseAction=baseAction;
                }
            }
       }
       if(bestEnemyAIAction!=null)
       {
          bestBaseAction.TakeAction(bestEnemyAIAction.gridPosition,onEnemyAIActionComplete);
          return true;
       }
       else
       {
         return false;
       }
    }
    public void TurnSystem_OnTurnChanged(object sender, EventArgs empty)
    {
        timer=2f;
        SetTakingTurn();
    }
}
