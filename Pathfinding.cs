using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    //Singleton
    public static Pathfinding Instance {get; private set;}
    //Fields
    private List<GridNode> openList;
    private List<GridNode> closedList;
    private int width;
    private int height;
    private const int BASE_MOVEMENT_COST=10;
    private int pathLength;

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
        Vector2 endPosition= endNode.GetGridPosition();
        GridNode startNode=LevelGrid.Instance.GetNodeAtPosition(startPosition);
        if(!validGridPositionList.Contains(endPosition))
        {
    
            return null; 
        }
        openList= new List<GridNode>();
        closedList=new List<GridNode>();
        openList.Add(startNode);
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
        startNode.SetHCost(CalculateDistance(startPosition,endPosition));
        startNode.CalculateFCost();
        while(openList.Count>0)
        {
            GridNode currentNode=GetLowestFCostNode(openList);
            if(currentNode==endNode)
            {
                pathLength=currentNode.CalculateFCost();
                return CalculatePath(endNode);
            }
            openList.Remove(currentNode);
            closedList.Add(currentNode);
            foreach(GridNode neighbor in GetNeighbors(currentNode.GetGridPosition()))
            {
                if(closedList.Contains(neighbor))
                {
                    continue;
                }
                if(!selectedUnit.GetWalkableNodeTypeList().Contains(neighbor.GetNodeType()))
                {
                    closedList.Add(neighbor);
                    continue;
                }
                int tentativeGCost=currentNode.GetGCost()+CalculateDistance(currentNode.GetGridPosition(),neighbor.GetGridPosition());
                if(tentativeGCost<neighbor.GetGCost())
                {
                    neighbor.SetPreviousNode(currentNode);
                    neighbor.SetGCost(tentativeGCost);
                    neighbor.SetHCost(CalculateDistance(neighbor.GetGridPosition(),endPosition));
                    neighbor.CalculateFCost();
                    if(!openList.Contains(neighbor))
                    {
                        openList.Add(neighbor);
                    }
                }


            }

        }
        //No Path found 
        pathLength=0;
        return null;
        
    }
    private int CalculateDistance(Vector2 startPosition,Vector2 endPosition)
    {
        int distance=(int)Vector2.Distance(startPosition,endPosition);
        return distance*BASE_MOVEMENT_COST;
    }
    private GridNode GetLowestFCostNode(List<GridNode> gridNodeList)
    {
        int minFCost=int.MaxValue;
        GridNode minFCostNode=null;
        foreach(GridNode node in gridNodeList)
        {
            int fCost=node.GetFCost();
            if(fCost<minFCost)
            {
                minFCost=fCost;
                minFCostNode=node;
            }

        }
        return minFCostNode;
    }
    private List<GridNode> GetNeighbors(Vector2 gridPosition)
    {
        List<GridNode> neighborList= new List<GridNode>();
        List<Vector2> neighborPositions= new List<Vector2> {new Vector2(1,0),
                                                            new Vector2(-1,0),
                                                            new Vector2(0,1),
                                                            new Vector2(0,-1)};
        foreach(Vector2 offsetPosition in neighborPositions)
        {
            Vector2 testPosition=gridPosition+offsetPosition;
            if(LevelGrid.Instance.IsValidGridPosition(testPosition))
            {
                GridNode neighbor=LevelGrid.Instance.GetNodeAtPosition(testPosition);
                neighborList.Add(neighbor);
            }
        }
        return neighborList;

    }
    private List<GridNode> CalculatePath(GridNode node)
    {
        List<GridNode> pathList=new List<GridNode>();
        pathList.Add(node);
        GridNode currentNode=node;
        while(currentNode.GetPreviousNode()!=null)
        {
            pathList.Add(currentNode.GetPreviousNode());
            currentNode=currentNode.GetPreviousNode();
        }
        pathList.Reverse();
        return pathList;
    }

}
