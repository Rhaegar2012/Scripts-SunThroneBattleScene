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
    public static EventHandler OnAnyActionStarted;
    public static EventHandler OnAnyActionCompleted;
    protected virtual void Awake()
    {
        unit=GetComponent<Unit>();
    }
    //Abstract functions
    public abstract string GetActionName();
    public abstract void TakeAction(Vector2 gridPosition ,Action onActionComplete);
    public abstract List<Vector2> GetValidGridPositionList();
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
        //TODO OnAnyActionCompleted Invoke
    }
}