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
    private int health=10;
    private int defenseRating;
    private int attackRating;
    private UnitType unitType;
    private bool isEnemy;
    private GridNode gridNode;
    private int actionPoints=2;
    private SpriteRenderer spriteRenderer;
    private Sprite unitSprite;
    private int movementRange;
    private BaseAction action;


    void Start()
    {
        //TODO
    }

    // Update is called once per frame
    void Update()
    {
       //TODO 
    }
    public void SetUnitParameters(int defenseRating,int attackRating,int movementRange
                                    ,UnitType unitType,bool isEnemy, GridNode gridNode,Sprite unitSprite)
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
        this.action=GetComponent<BaseAction>();
    }
    public bool IsEnemy()
    {
        return isEnemy;
    }
    public GridNode GetUnitNode()
    {
        return gridNode;
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
    public BaseAction GetAction()
    {
        return action;
    }

    


}
