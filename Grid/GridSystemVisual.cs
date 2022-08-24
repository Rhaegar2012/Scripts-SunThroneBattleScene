using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystemVisual : MonoBehaviour
{
   //Fields
   [SerializeField] Transform grassNodePrefab;
   [SerializeField] Transform forestNodePrefab;
   [SerializeField] Transform riverNodePrefab;
   [SerializeField] Transform mountainNodePrefab;
   [SerializeField] Transform roadNodePrefab;
   private Transform nodeVisual;


   private void Start()
   {
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
}
