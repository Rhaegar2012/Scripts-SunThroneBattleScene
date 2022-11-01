using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitActionSystem : MonoBehaviour
{
    //Singleton 
    public static UnitActionSystem Instance {get; private set;}
    //Events
    public event EventHandler OnSelectedUnitChanged;
    public event EventHandler OnDeselectedUnit;
    public event EventHandler OnActionPositionSelected;
    //Fields
    private Unit selectedUnit;
    private BaseAction baseAction=null;
    Vector2 actionPosition;
    private bool isBusy;
    private void Awake()
    {
        if(Instance!=null)
        {
            Debug.LogWarning("UnitActionSystem Singleton already exists");
            Destroy(gameObject);
            return;

        }
        Instance=this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!UnitSelector.Instance.MakeSelection())
        {
            return;
        }
        if(TryHandleUnitSelection())
        {
            UnitSelector.Instance.SwitchSelectorStatus();
            return;
        }
        if(TryHandleUnitActionMenu())
        {
            return;
        }
        //TryHandleSelectedAction();

    }
 
    private bool TryHandleUnitSelection()
    {
        GridNode selectorCurrentNode=UnitSelector.Instance.GetCurrentNode();      
        if(selectorCurrentNode.HasAnyUnit())
        {
            Unit nodeUnit=selectorCurrentNode.GetUnit();
            //Debug.Log($"Unit in node {nodeUnit.GetUnitType()}");
                
            //Unit already selected
            if(selectedUnit==nodeUnit)
            {
                return false;
            }
            //Unit has already completed action
            if(nodeUnit.UnitCompletedAction())
            {
                return false;
            }
            //Unit is an enemy;
            if(nodeUnit.IsEnemy())
            {
                return false;
            }
            SetSelectedUnit(nodeUnit);
            return true;

        }
        Vector2 unitSelectorActionNodePosition= UnitSelector.Instance.GetGridPosition();
        if(selectedUnit!=null && !baseAction.IsValidGridPositionList(unitSelectorActionNodePosition))
        {
            DeselectUnit();
        }
      
       return false;

    }
    private bool TryHandleUnitActionMenu()
    {
        //verify if node is outside of unit movement range
        //verify if unit selector is active if so , activate unit action menu
        if(selectedUnit!=null)
        {
            actionPosition=UnitSelector.Instance.GetGridPosition();
            if(actionPosition!=selectedUnit.GetUnitPosition()&&UnitSelector.Instance.MakeSelection())
            {

                 OnActionPositionSelected?.Invoke(this,EventArgs.Empty);
                 UnitSelector.Instance.SetSelectorActive(false);
                 return true;
            }
            if(actionPosition==selectedUnit.GetUnitPosition()&&UnitSelector.Instance.MakeSelection())
            {
            
                OnActionPositionSelected?.Invoke(this,EventArgs.Empty);
                UnitSelector.Instance.SetSelectorActive(false);
                return true;
            }
            int testDistance= Mathf.Abs((int)actionPosition.x)+Mathf.Abs((int)actionPosition.y);
            if(testDistance>selectedUnit.GetMovementRange()&&UnitSelector.Instance.MakeSelection())
            {
                DeselectUnit();
                return true;
            }
           

           
        }
        return false; 

    }

    public void TryHandleSelectedAction(Button button)
    {
        SetAction(button.name);
        Vector2 unitSelectorActionNodePosition = UnitSelector.Instance.GetGridPosition();
        if(selectedUnit!=null)
        {
         
            if(baseAction.IsValidGridPositionList(unitSelectorActionNodePosition))
            {
                
                baseAction.TakeAction(unitSelectorActionNodePosition,ClearBusy);
                OnActionPositionSelected?.Invoke(this,EventArgs.Empty);
                UnitSelector.Instance.SetSelectorActive(true);
                SetSelectedUnit(null);
            }
        }
    }
    private void SetSelectedUnit(Unit unit)
    {
        selectedUnit=unit;
        if(selectedUnit!=null)
        {
            SetAction("Move");
        }
        
        OnSelectedUnitChanged?.Invoke(this,EventArgs.Empty);
    }
    private void SetAction(string actionName)
    {
        baseAction=selectedUnit.GetAction(actionName);
    }
    public Unit GetSelectedUnit()
    {
        return selectedUnit;
    }
    public void ClearBusy()
    {
        this.isBusy=false;
    }
    private void DeselectUnit()
    {
        selectedUnit=null;
        baseAction=null;
        OnDeselectedUnit?.Invoke(this,EventArgs.Empty);
    }
}
