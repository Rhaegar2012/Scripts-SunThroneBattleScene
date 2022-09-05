using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    //Singleton
    public static Pathfinding Instance {get; private set;}
    //Fields
    private List<Vector2> openList;
    private List<Vector2> closedList;
    private int width;
    private int height;
    private const int BASE_MOVEMENT_COST=10;

    void Awake()
    {
        if(Instance!=null)
        {
            Debug.LogWarning("Pathfinding singleton instance already exists");
            Destroy(gameObject);
            return;
        }
        Instance=this;
    }
    void Start()
    {
        this.width=LevelGrid.Instance.GetWidth();
        this.height=LevelGrid.Instance.GetHeight();
    }
    public List<GridNode> FindPath(Unit selectedUnit,List<Vector2> validGridPositionList, GridNode endNode)
    {
        int movementRange= selectedUnit.GetMovementRange();
        Vector2 startPosition=selectedUnit.GetUnitPosition();
        Vector2 endPosition=endNode.GetGridPosition();
        GridNode startNode=LevelGrid.Instance.GetNodeAtPosition(startPosition);
        GridNode endNode=LevelGrid.Instance.GetNodeAtPosition(endPosition);
        if(!validGridPositionList.Contains(endNode))
        {
            return null; 
        }
        openList= new List<Vector2>();
        closedList=new List<Vector2>();
        openList.Add(startPosition);
        for(int x=0; x<width;x++)
        {
            for(int y=0;y<height;y++)
            {
                GridNode gridNode=LevelGrid.Instance.GetNodeAtPosition(new Vector2(x,y));
                gridNode.SetGCost(int.MaxValue);
                gridNode.SetHCost(0);
                gridNode.CalculateFCost();
                gridNode.ResetPreviousNode();
            }
        }
        startNode.SetGCost(0);
        startNode.SetHCost(0);
        startNode.CalculateFCost();
        
    }
    private int CalculateDistance(Vector2 startPosition,Vector2 endPosition)
    {
        //TODO
    }
    private GridNode GetLowestFCostNode(List<GridNode> gridNodeList)
    {
        //TODO
    }

}
