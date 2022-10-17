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
    List<Unit> enemiesInRange=new List<Unit>();
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
            //Debug.Log($"Unit {unit.GetUnitType()} attacks {targetUnit.GetUnitType()}");
            unit.SetCompletedAction(true);
            int damageAmount=CalculateDamage();
            //Debug.Log($"Damage inflicted {damageAmount}");
            targetUnit.Damage(damageAmount);
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
        List<Vector2> attackDirections= new List<Vector2>{new Vector2(1,0f),
                                                          new Vector2(-1,0f),
                                                          new Vector2(0f,1f),
                                                          new Vector2(0f,-1f)};
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
                int testDistance= Mathf.Abs(x)+Mathf.Abs(y);
                if(testDistance>unit.GetMovementRange())
                {
                    continue;
                }
                if(!LevelGrid.Instance.HasAnyUnitAtGridNode(testPosition))
                {
                    continue;
                }
                Unit testUnit=LevelGrid.Instance.GetUnitAtGridNode(testPosition);
                if(testUnit.IsEnemy()==unit.IsEnemy())
                {
                    continue;
                }
                enemiesInRange.Add(testUnit);
                foreach(Vector2 direction in attackDirections)
                {
                    Vector2 attackPosition=testPosition+direction;
                    if(!LevelGrid.Instance.IsValidGridPosition(attackPosition))
                    {
                        continue;
                    }
                    if(!LevelGrid.Instance.HasAnyUnitAtGridNode(attackPosition))
                    {
                        validGridPositionList.Add(attackPosition);
                    }
                }
                
            }
        }
        return validGridPositionList;
    }
    public override EnemyAIAction GetEnemyAIAction(Vector2 gridPosition)
    {
        
        return new EnemyAIAction
        {
            gridPosition=gridPosition,
            actionValue=100
        };
     
    }
    public int GetTargetCountAtPosition(Vector2 gridPosition)
    {
        enemiesInRange.Clear();
        GetValidGridPositionList();
        return enemiesInRange.Count;
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
        attackNode=gridPosition;
        if(EnemyUnitInAttackRange(attackNode))
        {

            moveAction.TakeAction(attackNode,onActionComplete);
            ActionStart(onActionComplete);

        }
      
    }
    private int CalculateDamage()
    {
        float baseAttack=unit.GetAttackRating();
        float baseDefense=targetUnit.GetDefenseRating();
        int damageAmount=(int)(Mathf.Ceil(baseAttack-baseDefense));
        return damageAmount;

    }
}
