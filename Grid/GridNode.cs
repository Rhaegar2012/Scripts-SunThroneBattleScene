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
    //Constructor
    public GridNode(Vector2 position, NodeType nodeType)
    {
        this.position=position;
        this.nodeType=nodeType;
    }

    public Vector2 GetGridPosition()
    {
        return position;
    }
    public NodeType GetNodeType()
    {
        return nodeType;
    }


}
