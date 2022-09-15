using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitAction : BaseAction
{
    // Start is called before the first frame update

    public override string GetActionName()
    {
        return "Wait";
    }
    public override  List<Vector2> GetValidGridPositionList()
    {
        List<Vector2> validGridPosition= new List<Vector2>();
        validGridPosition.Add(unit.GetUnitPosition());
        return validGridPosition;

    }
    public override EnemyAIAction GetEnemyAIAction(Vector2 gridPosition)
    {
        return new EnemyAIAction
        {
            actionValue=1,
            gridPosition=gridPosition
        };
    }
    public override void TakeAction(Vector2 gridPosition, Action onActionComplete)
    {
        ActionStart(onActionComplete);
        unit.SetCompletedAction(true);
        ActionComplete();
    }
}
