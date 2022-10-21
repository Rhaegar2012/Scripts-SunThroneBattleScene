using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAction : BaseAction
{
    //Events
    public static event EventHandler OnAnyUnitMoved;
    //Fields
    [SerializeField] private float moveSpeed;
    private float stoppingDistance=0.1f;
    private Vector2 currentGridPosition;
    private Vector2 targetPosition;
    private int currentIndex;
    private List<GridNode> pathList;
    private float distanceToTargetNode;
    // Start is called before the first frame update
    void Start()
    {
        currentGridPosition=unit.GetUnitPosition();
        pathList=new List<GridNode>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!isActive)
        {
            return;
        }
        targetPosition=pathList[currentIndex].GetGridPosition();
        //Debug.Log($"Current Node movement:{targetPosition}");
        float distanceToTarget= Vector2.Distance(currentGridPosition,targetPosition);
        Vector2 moveDirection=(targetPosition-currentGridPosition).normalized;
        if(distanceToTarget>stoppingDistance)
        {
            transform.position+=new Vector3(moveDirection.x,moveDirection.y,0f)*moveSpeed*Time.deltaTime;
            currentGridPosition=new Vector2(transform.position.x,transform.position.y);

        }
        else
        {
            transform.position=new Vector2(Mathf.Round(currentGridPosition.x),Mathf.Round(currentGridPosition.y));
            currentIndex++;
            
        }
        if(currentIndex>=pathList.Count)
        {
            unit.SetCompletedAction(true);
            ActionComplete();
        }
        
    }
    public override string GetActionName()
    {
        return "Move";
    }
    public override List<Vector2> GetValidGridPositionList()
    {
        int movementRange=unit.GetMovementRange();
        Vector2 unitGridPosition=unit.GetUnitPosition();
        List<Vector2> validGridPositionList= new List<Vector2>();
        foreach(Vector2 position in unit.GetValidMovementPositionList())
        {
            GridNode testNode= LevelGrid.Instance.GetNodeAtPosition(position);
            NodeType testNodeType= testNode.GetNodeType();
            List<NodeType> walkableNodeTypes=unit.GetWalkableNodeTypeList();
            if(!walkableNodeTypes.Contains(testNodeType))
            {
                continue;
            }
            validGridPositionList.Add(position);
        }
        return validGridPositionList;
    }
    public override void TakeAction(Vector2 gridPosition, Action onActionComplete)
    {
        currentIndex=0;
        targetPosition=gridPosition;
        List<Vector2> validGridPositions= GetValidGridPositionList();
        //If an attack position is outside of movement range allows enemy to move to the edge of 
        //the movement range to get closer for attack
        //(Enemy AI)
        if(!validGridPositions.Contains(targetPosition))
        {
            targetPosition=validGridPositions.Find(position=>Vector2.Distance(position,gridPosition)>=unit.GetMovementRange());
        }
        GridNode targetNode=LevelGrid.Instance.GetNodeAtPosition(targetPosition);
        pathList= Pathfinding.Instance.FindPath(unit,GetValidGridPositionList(),targetNode);
        OnAnyUnitMoved?.Invoke(this,EventArgs.Empty);
        ActionStart(onActionComplete);
        
    }
    public override EnemyAIAction GetEnemyAIAction(Vector2 gridPosition)
    {
        //Checks if there is an enemy in this position to be attacked
        AttackAction attackAction=unit.GetAction("Attack") as AttackAction;
        int enemyCount=attackAction.GetTargetCountAtPosition(gridPosition);
        if(enemyCount==0)
        {
            distanceToTargetNode=Vector2.Distance(unit.GetUnitPosition(),gridPosition);
            return new EnemyAIAction
            {
                actionValue=(int)distanceToTargetNode*5,
                gridPosition=gridPosition
            };
        }
        return new EnemyAIAction
        {
            actionValue=enemyCount*10,
            gridPosition=gridPosition
        };
    }
}
