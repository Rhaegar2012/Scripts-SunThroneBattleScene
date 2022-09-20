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
            totalDefenseRating=defenseRating+defenseModifier;
        }
  
        return totalDefenseRating;
        
    }
    public int GetAttackRating()
    {
        return attackRating;
    }
    public int GetHealth()
    {
        return healthSystem.GetHealth();
    }
 
    


}
