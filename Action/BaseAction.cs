using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAction : MonoBehaviour
{
    //Fields
    protected Unit unit;
    protected bool isActive;
    //Action Delegate
    protected Action onActionComplete;
    public  EventHandler OnActionStarted;
    public  EventHandler OnActionCompleted;
    protected virtual void Awake()
    {
        unit=GetComponent<Unit>();
    }
    //Abstract functions
    public abstract string GetActionName();
    public abstract void TakeAction(Vector2 gridPosition ,Action onActionComplete);
    public abstract List<Vector2> GetValidGridPositionList();
    public abstract EnemyAIAction GetEnemyAIAction(Vector2 gridPosition);
    //General functions
    public bool IsValidGridPositionList(Vector2 gridPosition)
    {
        List<Vector2> validGridPositionList=GetValidGridPositionList();
        return validGridPositionList.Contains(gridPosition);
    }
    protected void ActionStart(Action onActionComplete)
    {
        isActive=true;
        this.onActionComplete=onActionComplete;
        //TODO OnAnyActionStarted Invoke
    }
    protected void ActionComplete()
    {
        isActive=false;
        onActionComplete();
        OnActionCompleted?.Invoke(this,EventArgs.Empty);
    }
    public EnemyAIAction GetBestEnemyAIAction()
    {
        List<Vector2> validGridPositions= GetValidGridPositionList();
        List<EnemyAIAction> enemyActionList=new List<EnemyAIAction>();
        foreach(Vector2 position in validGridPositions)
        {
            EnemyAIAction enemyAction=GetEnemyAIAction(position);
            enemyActionList.Add(enemyAction);
        }
        if(enemyActionList.Count>0)
        {
            enemyActionList.Sort((EnemyAIAction a, EnemyAIAction b)=>b.actionValue-a.actionValue);
            return enemyActionList[0];
        }
        else
        {
            return null;
        }
     
    }
}
