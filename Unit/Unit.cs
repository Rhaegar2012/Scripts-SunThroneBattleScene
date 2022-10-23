using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UnitType
{
    Infantry=0,
    LightArmor=1,
    MBT=2,
    Artillery=3,
    Support=4,
    Aircraft=5
}
public class Unit : MonoBehaviour
{
    //Events
    //Fields
    private float defenseRating;
    private int attackRating;
    private UnitType unitType;
    private bool isEnemy;
    private GridNode gridNode;
    private SpriteRenderer spriteRenderer;
    private Sprite unitSprite;
    private int movementRange;
    private BaseAction[] actionList;
    private List<NodeType> walkableNodeTypeList;
    private bool actionCompleted=false;
    private UnitHealthSystem healthSystem;


    void Awake()
    {
        this.actionList=GetComponents<BaseAction>();
        this.healthSystem=GetComponent<UnitHealthSystem>();
    }
    void Start()
    {
        TurnSystem.Instance.OnTurnChanged+=TurnSystem_OnTurnChanged;
    }

    // Update is called once per frame
    void Update()
    {
       Vector2 newGridPosition= new Vector2(transform.position.x,transform.position.y);
       if(newGridPosition!=gridNode.GetGridPosition())
       {
            LevelGrid.Instance.MoveUnitGridPosition(this,gridNode.GetGridPosition(),newGridPosition);
       }

    }
    public void SetUnitParameters(int defenseRating,int attackRating,int movementRange
                                    ,UnitType unitType,bool isEnemy, GridNode gridNode,
                                    Sprite unitSprite)
    {
        this.defenseRating=defenseRating;
        this.attackRating=attackRating;
        this.unitType=unitType;
        this.isEnemy=isEnemy;
        this.gridNode=gridNode;
        this.unitSprite=unitSprite;
        this.movementRange=movementRange;
        spriteRenderer=gameObject.GetComponentInChildren(typeof(SpriteRenderer)) as SpriteRenderer;
        spriteRenderer.sprite=unitSprite;
        
       
    }
    public bool IsEnemy()
    {
        return isEnemy;
    }
    public GridNode GetUnitNode()
    {
        return gridNode;
    }
    public void SetUnitNode(GridNode gridNode)
    {
        this.gridNode=gridNode;
    }
    public Vector2 GetUnitPosition()
    {
        return gridNode.GetGridPosition();
    }
    public UnitType GetUnitType()
    {
        return unitType;
    }
    public int GetMovementRange()
    {
        return movementRange;
    }
    public List<Vector2> GetValidMovementPositionList()
    {
        Vector2 unitPosition=GetUnitPosition();
        int unitBaseMovement=movementRange;
        List<Vector2> validMovementPositions= new List<Vector2>();
        List<Vector2> directions=new List<Vector2>{new Vector2(1,0),new Vector2(-1,0),
                                                   new Vector2(0,1),new Vector2(0,-1),
                                                   new Vector2(1,1),new Vector2(-1,-1),
                                                   new Vector2(-1,1),new Vector2(1,-1)};
        foreach(Vector2 direction in directions)
        {
            Vector2 testPosition=unitPosition+direction;
            while(unitBaseMovement>0)
            {
                if(LevelGrid.Instance.IsValidGridPosition(testPosition))
                {
                    if(LevelGrid.Instance.HasAnyUnitAtGridNode(testPosition))
                    {
                        unitBaseMovement=0;
                    }
                    if(testPosition==unitPosition)
                    {
                        unitBaseMovement=0;
                    }
                    validMovementPositions.Add(testPosition);
                    GridNode testNode=LevelGrid.Instance.GetNodeAtPosition(testPosition);
                    NodeType nodeType=testNode.GetNodeType(); 
                    switch(nodeType)
                    {
                        case NodeType.Grassland:
                            unitBaseMovement=unitBaseMovement-1;
                            break;
                        case NodeType.Forest:
                            unitBaseMovement=unitBaseMovement-2;
                            break;
                        case NodeType.Mountain:
                            unitBaseMovement=unitBaseMovement-3;
                            break;
                        case NodeType.River:
                            unitBaseMovement=unitBaseMovement-2;
                            break;
                        case NodeType.Road:
                            unitBaseMovement=unitBaseMovement-0;
                            break;

                    }
                    
                    
                }
                else
                {
                    unitBaseMovement=0;
                }
                
                testPosition+=direction;
            }
            unitBaseMovement=movementRange;
            
        }
        return validMovementPositions;
    }
    public BaseAction GetAction(string actionName)
    {
        BaseAction selectedAction= Array.Find(actionList,action=>action.GetActionName()==actionName);
        return selectedAction;
    }
    public BaseAction[] GetActionArray()
    {
        return actionList;
    }
    public void SetWalkableNodeTypes()
    {
        switch(unitType)
        {
           case UnitType.Infantry:
                walkableNodeTypeList= new List<NodeType>{NodeType.Grassland,
                                                         NodeType.Forest,
                                                         NodeType.Mountain,
                                                         NodeType.River,
                                                         NodeType.Road};
                break;
            case UnitType.LightArmor:
                walkableNodeTypeList= new List<NodeType>{NodeType.Grassland,
                                                         NodeType.Forest,
                                                         NodeType.Road};
                break;
            case UnitType.MBT:
               walkableNodeTypeList= new List<NodeType>{NodeType.Grassland,
                                                         NodeType.Forest,
                                                         NodeType.Road};
            break;
               
        }
    }
    public void Damage(int damageAmount)
    {
        healthSystem.Damage(damageAmount);
    }
    public List<NodeType> GetWalkableNodeTypeList()
    {
        return walkableNodeTypeList;
    }
    public bool UnitCompletedAction()
    {
        return actionCompleted;
    }
    public void SetCompletedAction(bool isComplete)
    {
        actionCompleted=isComplete;
    }
    public void TurnSystem_OnTurnChanged(object sender, EventArgs empty)
    {
        actionCompleted=false;
    }
    public float GetDefenseRating()
    {
        float totalDefenseRating=defenseRating;
        float defenseModifier=0f;
        float unitHealth=healthSystem.GetHealthNormalized();
        NodeType nodeType= gridNode.GetNodeType();
        switch(nodeType)
        {
            case NodeType.Grassland:
                defenseModifier=0.15f;
                break;
            case NodeType.Forest:
                defenseModifier=0.3f;
                break;
            case NodeType.Mountain:
                defenseModifier=0.5f;
                break;
            case NodeType.River:
                defenseModifier=0f;
                break;
            case NodeType.Road:
                defenseModifier=0f;
                break;
        }
        if(totalDefenseRating==defenseRating)
        {
            totalDefenseRating=(defenseRating+defenseModifier)*unitHealth;
        }
  
        return totalDefenseRating;
        
    }
    public float GetAttackRating()
    {
        float unitHealth = healthSystem.GetHealthNormalized();

        return attackRating*unitHealth;
    }
    public int GetHealth()
    {
        return healthSystem.GetHealth();
    }
 
    


}
