using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystem 
{
    [SerializeField] private int height;
    [SerializeField] private int width;
    [SerializeField] private float cellSize;
    private GridNode[,] gridArray;
    //Constructor
    public GridSystem(int height, int width,float cellSize)
    {
        this.height=height;
        this.width=width;
        this.cellSize=cellSize;
        gridArray= new GridNode[width,height];
        
    }
}
