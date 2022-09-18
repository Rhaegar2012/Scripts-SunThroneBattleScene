using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAction : BaseAction
{
    //Fields
    Unit targetUnit;
    private BaseAction moveAction;
    private int attackRange;
    private Vector2 attackNode;
    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
        moveAction=GetComponent<MoveAction>();
    }
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
        if(unit.GetUnitPosition()==attackNode)
        {
            targetUnit.Damage();
            unit.SetCompletedAction(true);
            ActionComplete();
        }
     

    }
    public override string GetActionName()
    {
        return "Attack";
    }
    public override List<Vector2> GetValidGridPositionList()
    {
        Vector2 gridPosition= unit.GetUnitPosition();
        return GetValidGridPositionList(gridPosition);
    }
    public  List<Vector2> GetValidGridPositionList(Vector2 unitGridPosition)
    {
        List<Vector2> validGridPositionList=new List<Vector2>();
        for(int x=-attackRange;x<attackRange;x++)
        {
            for(int y=-attackRange;y<attackRange;y++)
            {
                Vector2 offsetPosition= new Vector2(x,y);
                Vector2 testPosition= unitGridPosition+offsetPosition;
                if(!LevelGrid.Instance.IsValidGridPosition(testPosition))
                {
                    continue;
                }
                if(!EnemyUnitInAttackRange(testPosition))
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
            actionValue=1000
        };
     
    }
    public int GetTargetCountAtPosition(Vector2 gridPosition)
    {
        return GetValidGridPositionList(gridPosition).Count;

    }
    public bool EnemyUnitInAttackRange(Vector2 gridPosition)
    {
        List<Vector2> attackDirections= new List<Vector2>{new Vector2(1,0f),
                                                          new Vector2(-1,0f),
                                                          new Vector2(0f,1f),
                                                          new Vector2(0f,-1f)};
        foreach(Vector2 direction in attackDirections)
        {
            Vector2 testPosition=gridPosition+direction;
            if(!LevelGrid.Instance.IsValidGridPosition(testPosition))
            {
                continue;
            }
            if(!LevelGrid.Instance.HasAnyUnitAtGridNode(testPosition))
            {
                continue;
            }
            Unit testUnit= LevelGrid.Instance.GetUnitAtGridNode(testPosition);
            if(testUnit.IsEnemy()==unit.IsEnemy())
            {
                continue;
            }
            targetUnit=testUnit;
            return true;
        }
        return false;

    }
    public override void TakeAction(Vector2 gridPosition, Action onActionComplete)
    {
        Debug.Log("Attack accessed");
        attackNode=gridPosition;
        moveAction.TakeAction(gridPosition,onActionComplete);
        ActionStart(onActionComplete);
      
     
    }
}
