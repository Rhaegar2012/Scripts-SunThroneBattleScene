using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CaptureAction : BaseAction
{
    Target target;
    private void Update()
    {
        if(isActive)
        {
            if(!target.IsTargetCaptured())
            {
                target.ExecuteCapture();
            }
            else
            {
                ActionComplete();
            }

        }

    }
    public override string GetActionName()
    {
        return "Capture";
    }
    public override List<Vector2> GetValidGridPositionList()
    {
        List<Vector2> validGridPositionList= new List<Vector2>();
        int movementRange=unit.GetMovementRange();
        for(int x=-movementRange;x<movementRange;x++)
        {
            for(int y=-movementRange;y<movementRange;y++)
            {
                Vector2 offsetPosition= new Vector2(x,y);
                Vector2 testPosition=unit.GetUnitPosition()+offsetPosition;
                if(!LevelGrid.Instance.IsValidGridPosition(testPosition))
                {
                    continue;
                } 
                if(!LevelGrid.Instance.HasAnyTargetAtGridNode(testPosition))
                {
                    continue;
                }
                Target target=LevelGrid.Instance.GetTargetAtGridNode(testPosition);
                if(target.IsEnemyTarget()==unit.IsEnemy())
                {
                    continue;
                }
                if(target.IsTargetCaptured())
                {
                    continue;
                }
                if(unit.GetUnitType()!=UnitType.Infantry)
                {
                    continue;
                }
                validGridPositionList.Add(testPosition);
            }
        }
        return validGridPositionList;
    }
    public override EnemyAIAction GetEnemyAIAction(Vector2 gridPosition)
    {
        UnitType unitType=unit.GetUnitType();
        if(unitType==UnitType.Infantry)
        {
            return new EnemyAIAction
            {
                gridPosition=gridPosition,
                actionValue=GetValidGridPositionList().Count*100
            };
        }
        else
        {
            return new EnemyAIAction
            {
                gridPosition=gridPosition,
                actionValue=1
            };
        }

    }
    public override void TakeAction(Vector2 gridPosition, Action onActionComplete)
    {
        if(GetValidGridPositionList().Contains(gridPosition))
        {
            target=LevelGrid.Instance.GetTargetAtGridNode(gridPosition);
        }
        ActionStart(onActionComplete);

    }
    
}