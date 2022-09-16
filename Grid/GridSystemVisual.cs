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
   private Transform[,] gridSystemVisualArray;
   private List<Transform> enemyGridVisualsList;
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
        //gridSystemVisual Array
        gridSystemVisualArray= new Transform[LevelGrid.Instance.GetWidth(),LevelGrid.Instance.GetHeight()];
        //EnemyGriddVisualList
        enemyGridVisualsList= new List<Transform>();
        //Subscribed events 
        UnitActionSystem.Instance.OnSelectedUnitChanged+=UnitActionSystem_OnSelectedUnitChanged;
        UnitActionSystem.Instance.OnDeselectedUnit+=UnitActionSystem_OnDeselectedUnit;
        MoveAction.OnAnyUnitMoved+=MoveAction_OnAnyUnitMoved;
        //Creates visualGrid singles
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
                //Valid Movement transform
                Transform visualSingle=Instantiate(validMovementNodePrefab,gridPosition,Quaternion.identity) as Transform;
                gridSystemVisualArray[x,y]=visualSingle;
                SpriteRenderer renderer=visualSingle.GetComponentInChildren<SpriteRenderer>();
                renderer.enabled=false;
            }
        }


   }

  
   private void UpdateGridVisual()
   {
        HideGridPositions();
        Unit selectedUnit= UnitActionSystem.Instance.GetSelectedUnit();
        if(selectedUnit!=null)
        {
              int  movementRange=selectedUnit.GetMovementRange();
              Vector2 unitPosition= selectedUnit.GetUnitPosition();
              for(int x=-movementRange;x<=movementRange;x++)
              {
                for(int y=-movementRange;y<=movementRange;y++)
                {
                
                    Vector2 offsetGridPosition= new Vector2(x,y);
                    Vector2 testGridPosition=unitPosition+offsetGridPosition;
                    if(!LevelGrid.Instance.IsValidGridPosition(testGridPosition))
                    {
                        continue;
                    }
                    GridNode testNode= LevelGrid.Instance.GetNodeAtPosition(testGridPosition);
                    NodeType testNodeType= testNode.GetNodeType();
                    List<NodeType> walkableNodeTypes= selectedUnit.GetWalkableNodeTypeList();
                    if(LevelGrid.Instance.HasAnyUnitAtGridNode(testGridPosition))
                    {
                        Unit unitAtNode=LevelGrid.Instance.GetUnitAtGridNode(testGridPosition);
                        if(unitAtNode.IsEnemy())
                        {
                            DisplayAttackNodes(testGridPosition);
                            continue;
                        }
                        else
                        {
                            continue;
                        }

                    }
                    int testDistance= Mathf.Abs(x)+Mathf.Abs(y);
                  if(testDistance>movementRange)
                  {
                    continue;
                  }
                  if(!walkableNodeTypes.Contains(testNodeType))
                  {
                    continue;
                  }

                    Transform visualSingle=gridSystemVisualArray[(int)testGridPosition.x,(int)testGridPosition.y];
                    SpriteRenderer renderer=visualSingle.GetComponentInChildren<SpriteRenderer>();
                    renderer.enabled=true;
                }
             }

        }
      
   }
   private void UnitActionSystem_OnSelectedUnitChanged(object sender, EventArgs empty)
   {
        UpdateGridVisual();
   }
   private void UnitActionSystem_OnDeselectedUnit(object sender, EventArgs empty)
   {
        HideGridPositions();
   }
   private void MoveAction_OnAnyUnitMoved(object sender, EventArgs empty)
   {
        HideGridPositions();
   }
   private void DisplayAttackNodes(Vector2 gridPosition)
   {
      List<Vector2> offsetDirections= new List<Vector2>{new Vector2(1f,0f),
                                                        new Vector2(-1f,0f),
                                                        new Vector2(0f,1f),
                                                        new Vector2(0f,-1f)};
      foreach(Vector2 direction in offsetDirections)
      {
        Vector2 attackNodePosition=gridPosition+direction;
        GridNode attackNode= LevelGrid.Instance.GetNodeAtPosition(attackNodePosition);
        attackNode.SetAttackNode(true);
        Transform enemyGridVisual=Instantiate(attackNodePrefab,attackNodePosition,Quaternion.identity);
        enemyGridVisualsList.Add(enemyGridVisual);
      }
   }
   private void HideGridPositions()
   {
        //Clears unit visual and attack nodes
        for(int x=0;x<LevelGrid.Instance.GetWidth();x++)
        {
            for(int y=0;y<LevelGrid.Instance.GetHeight();y++)
            {
                Transform visualSingle=gridSystemVisualArray[x,y];
                SpriteRenderer renderer=visualSingle.GetComponentInChildren<SpriteRenderer>();
                renderer.enabled=false;
                //Resets attack nodes 
                GridNode node = LevelGrid.Instance.GetNodeAtPosition(new Vector2(x,y));
                node.SetAttackNode(false);
            }
        }
        //Clears enemy visuals
        foreach(Transform enemyVisual in enemyGridVisualsList)
        {
            Destroy(enemyVisual.gameObject);
        }
        enemyGridVisualsList.Clear();
   }


}
