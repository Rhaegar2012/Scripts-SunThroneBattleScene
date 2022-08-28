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
    //Constructor
    public GridNode(Vector2 position, NodeType nodeType)
    {
        this.position=position;
        this.nodeType=nodeType;
        unitList=new List<Unit>();
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
    public bool HasAnyUnit()
    {
        return unitList.Count>0;
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




}
