using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystem 
{
    private int height;
    private int width;
    private float cellSize;
    private GridNode[,] gridArray;
    //Constructor
    public GridSystem(int height, int width,float cellSize,int [,] mapData)
    {
        this.height=height;
        this.width=width;
        this.cellSize=cellSize;
        gridArray= new GridNode[width,height];
        //Node Creation
        for(int x=0;x<width;x++)
        {
            for(int y=0;y<height;y++)
            {
                Vector2 gridPosition= new Vector2(x,y);
                gridArray[x,y]=new GridNode(gridPosition, (NodeType)mapData[x,y]);
            }
        }

        
    }

    public bool IsValidGridPosition(Vector2 position)
    {
        return (position.x>=0 && 
                position.x<width && 
                position.y>=0 && 
                position.y<height);
    }

    public GridNode GetNodeAtPosition(Vector2 position)
    {
        return gridArray[(int)position.x,(int)position.y];
    }
    public int GetWidth()
    {
        return width;
    }

    public int GetHeight()
    {
        return height;
    }
    public float GetCellSize()
    {
        return cellSize;
    }
}
