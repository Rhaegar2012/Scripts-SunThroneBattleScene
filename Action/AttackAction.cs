using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAction : BaseAction
{
    //Fields
    private int attackRange;
    Unit targetUnit;
    // Start is called before the first frame update
    void Start()
    {
        attackRange=unit.GetMovementRange()+1;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isActive)
        {
            return;
        }

    }
    public override string GetActionName()
    {
        return "Attack";
    }
    public override List<Vector2> GetValidGridPositionList()
    {
        List<Vector2> validGridPositionList=new List<Vector2>();
        for(int x=-attackRange;x<attackRange;x++)
        {
            for(int y=-attackRange;y<attackRange;y++)
            {
                Vector2 offsetPosition= new Vector2(x,y);
                Vector2 testPosition= unit.GetUnitPosition()+offsetPosition;
                if(!LevelGrid.Instance.IsValidGridPosition(testPosition))
                {
                    continue;
                }
                Unit testUnit=LevelGrid.Instance.GetUnitAtGridNode(testPosition);
                if(testUnit!=null && testUnit.IsEnemy()==unit.IsEnemy())
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
        return new EnemyAIAction
        {
            gridPosition=gridPosition,
            actionValue=10
        };
    }
    public override void TakeAction(Vector2 gridPosition, Action onActionComplete)
    {
        //TODO
        ActionStart(onActionComplete);

    }
}
