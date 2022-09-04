using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGrid : MonoBehaviour
{
    //Singleton
    public static LevelGrid Instance {get; private set;}
    //Fields
    [SerializeField] int width;
    [SerializeField] int height;
    [SerializeField] float cellSize;
    private int[,] mapData;
    private GridSystem gridSystem;

    private void Awake()
    {
        if(Instance!=null)
        {
            Debug.LogWarning("LevelGrid Singleton already exists");
            Destroy(gameObject);
            return;
        }
        Instance=this;
        this.mapData=GridData.Instance.ReadMap(width,height);
        gridSystem= new GridSystem(width,height,cellSize,mapData);
    }

    void Start()
    {
        //TODO
    }

    //Callbacks to GridSystem 
    public bool IsValidGridPosition(Vector2 gridPosition)
    {
        return gridSystem.IsValidGridPosition(gridPosition);
    }

    public GridNode GetNodeAtPosition(Vector2 gridPosition)
    {
        return gridSystem.GetNodeAtPosition(gridPosition);
    }
    public int GetWidth()
    {
        return gridSystem.GetWidth();
    }
    public int GetHeight()
    {
        return gridSystem.GetHeight();
    }
    public float GetCellSize()
    {
        return gridSystem.GetCellSize();
    }
    //Unit location methods
    public void SetUnitAtGridNode(Vector2 gridPosition,Unit unit)
    {
        GridNode node= GetNodeAtPosition(gridPosition);
        node.AddUnit(unit);

    }
    public Unit GetUnitAtGridNode(Vector2 gridPosition)
    {
        GridNode node= GetNodeAtPosition(gridPosition);
        Unit unit=node.GetUnit();
        return unit;
    }
    public bool HasAnyUnitAtGridNode(Vector2 gridPosition)
    {
        
        GridNode node= GetNodeAtPosition(gridPosition);
        return node.HasAnyUnit();
    }
    public void RemoveUnitAtGridNode(Vector2 gridPosition,Unit unit)
    {
        GridNode node= GetNodeAtPosition(gridPosition);
        node.RemoveUnit(unit);

    }
    public void MoveUnitGridPosition(Unit unit, Vector2 startPosition,Vector2 targetPosition)
    {
        RemoveUnitAtGridNode(startPosition,unit);
        SetUnitAtGridNode(targetPosition,unit);
        GridNode unitNewNode=GetNodeAtPosition(targetPosition);
        unit.SetUnitNode(unitNewNode);
    }
    public NodeType GetNodeType(Vector2 gridPosition)
    {
        GridNode node= GetNodeAtPosition(gridPosition);
        return node.GetNodeType();
    }

}
