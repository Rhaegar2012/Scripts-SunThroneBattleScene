using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GridVisualType
{
    Movement,
    Attack,
    RangedAttack
}
public class GridSystemVisual : MonoBehaviour
{
   //Singleton
   public static GridSystemVisual Instance{get; private set;}
   //Fields
   [Header("Node Visual")]
   [SerializeField] Transform grassNodePrefab;
   [SerializeField] Transform forestNodePrefab;
   [SerializeField] Transform riverNodePrefab;
   [SerializeField] Transform mountainNodePrefab;
   [SerializeField] Transform roadNodePrefab;
   private Transform nodeVisual;
   [Header("Action Visual")]
   [SerializeField] Transform validMovementNodePrefab;
   [SerializeField] Transform attackNodePrefab;
   private List<Transform> gridSystemVisualList;

   private void Awake()
   {
        if(Instance!=null)
        {
            Debug.LogWarning("GridSystemVisual Singleton already exists");
            Destroy(gameObject);
            return;
        }
        Instance=this;
   }

   private void Start()
   {
        //Subscribed events 
        UnitActionSystem.Instance.OnSelectedUnitChanged+=UnitActionSystem_OnSelectedUnitChanged;
        gridSystemVisualList=new List<Transform>();
        for(int x=0;x<LevelGrid.Instance.GetWidth();x++)
        {
            for(int y=0;y<LevelGrid.Instance.GetHeight();y++)
            {
                Vector2 gridPosition= new Vector2(x,y);
                GridNode node= LevelGrid.Instance.GetNodeAtPosition(gridPosition);
                NodeType nodeType= node.GetNodeType();
                switch(nodeType)
                {
                    case NodeType.Grassland:
                        nodeVisual= Instantiate(grassNodePrefab,gridPosition,Quaternion.identity);
                        break;
                    case NodeType.Forest:
                        nodeVisual= Instantiate(forestNodePrefab,gridPosition,Quaternion.identity);
                        break;
                    case NodeType.Mountain:
                        nodeVisual= Instantiate(mountainNodePrefab,gridPosition,Quaternion.identity);
                        break;
                    case NodeType.River:
                        nodeVisual= Instantiate(riverNodePrefab,gridPosition,Quaternion.identity);
                        break;
                    case NodeType.Road:
                        nodeVisual= Instantiate(roadNodePrefab,gridPosition,Quaternion.identity);
                        break;
                }
            }
        }


   }

  
   private void UpdateGridVisual()
   {
        HideGridPositions();
        Unit selectedUnit= UnitActionSystem.Instance.GetSelectedUnit();
        int  movementRange=selectedUnit.GetMovementRange();
        Vector2 unitPosition= selectedUnit.GetUnitPosition();
        for(int x=(int)unitPosition.x-movementRange;x<(int)unitPosition.x+movementRange;x++)
        {
            for(int y=(int)unitPosition.y-movementRange;y<(int)unitPosition.y+movementRange;y++)
            {
                Vector2 gridPosition= new Vector2(x,y);
                if(!LevelGrid.Instance.IsValidGridPosition(gridPosition))
                {
                    continue;
                }
                if(LevelGrid.Instance.HasAnyUnitAtGridNode(gridPosition))
                {
                    Unit unitAtNode=LevelGrid.Instance.GetUnitAtGridNode(gridPosition);
                    if(unitAtNode.IsEnemy())
                    {
                        Instantiate(attackNodePrefab,gridPosition,Quaternion.identity);
                    }
                    else
                    {
                        continue;
                    }

                }
                Transform visualSingle=Instantiate(validMovementNodePrefab,gridPosition,Quaternion.identity) as Transform;
                gridSystemVisualList.Add(visualSingle);
            }
        }
   }
   private void UnitActionSystem_OnSelectedUnitChanged(object sender, EventArgs empty)
   {
        UpdateGridVisual();
   }
   private void HideGridPositions()
   {
        if(gridSystemVisualList.Count>0)
        {
            foreach(Transform gridVisual in gridSystemVisualList)
            {
                if(gridVisual!=null)
                {
                    Destroy(gridVisual.gameObject);
                }
            }
        }
   }


}
