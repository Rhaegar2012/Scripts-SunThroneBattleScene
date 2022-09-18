using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UnitSelector : MonoBehaviour
{
    //Singleton
    public static UnitSelector Instance {get; private set;}
    //Events
 
    //Fields
    private float cellSize;
    private GridNode currentNode;
    private Vector2 currentGridPosition;
    private PlayerInputActions playerInput;
    private bool isActive=true;
    private bool selectorKeyActive=false;
    private void Awake()
    {
        if(Instance!=null)
        {
            Debug.LogWarning("UnitSelector Singleton instance already exists");
            Destroy(gameObject);
            return;
        }
        Instance=this;
        playerInput=new PlayerInputActions();
        playerInput.Player.Enable();
        currentGridPosition= new Vector2(0,0);
        

    }
    // Start is called before the first frame update
    void Start()
    {
        cellSize=LevelGrid.Instance.GetCellSize();
        currentNode=LevelGrid.Instance.GetNodeAtPosition(currentGridPosition);
        TurnSystem.Instance.OnTurnChanged+=TurnSystem_OnTurnChanged;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKeyDown)
        {
            UnitSelectorMovement();
            //Debug.Log($"Current Node {currentNode.GetGridPosition().ToString()}");
        }
      
    }
    public GridNode UpdateUnitSelectorNode()
    {
        currentGridPosition= transform.position;
        float currentNodeX= transform.position.x+cellSize/2;
        float currentNodeY= transform.position.y+cellSize/2;
        currentNode=LevelGrid.Instance.GetNodeAtPosition(new Vector2(currentNodeX,currentNodeY));
        return currentNode;
        
    }

    public void UnitSelectorMovement()
    {
        Vector2 moveAmount = playerInput.Player.SelectorMovement.ReadValue<Vector2>();
        Vector2 trialPosition=currentNode.GetGridPosition()+moveAmount;
        if(LevelGrid.Instance.IsValidGridPosition(trialPosition))
        {
            transform.position+=new Vector3(moveAmount.x,moveAmount.y,0f);
            UpdateUnitSelectorNode();
        }
    }
    public bool MakeSelection()
    {
        return selectorKeyActive;
    }
    public void SwitchSelectorStatus()
    {
        selectorKeyActive=!selectorKeyActive;
        
    }
    public GridNode GetCurrentNode()
    {
        return currentNode;
    }
    public Vector2 GetGridPosition()
    {
        return currentGridPosition;
    }
    public void TurnSystem_OnTurnChanged(object sender, EventArgs empty)
    {
        isActive=!isActive;
        if(isActive)
        {
            CameraController.Instance.UpdateFollowingUnit(transform);
        }
        gameObject.SetActive(isActive);
    }

    public void SetSelectorActive(bool isActive)
    {
       gameObject.SetActive(isActive);     
    }


}
