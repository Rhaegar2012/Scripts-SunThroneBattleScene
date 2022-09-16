using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitAction : BaseAction
{
    // Start is called before the first frame update
    void Update()
    {
        if(!isActive)
        {
            return;
        }
        unit.SetCompletedAction(true);
        ActionComplete();

    }
    public override string GetActionName()
    {
        return "Wait";
    }
    public override  List<Vector2> GetValidGridPositionList()
    {
        List<Vector2> validGridPositionList= new List<Vector2>();
        int movementRange=unit.GetMovementRange();
        for(int x=-movementRange;x<movementRange;x++)
        {
            for(int y=-movementRange;y<movementRange;y++)
            {
                Vector2 offsetPosition = new Vector2(x,y);
                Vector2 testPosition=unit.GetUnitPosition()+offsetPosition;
                if(!LevelGrid.Instance.IsValidGridPosition(testPosition))
                {
                    continue;
                }
                validGridPositionList.Add(testPosition);

            }
        }
        validGridPositionList.Add(unit.GetUnitPosition());
        return validGridPositionList;

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
       
    }
}
