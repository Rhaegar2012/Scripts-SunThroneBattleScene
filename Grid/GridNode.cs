using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Enums 
public enum NodeType 
{
    Grassland=0,
    Forest=1,
    Mountain=2,
    River=3,
    Road=4

}
public class GridNode
{
    
    //Fields
    private Vector2 position;
    private GridSystem gridSystem;
    private NodeType nodeType;
    private List<Unit> unitList;
    private List<Target> targetList;
    private int gCost;
    private int hCost;
    private int fCost;
    private GridNode previousNode;
    private bool attackNode;
    //Constructor
    public GridNode(Vector2 position, NodeType nodeType)
    {
        this.position=position;
        this.nodeType=nodeType;
        unitList=new List<Unit>();
        targetList=new List<Target>();
    }

    public Vector2 GetGridPosition()
    {
        return position;
    }
    public NodeType GetNodeType()
    {
        return nodeType;
    }
    public void AddUnit(Unit unit)
    {
        unitList.Add(unit);
    }
    public void RemoveUnit(Unit unit)
    {
        unitList.Remove(unit);
    }
    public void AddTarget(Target target)
    {
        targetList.Add(target);
    }
    public void RemoveTarget(Target target)
    {
        targetList.Remove(target);
    }
    public bool HasAnyTarget()
    {
        return targetList.Count>0;
    }
    public bool HasAnyUnit()
    {
        return unitList.Count>0;
    }
    public Target GetTarget()
    {
        if(HasAnyTarget())
        {
            return targetList[0];
        }
        else
        {
            return null;
        }
    }
    public Unit GetUnit()
    {
        if(HasAnyUnit())
        {
            return unitList[0];
        }
        else
        {
            return null;
        }
    }
    public List<Unit> GetUnitList()
    {
        return unitList;
    }
    public int GetGCost()
    {
        return gCost;
    }
    public int GetHCost()
    {
        return hCost;
    }
    public int GetFCost()
    {
        return fCost;
    }
    public void SetGCost(int gCost)
    {
        this.gCost=gCost;
    }
    public void SetHCost(int hCost)
    {
        this.hCost=hCost;
    }
    public int CalculateFCost()
    {
        return fCost=gCost+hCost;
    }
    public GridNode GetPreviousNode()
    {
        return previousNode;
    }
    public void SetPreviousNode(GridNode previousNode)
    {
        this.previousNode=previousNode;
    }
    public void ResetPreviousNode()
    {
        previousNode=null;
    }
    public bool IsAttackNode()
    {
        return attackNode;
    }
    public void SetAttackNode(bool isAttackNode)
    {
        attackNode=isAttackNode;
    }




}
