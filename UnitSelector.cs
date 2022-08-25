using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UnitSelector : MonoBehaviour
{
    //Singleton
    public static UnitSelector Instance {get; private set;}
    //Fields
    private float cellSize;
    private GridNode currentNode;
    private void Awake()
    {
        if(Instance!=null)
        {
            Debug.LogWarning("UnitSelector Singleton instance already exists");
            Destroy(gameObject);
            return;
        }
        Instance=this;

    }
    // Start is called before the first frame update
    void Start()
    {
        cellSize=LevelGrid.Instance.GetCellSize();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public GridNode GetUnitSelectorNode()
    {
        Vector2 currentPosition= transform.position;
        float currentNodeX= transform.position.x+cellSize/2;
        float currentNodeY= transform.position.y+cellSize/2;
        currentNode=LevelGrid.Instance.GetNodeAtPosition(new Vector2(currentNodeX,currentNodeY));
        return currentNode;
        
    }


}
