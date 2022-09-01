using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAction : BaseAction
{
    //Fields
    [SerializeField] private float moveSpeed;
    private float stoppingDistance=0.1f;
    private Vector2 currentGridPosition;
    private Vector2 targetPosition;
    // Start is called before the first frame update
    void Start()
    {
        currentGridPosition=unit.GetUnitPosition();
    }

    // Update is called once per frame
    void Update()
    {
        if(isActive)
        {
            float distanceToTarget= Vector2.Distance(currentGridPosition,targetPosition);
            Vector2 moveDirection=(targetPosition-currentGridPosition).normalized;
            Debug.Log($"Move direction {moveDirection}");
            if(distanceToTarget>stoppingDistance)
            {
                transform.position+=new Vector3(moveDirection.x,moveDirection.y,0f)*moveSpeed*Time.deltaTime;

            }
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
        for(int x=-movementRange;x<=movementRange;x++)
        {
            for(int y=-movementRange;y<=movementRange;y++)
            {
                Vector2 offsetGridPosition= new Vector2(x,y);
                Vector2 testGridPosition=unitGridPosition+offsetGridPosition;
                if(!LevelGrid.Instance.IsValidGridPosition(testGridPosition))
                {
                    continue;
                }
                if(testGridPosition==unitGridPosition)
                {
                    continue;
                }
                if(LevelGrid.Instance.HasAnyUnitAtGridNode(testGridPosition))
                {
                    continue;
                }
                validGridPositionList.Add(testGridPosition);
                
            }
        }
        return validGridPositionList;
    }
    public override void TakeAction(Vector2 gridPosition, Action onActionComplete)
    {
        targetPosition=gridPosition;
        ActionStart(onActionComplete);
        //TODO
    }
}
